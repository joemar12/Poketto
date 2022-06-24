using HotChocolate.AspNetCore.Authorization;
using HotChocolate.Types.Descriptors;
using System.Reflection;

namespace Poketto.Application.GraphQL.Security
{
    [AttributeUsage(
    AttributeTargets.Class
    | AttributeTargets.Struct
    | AttributeTargets.Property
    | AttributeTargets.Method,
    Inherited = true,
    AllowMultiple = true)]
    public class RequireScopeAuthorizationAttribute : DescriptorAttribute
    {
        public RequireScopeAuthorizationAttribute(params string[] requiredScopes)
        {
            Scopes = requiredScopes ?? throw new ArgumentNullException(nameof(requiredScopes));
        }
        public string[]? Scopes { get; set; }
        public string? RequiredScopesConfigurationKey { get; set; }
        public ApplyPolicy Apply { get; set; } = ApplyPolicy.BeforeResolver;

        protected override void TryConfigure(IDescriptorContext context, IDescriptor descriptor, ICustomAttributeProvider element)
        {
            if (descriptor is IObjectTypeDescriptor type)
            {
                type.Directive(CreateDirective());
            }
            else if (descriptor is IObjectFieldDescriptor field)
            {
                field.Directive(CreateDirective());
            }
        }

        private RequireScopeAuthorizationDirective CreateDirective()
        {
            if (Scopes is not null && Scopes.Length > 0)
            {
                return new RequireScopeAuthorizationDirective(
                    Scopes,
                    apply: Apply);
            }
            else
            {
                return new RequireScopeAuthorizationDirective(
                    RequiredScopesConfigurationKey,
                    apply: Apply);
            }
        }
    }
}
