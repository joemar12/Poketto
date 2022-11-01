namespace Poketto.Application.Common.Exceptions
{
    public class DeleteRecordException : DomainException
    {
        public DeleteRecordException()
        {
        }

        public DeleteRecordException(string? message) : base(message)
        {
        }

        public DeleteRecordException(string? message, Exception innerException) : base(message, innerException)
        {
        }
    }
}