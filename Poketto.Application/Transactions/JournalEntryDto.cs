using Poketto.Application.Common;
using Poketto.Domain.Enums;

namespace Poketto.Application.Transactions
{
    public record JournalEntryDto : BaseAuditableDto
    {
        public string RefCode { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime JournalEntryDate { get; set; }
        public JournalEntryStatus Status { get; set; }
        public string OwnerUserId { get; set; } = string.Empty;

        public IList<JournalEntryItemDto> JournalEntryItems { get; set; } = new List<JournalEntryItemDto>();
    }
}