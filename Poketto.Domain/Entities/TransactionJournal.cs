using Poketto.Domain.Common;
using Poketto.Domain.Enums;

namespace Poketto.Domain.Entities
{
    public class TransactionJournal : BaseAuditableEntity
    {
        public string Description { get; set; } = string.Empty;
        public Guid TransactionGroupId { get; set; }
        public TransactionType TransactionType { get; set; }
        public string OwnerUserId { get; set; } = string.Empty;

        public virtual IList<JournalEntry> JournalEntries { get; set; } = new List<JournalEntry>();
        public virtual TransactionGroup TransactionGroup { get; set; } = new TransactionGroup();
    }
}
