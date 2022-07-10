using Poketto.Application.Common;

namespace Poketto.Application.GraphQL.Errors
{
    public class ValidationError : BaseError
    {
        public ValidationError()
        {
            Message = "Invalid parameter";
        }

        public ValidationError(string message)
        {
            Message = message;
        }
    }
}
