using Poketto.Application.Common.Exceptions;

namespace Poketto.Application.GraphQL.Errors;

public class NotFoundError : BaseResultError
{
    public NotFoundError(string message)
    {
        Message = message;
        Code = Common.ErrorCodes.NotFound;
    }
    public static NotFoundError? CreateErrorFrom(NotFoundException ex)
    {
        return new NotFoundError(ex.Message);
    }
}