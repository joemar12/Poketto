using Poketto.Application.GraphQL.Queries.Accounts;

namespace Poketto.Application.GraphQL.Mutations.Accounts
{
    public record AccountPayload
    {
        public AccountDto? Account { get; set; }
        public string? Error { get; set; }
    }
}
