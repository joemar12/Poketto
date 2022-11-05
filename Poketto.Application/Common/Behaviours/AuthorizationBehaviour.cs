﻿using MediatR;
using Microsoft.Extensions.Configuration;
using Poketto.Application.Common.Interfaces;
using Poketto.Application.Common.Security;
using System.Reflection;

namespace Poketto.Application.Common.Behaviours
{
    public class AuthorizationBehaviour<TRequest, TResponse>
        : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IConfiguration _config;

        public AuthorizationBehaviour(
            ICurrentUserService currentUserService,
            IConfiguration config)
        {
            _currentUserService = currentUserService;
            _config = config;
        }

        public async Task<TResponse> Handle(TRequest request,
                                            CancellationToken cancellationToken,
                                            RequestHandlerDelegate<TResponse> next)
        {
            var authorizeAttributes = request.GetType().GetCustomAttributes<AuthorizeAttribute>();

            if (authorizeAttributes.Any())
            {
                // Must be authenticated user
                if (string.IsNullOrEmpty(_currentUserService.GetCurrentUser()))
                {
                    throw new UnauthorizedAccessException();
                }
                // if scopes are required
                var requiredScopes = authorizeAttributes
                    .SelectMany(x =>
                    {
                        if (x.Scopes is null || x.Scopes.Length < 1)
                        {
                            return _config.GetValue<string>(x.RequiredScopesConfigurationKey).Split(' ').ToList();
                        }
                        else
                        {
                            return x.Scopes.ToList();
                        }
                    })
                    .ToList();

                if (requiredScopes is not null
                    && requiredScopes.Count > 0
                    && requiredScopes.All(x => !string.IsNullOrEmpty(x)))
                {
                    var hasRequiredScopes = requiredScopes
                        .Intersect(_currentUserService.GetCurrentUserScopes() ?? new List<string>())
                        .Any();
                    if (!hasRequiredScopes)
                    {
                        throw new UnauthorizedAccessException("Required scope(s) not found");
                    }
                }
            }

            // User is authorized / authorization not required
            return await next();
        }
    }
}