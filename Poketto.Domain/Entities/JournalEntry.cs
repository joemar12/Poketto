using Poketto.Domain.Common;
using Poketto.Domain.Enums;

namespace Poketto.Domain.Entities
{
    public class JournalEntry : BaseAuditableEntity
    {
        public double Amount { get; set; }

        public Guid TransactionJournalId { get; set; }
        public virtual TransactionJournal TransactionJournal { get; set; } = new TransactionJournal();
        public Guid AccountId { get; set; }
        public virtual Account Account { get; set; } = new Account();
    }
}
