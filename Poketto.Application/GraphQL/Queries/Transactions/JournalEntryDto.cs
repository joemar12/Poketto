using AutoMapper;
using Poketto.Application.Common.Mapping;
using Poketto.Application.Models;
using Poketto.Domain.Entities;

namespace Poketto.Application.GraphQL.Queries.Transactions
{
    public record JournalEntryDto : BaseAuditableEntityDto, IMappableFrom<JournalEntry>
    {
        public double Amount { get; set; }
        public Guid TransactionJournalId { get; set; }
        public Guid AccountId { get; set; }
        public string AccountName { get; set; } = string.Empty;
        public TransactionJournalDto TransactionJournal { get; set; } = new TransactionJournalDto();

        public void CreateMap(Profile profile)
        {
            profile.CreateMap<JournalEntry, JournalEntryDto>()
                .ForMember(x => x.AccountName, opt => opt.MapFrom(x => x.Account.Name));
        }
    }
}
