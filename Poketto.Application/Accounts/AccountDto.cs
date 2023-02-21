using Poketto.Application.Common;
using Poketto.Domain.Enums;

namespace Poketto.Application.Accounts
{
    public record AccountDto : BaseAuditableEntityDto
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public AccountType AccountType { get; set; }
        public string OwnerUserId { get; set; } = string.Empty;
        public Guid ParentAccountId { get; set; }
        public bool IsPlaceholder { get; set; }

        public IEnumerable<AccountDto>? ChildAccounts { get; set; }
    }
}