using Poketto.Application.Common.Exceptions;

namespace Poketto.Application.GraphQL.Errors
{
    public class ValidationError : BaseResultError
    {
        public ValidationError(string message, IDictionary<string, string[]> errors)
        {
            Message = message;
            Code = Common.ErrorCodes.ValidationFailed;
            Failures = errors;
        }
        public IDictionary<string, string[]> Failures { get; set; }
        public static ValidationError CreateErrorFrom(ValidationException ex)
        {
            return new ValidationError(ex.Message, ex.Errors);
        }
    }
}