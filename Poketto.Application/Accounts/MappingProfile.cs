using AutoMapper;
using Poketto.Application.Accounts.Commands;
using Poketto.Application.GraphQL.Accounts;
using Poketto.Domain.Entities;

namespace Poketto.Application.Accounts
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Account, AccountDto>();
            CreateMap<AccountInput, AddAccountCommand>();
            CreateMap<AddAccountCommand, Account>();
            CreateMap<UpdateAccountCommand, Account>();
            CreateMap<AccountInput, UpdateAccountCommand>();
        }
    }
}
