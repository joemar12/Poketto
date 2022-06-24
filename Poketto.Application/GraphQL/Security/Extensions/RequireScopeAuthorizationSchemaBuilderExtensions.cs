using Microsoft.Extensions.DependencyInjection;

namespace Poketto.Application.GraphQL.Security.Extensions
{
    public static class RequireScopeAuthorizationSchemaBuilderExtensions
    {
        public static ISchemaBuilder AddRequireScopeAuthorizationDirective(
        this ISchemaBuilder builder)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            return builder.AddDirectiveType<RequireScopeAuthorizationDirectiveType>();
        }
    }
}
