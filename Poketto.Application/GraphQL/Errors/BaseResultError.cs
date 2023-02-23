namespace Poketto.Application.GraphQL.Errors
{
    public abstract class BaseResultError : IResultError
    {
        public string Code { get; protected set; } = string.Empty;
        public string Message { get; protected set; } = string.Empty;
    }
}
