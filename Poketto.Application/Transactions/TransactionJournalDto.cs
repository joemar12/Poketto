using AutoMapper;
using Poketto.Application.Common;
using Poketto.Application.Common.Mapping;
using Poketto.Domain.Entities;
using Poketto.Domain.Enums;

namespace Poketto.Application.Transactions
{
    public record TransactionJournalDto : BaseAuditableEntityDto, IMappableFrom<TransactionJournal>
    {
        public string Description { get; set; } = string.Empty;
        public Guid TransactionGroupId { get; set; }
        public TransactionType TransactionType { get; set; }
        public string OwnerUserId { get; set; } = string.Empty;

        public IList<JournalEntryDto> JournalEntries { get; set; } = new List<JournalEntryDto>();

        public void CreateMap(Profile profile)
        {
            profile.CreateMap<TransactionJournal, TransactionJournalDto>();
        }
    }
}
