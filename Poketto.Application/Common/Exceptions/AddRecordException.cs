namespace Poketto.Application.Common.Exceptions
{
    public class AddRecordException : DomainException
    {
        public AddRecordException()
        {
        }
        public AddRecordException(string? message) : base(message)
        {
        }

        public AddRecordException(string? message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
