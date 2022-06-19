using AutoMapper;
using Poketto.Application.Common.Mapping;
using Poketto.Domain.Entities;
using Poketto.Domain.Enums;

namespace Poketto.Application.Models
{
    public class AccountDto : BaseAuditableEntityDto, IMappableFrom<Account>
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public AccountType AccountType { get; set; }
        public string OwnerUserId { get; set; } = string.Empty;
        public bool IsPlaceholder { get; set; }

        public IList<AccountDto>? ChildAccounts { get; set; }

        public void CreateMap(Profile profile)
        {
            profile.CreateMap<Account, AccountDto>();
        }
    }
}
