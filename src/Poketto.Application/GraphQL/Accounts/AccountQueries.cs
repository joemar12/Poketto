using MediatR;
using Poketto.Application.Accounts;
using Poketto.Application.Accounts.Queries;

namespace Poketto.Application.GraphQL.Accounts;

[ExtendObjectType(OperationTypeNames.Query)]
public class AccountQueries
{
    [UseFiltering]
    public async Task<IQueryable<AccountDto>> GetTemplateAccounts([Service] ISender mediator)
    {
        var result = await mediator.Send(new TemplateAccountsQuery());

        return result;
    }

    [UseFiltering]
    public async Task<IQueryable<AccountDto>> GetUserAccounts([Service] ISender mediator)
    {
        var result = await mediator.Send(new AccountsQuery());

        return result;
    }
}