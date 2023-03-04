using Poketto.Domain.Common;
using Poketto.Domain.Enums;

namespace Poketto.Domain.Entities;

public class JournalEntryItem : BaseAuditableEntity
{
    public string Description { get; set; } = string.Empty;
    public double Amount { get; set; }
    public TransactionType TransactionType { get; set; }
    public Guid JournalEntryId { get; set; }
    public JournalEntry JournalEntry { get; set; } = new JournalEntry();
    public Guid AccountId { get; set; }
    public Account Account { get; set; } = new Account();
}
