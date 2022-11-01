using AutoMapper;

namespace Poketto.Application.Common.Mapping
{
    public interface IMappableFrom<T>
    {
        void CreateMap(Profile profile);
    }
}