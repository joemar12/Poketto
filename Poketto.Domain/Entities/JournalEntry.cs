using Poketto.Domain.Common;

namespace Poketto.Domain.Entities
{
    public class JournalEntry : BaseAuditableEntity
    {
        public string RefCode { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime JournalEntryDate { get; set; }
        public string OwnerUserId { get; set; } = string.Empty;
        public IList<JournalEntryItem> JournalEntryItems { get; set; } = new List<JournalEntryItem>();
    }
}
