namespace Poketto.Application.Common.Exceptions
{
    public class UpdateRecordException : DomainException
    {
        public UpdateRecordException()
        {
        }
        public UpdateRecordException(string? message) : base(message)
        {
        }

        public UpdateRecordException(string? message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
