namespace Poketto.Application.GraphQL.Errors
{
    [InterfaceType("ResultError")]
    public interface IResultError
    {
        string Code { get; }
        string Message { get; }
    }
}
