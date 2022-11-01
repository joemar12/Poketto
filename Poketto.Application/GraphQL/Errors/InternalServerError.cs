using Poketto.Application.Common;

namespace Poketto.Application.GraphQL.Errors
{
    public class InternalServerError : BaseError
    {
        public InternalServerError()
        {
            Message = "Internal server error";
        }

        public InternalServerError(string message)
        {
            Message = message;
        }
    }
}