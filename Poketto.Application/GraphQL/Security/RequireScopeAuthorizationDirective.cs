using HotChocolate.AspNetCore.Authorization;
using System.Runtime.Serialization;

namespace Poketto.Application.GraphQL.Security
{
    public class RequireScopeAuthorizationDirective : ISerializable
    {
        public RequireScopeAuthorizationDirective(string? configKey,
                                     ApplyPolicy apply = ApplyPolicy.BeforeResolver) 
            : this(null, configKey, apply)
        {

        }

        public RequireScopeAuthorizationDirective(IReadOnlyList<string>? scopes,
                                     string? configKey = null,
                                     ApplyPolicy apply = ApplyPolicy.BeforeResolver)
        {
            Scopes = scopes;
            RequiredScopesConfigurationKey = configKey;
            Apply = apply;
        }

        public IReadOnlyList<string>? Scopes { get; }
        public string? RequiredScopesConfigurationKey { get; }
        public ApplyPolicy Apply { get; }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue(nameof(Scopes), Scopes?.ToList());
            info.AddValue(nameof(RequiredScopesConfigurationKey), RequiredScopesConfigurationKey ?? string.Empty);
            info.AddValue(nameof(Apply), (int)Apply);
        }
    }
}
