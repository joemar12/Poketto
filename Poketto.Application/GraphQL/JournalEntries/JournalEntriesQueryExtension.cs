using MediatR;
using Poketto.Application.Transactions;
using Poketto.Application.Transactions.Queries;

namespace Poketto.Application.GraphQL.JournalEntries
{
    [ExtendObjectType(OperationTypeNames.Query)]
    public class JournalEntriesQueryExtension
    {
        [UseProjection]
        [UseFiltering]
        [UseSorting]
        public async Task<IQueryable<JournalEntryDto>> GetTransactionJournals([Service] IMediator mediator)
        {
            var result = await mediator.Send(new TransactionJournalsQuery());

            return result;
        }
    }
}