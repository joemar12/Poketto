using Poketto.Application.Common;

namespace Poketto.Application.GraphQL.Errors
{
    public class NotFoundError : BaseError
    {
        public NotFoundError()
        {
            Message = "Record not found";
        }

        public NotFoundError(string message)
        {
            Message = message;
        }
    }
}