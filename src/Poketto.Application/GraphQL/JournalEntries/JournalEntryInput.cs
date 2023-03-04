using Poketto.Application.Transactions;
using Poketto.Domain.Enums;

namespace Poketto.Application.GraphQL.JournalEntries;

public class JournalEntryInput
{
    public string RefCode { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime JournalEntryDate { get; set; }
    public JournalEntryStatus Status { get; set; }
    public IList<JournalEntryItemDto> JournalEntryItems { get; set; } = new List<JournalEntryItemDto>();
}
