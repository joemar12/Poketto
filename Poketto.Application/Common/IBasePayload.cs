namespace Poketto.Application.Common
{
    public interface IBasePayload
    {
        void AddError(IBaseError error);
    }
}