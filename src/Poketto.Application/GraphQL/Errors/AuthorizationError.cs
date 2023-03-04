using Poketto.Application.Common.Exceptions;

namespace Poketto.Application.GraphQL.Errors;

public class AuthorizationError : BaseResultError
{
    public AuthorizationError(string message, string code)
    {
        Message = message;
        Code = code;
    }
    public static AuthorizationError? CreateErrorFrom(AuthorizationException ex)
    {
        return new AuthorizationError(ex.Message, ex.Code);
    }
}