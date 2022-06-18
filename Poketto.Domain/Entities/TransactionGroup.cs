using Poketto.Domain.Common;

namespace Poketto.Domain.Entities
{
    public class TransactionGroup : BaseAuditableEntity
    {
        public string Title { get; set; } = string.Empty;
        public string OwnerUserId { get; set; } = string.Empty;
        public virtual IList<TransactionJournal> TransactionJournals { get; set; } = new List<TransactionJournal>();
    }
}
