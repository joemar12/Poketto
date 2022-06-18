using Poketto.Domain.Common;
using Poketto.Domain.Enums;

namespace Poketto.Domain.Entities
{
    public class Account : BaseAuditableEntity
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public AccountType AccountType { get; set; }
        public string OwnerUserId { get; set; } = string.Empty;
        public bool IsPlaceholder { get; set; }

        public virtual IList<JournalEntry> JournalEntries { get; set; } =new List<JournalEntry>();
        public Guid? ParentAccountId { get; set; }
        public virtual Account? ParentAccount { get; set; }
        public virtual IList<Account>? ChildAccounts { get; set; }
    }
}

