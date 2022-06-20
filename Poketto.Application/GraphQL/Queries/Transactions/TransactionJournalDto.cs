using AutoMapper;
using Poketto.Application.Common.Mapping;
using Poketto.Application.Models;
using Poketto.Domain.Entities;
using Poketto.Domain.Enums;

namespace Poketto.Application.GraphQL.Queries.Transactions
{
    public class TransactionJournalDto : BaseAuditableEntityDto, IMappableFrom<TransactionJournal>
    {
        public string Description { get; set; } = string.Empty;
        public Guid TransactionGroupId { get; set; }
        public TransactionType TransactionType { get; set; }
        public string OwnerUserId { get; set; } = string.Empty;

        public IList<JournalEntry> JournalEntries { get; set; } = new List<JournalEntry>();

        public void CreateMap(Profile profile)
        {
            profile.CreateMap<TransactionJournal, TransactionJournalDto>();
        }
    }
}
