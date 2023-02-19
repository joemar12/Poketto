using AutoMapper;

namespace Poketto.Application.Common.Mapping
{
    public interface IMappable<T>
    {
        void CreateMap(Profile profile);
    }
}