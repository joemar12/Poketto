using Poketto.Domain.Enums;

namespace Poketto.Application.GraphQL.Accounts
{
    public record AccountInput
    {
        public Guid? Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public AccountType AccountType { get; set; }
        public Guid? ParentAccountId { get; set; }
        public bool IsPlaceholder { get; set; }
    }
}