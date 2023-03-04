namespace Poketto.Application.Common.Security;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = true)]
public class AuthorizeAttribute : Attribute
{
    public AuthorizeAttribute(params string[] requiredScopes)
    {
        Scopes = requiredScopes ?? throw new ArgumentNullException(nameof(requiredScopes));
    }

    public string[]? Scopes { get; set; }
    public string? RequiredScopesConfigurationKey { get; set; }
}