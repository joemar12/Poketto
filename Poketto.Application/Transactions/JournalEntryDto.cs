using Poketto.Application.Common;

namespace Poketto.Application.Transactions
{
    public record JournalEntryDto : BaseAuditableEntityDto
    {
        public string RefCode { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime JournalEntryDate { get; set; }
        public string OwnerUserId { get; set; } = string.Empty;

        public IList<JournalEntryItemDto> JournalEntryItems { get; set; } = new List<JournalEntryItemDto>();
    }
}