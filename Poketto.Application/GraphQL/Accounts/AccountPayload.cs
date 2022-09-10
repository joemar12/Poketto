using Poketto.Application.Accounts;

namespace Poketto.Application.GraphQL.Accounts
{
    public record AccountPayload
    {
        public AccountDto? Account { get; set; }
        public string? Error { get; set; }
    }

    public record AccountListPayload
    {
        public IQueryable<AccountDto>? Accounts { get; set; }
        public string? Error { get; set; }
    }
}
