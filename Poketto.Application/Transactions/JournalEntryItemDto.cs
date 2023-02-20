using AutoMapper;
using Poketto.Application.Common;
using Poketto.Application.Common.Mapping;
using Poketto.Domain.Entities;
using Poketto.Domain.Enums;

namespace Poketto.Application.Transactions
{
    public record JournalEntryItemDto : BaseAuditableEntityDto
    {
        public string Description { get; set; } = string.Empty;
        public double Amount { get; set; }
        public TransactionType TransactionType { get; set; }
        public Guid JournalEntryId { get; set; }
        public Guid AccountId { get; set; }
        public string AccountName { get; set; } = string.Empty;
    }
}