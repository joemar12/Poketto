using MediatR;
using Poketto.Application.Transactions;

namespace Poketto.Application.GraphQL.Transactions
{
    [ExtendObjectType(OperationTypeNames.Query)]
    public class TransactionsQueryExtension
    {
        [UseProjection]
        [UseFiltering]
        [UseSorting]
        public async Task<IQueryable<TransactionJournalDto>> GetTransactionJournals([Service] IMediator mediator)
        {
            var result = await mediator.Send(new TransactionJournalsQuery());

            return result;
        }

        [UseProjection]
        [UseFiltering]
        [UseSorting]
        public async Task<IQueryable<TransactionGroupDto>> TransactionGroups([Service] IMediator mediator)
        {
            var result = await mediator.Send(new TransactionGroupsQuery());

            return result;
        }
    }
}