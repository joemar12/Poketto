﻿using MediatR.Pipeline;
using Microsoft.Extensions.Logging;
using Poketto.Application.Common.Interfaces;

namespace Poketto.Application.Common.Behaviours;

public class LoggingBehaviour<TRequest> : IRequestPreProcessor<TRequest> where TRequest : notnull
{
    private readonly ILogger _logger;
    private readonly ICurrentUserService _currentUserService;

    public LoggingBehaviour(ILogger<TRequest> logger, ICurrentUserService currentUserService)
    {
        _logger = logger;
        _currentUserService = currentUserService;
    }

    public Task Process(TRequest request, CancellationToken cancellationToken)
    {
        var requestName = typeof(TRequest).Name;
        var userId = _currentUserService.GetCurrentUser() ?? string.Empty;
        _logger.LogInformation("Poketto Request: {Name} {@UserId} {@Request}",
            requestName, userId, request);
        return Task.CompletedTask;
    }
}