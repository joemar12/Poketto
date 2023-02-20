using AutoMapper;
using Poketto.Domain.Entities;

namespace Poketto.Application.Accounts
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Account, AccountDto>();
        }
    }
}
