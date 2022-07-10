using Poketto.Application.Common;

namespace Poketto.Application.GraphQL.Errors
{
    public class UnauthorizedError : BaseError
    {
        public UnauthorizedError()
        {
            Message = "Unauthorized to access resource";
        }

        public UnauthorizedError(string message)
        {
            Message = message;
        }
    }
}
