﻿using Poketto.Application.Common.Interfaces;
using System.Security.Claims;

namespace Poketto.Api.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public string? GetCurrentUser() => _httpContextAccessor.HttpContext?.User?.FindFirstValue("emails");
    }
}
