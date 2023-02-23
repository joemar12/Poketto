namespace Poketto.Application.GraphQL.Errors
{
    public class InternalServerError : BaseResultError
    {
        public InternalServerError(string message)
        {
            Message = message;
            Code = Common.ErrorCodes.InternalServerError;
        }
        public static InternalServerError? CreateErrorFrom(Exception ex)
        {
            return new InternalServerError(ex.Message);
        }
    }
}