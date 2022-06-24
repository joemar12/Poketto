using HotChocolate.AspNetCore.Authorization;
using HotChocolate.Resolvers;

namespace Poketto.Application.GraphQL.Security
{
    public interface IRequireScopeAuthorizationHandler
    {
        AuthorizeResult ValidateScopes(
            IMiddlewareContext context,
            RequireScopeAuthorizationDirective directive);
    }
}
