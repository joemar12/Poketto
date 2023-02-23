namespace Poketto.Application.Common.Exceptions
{
    public class AuthorizationException : DomainException
    {
        public AuthorizationException() { }
        public AuthorizationException(string? message, string? code) : base(message)
        {
            Code = code ?? string.Empty;
        }

        public AuthorizationException(string? message, string? code, Exception innerException) : base(message, innerException)
        {
            Code = code ?? string.Empty;
        }
        public string Code { get; set; } = string.Empty;
    }
}