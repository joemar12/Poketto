using HotChocolate.Execution.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Poketto.Application.GraphQL.Security.Extensions
{
    public static class RequireScopeRequestExecutorBuilder
    {
        public static IRequestExecutorBuilder AddRequiredScopesAuthorization(this IRequestExecutorBuilder builder)
        {
            builder.AddAuthorization();

            builder.ConfigureSchema(sb => sb.AddRequireScopeAuthorizationDirective());
            builder.Services.TryAddSingleton<IRequireScopeAuthorizationHandler, DefaultRequireScopeAuthorizationHandler>();
            return builder;
        }
    }
}
