using MediatR;
using Poketto.Application.Accounts;

namespace Poketto.Application.GraphQL.Accounts
{
    [ExtendObjectType(OperationTypeNames.Query)]
    public class ChartOfAccountsQueryExtension
    {
        [UseFiltering]
        public async Task<IQueryable<AccountDto>> GetTemplateAccounts([Service] IMediator _mediator)
        {
            var result = await _mediator.Send(new TemplateAccountsQuery());

            return result;
        }

        [UseFiltering]
        public async Task<IQueryable<AccountDto>> GetUserAccounts([Service] IMediator _mediator)
        {
            var result = await _mediator.Send(new AccountsQuery());

            return result;
        }
    }
}