using AutoMapper;
using Poketto.Application.Common;
using Poketto.Application.Common.Mapping;
using Poketto.Domain.Entities;
using Poketto.Domain.Enums;

namespace Poketto.Application.Accounts
{
    public record AccountDto : BaseAuditableEntityDto, IMappableFrom<Account>
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public AccountType AccountType { get; set; }
        public string OwnerUserId { get; set; } = string.Empty;
        public Guid ParentAccountId { get; set; }
        public bool IsPlaceholder { get; set; }

        public IEnumerable<AccountDto>? ChildAccounts { get; set; }

        public void CreateMap(Profile profile)
        {
            profile.CreateMap<Account, AccountDto>();
        }
    }
}