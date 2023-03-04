using Microsoft.Identity.Web;
using Poketto.Application.Common.Interfaces;
using System.Security.Claims;

namespace Poketto.Api.Services;

public class CurrentUserService : ICurrentUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CurrentUserService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public string? GetCurrentUser() => _httpContextAccessor.HttpContext?.User?.FindFirstValue("emails");

    public IList<string>? GetCurrentUserScopes()
    {
        var result = new List<string>();
        var userClaimsPrincipal = _httpContextAccessor.HttpContext?.User;
        if (userClaimsPrincipal is not null)
        {
            var scopeClaims = userClaimsPrincipal.FindAll(ClaimConstants.Scope)
                .Union(userClaimsPrincipal.FindAll(ClaimConstants.Scp))
                .ToList();
            result.AddRange(scopeClaims.SelectMany(x => x.Value.Split(' ')).ToList());
        }
        return result;
    }
}