namespace Poketto.Application.Common.Interfaces
{
    public interface ICurrentUserService
    {
        string? GetCurrentUser();
        IList<string>? GetCurrentUserScopes();
    }
}
