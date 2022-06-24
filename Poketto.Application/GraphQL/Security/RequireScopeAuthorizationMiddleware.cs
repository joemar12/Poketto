using HotChocolate.AspNetCore.Authorization;
using HotChocolate.Resolvers;

namespace Poketto.Application.GraphQL.Security
{
    internal sealed class RequireScopeAuthorizationMiddleware
    {
        private readonly FieldDelegate _next;
        private readonly IRequireScopeAuthorizationHandler _requireScopeHandler;

        public RequireScopeAuthorizationMiddleware(FieldDelegate next, IRequireScopeAuthorizationHandler requireScopeHandler)
        {
            _next = next ??
                throw new ArgumentNullException(nameof(next));
            _requireScopeHandler = requireScopeHandler ??
                throw new ArgumentNullException(nameof(requireScopeHandler));
        }

        public async Task InvokeAsync(IDirectiveContext context)
        {
            RequireScopeAuthorizationDirective directive = context.Directive.ToObject<RequireScopeAuthorizationDirective>();

            if (directive.Apply == ApplyPolicy.AfterResolver)
            {
                await _next(context).ConfigureAwait(false);
                AuthorizeResult state = _requireScopeHandler.ValidateScopes(context, directive);
                if (state != AuthorizeResult.Allowed && !IsError(context))
                {
                    SetError(context, state);
                }
            }
            else
            {
                AuthorizeResult state = _requireScopeHandler.ValidateScopes(context, directive);
                if (state == AuthorizeResult.Allowed)
                {
                    await _next(context).ConfigureAwait(false);
                }
                else
                {
                    SetError(context, state);
                }
            }
        }

        private bool IsError(IMiddlewareContext context) => context.Result is IError or IEnumerable<IError>;

        private void SetError(IMiddlewareContext context,
                              AuthorizeResult state)
        => context.Result = state switch
        {
            AuthorizeResult.NotAllowed
                => ErrorBuilder.New()
                    .SetMessage("Required scope(s) not found")
                    .SetCode(ErrorCodes.Authentication.NotAuthorized)
                    .SetPath(context.Path)
                    .AddLocation(context.Selection.SyntaxNode)
                    .Build(),
            _
                => ErrorBuilder.New()
                    .SetMessage("The current user is not authorized to access this resource.")
                    .SetCode(ErrorCodes.Authentication.NotAuthenticated)
                    .SetPath(context.Path)
                    .AddLocation(context.Selection.SyntaxNode)
                    .Build()
        };
    }
}
