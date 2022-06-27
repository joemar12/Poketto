using AutoMapper;
using Poketto.Application.Common.Mapping;
using Poketto.Application.Models;
using Poketto.Domain.Entities;
using Poketto.Domain.Enums;

namespace Poketto.Application.GraphQL.Queries.Transactions
{
    public class TransactionGroupDto : BaseAuditableEntityDto, IMappableFrom<TransactionGroup>
    {
        public string Title { get; set; } = string.Empty;
        public string OwnerUserId { get; set; } = string.Empty;
        public double TotalDebits 
        {
            get
            {
                var totalDebits = JournalEntries.Where(x => x.TransactionJournal.TransactionType == TransactionType.Debit).Sum(x => x.Amount);
                return totalDebits;
            }
        }
        public double TotalCredits
        {
            get
            {
                var totalDebits = JournalEntries.Where(x => x.TransactionJournal.TransactionType == TransactionType.Credit).Sum(x => x.Amount);
                return totalDebits;
            }
        }
        public IList<JournalEntry> JournalEntries { get; set; } = new List<JournalEntry>();

        public void CreateMap(Profile profile)
        {
            profile.CreateMap<TransactionGroup, TransactionGroupDto>()
                .ForMember(x => x.JournalEntries, opt => opt.MapFrom(x => x.TransactionJournals.SelectMany(x => x.JournalEntries)));
        }
    }
}
