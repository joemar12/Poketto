using MediatR;
using Poketto.Application.Transactions;
using Poketto.Application.Transactions.Queries;

namespace Poketto.Application.GraphQL.JournalEntries
{
    [ExtendObjectType(OperationTypeNames.Query)]
    public class JournalEntryQueries
    {
        [UseProjection]
        [UseFiltering]
        [UseSorting]
        public async Task<IQueryable<JournalEntryDto>> GetJournalEntries([Service] ISender mediator)
        {
            var result = await mediator.Send(new TransactionJournalsQuery());
            return result;
        }
    }
}