using AutoMapper;
using Poketto.Application.Common.Interfaces;
using Poketto.Application.GraphQL.Queries.Accounts;

namespace Poketto.Application.GraphQL.Queries.Transactions
{
    [ExtendObjectType(OperationTypeNames.Query)]
    public class TransactionJournalExtensions
    {
        private readonly IMapper _mapper;

        public TransactionJournalExtensions(IMapper mapper)
        {
            _mapper = mapper;
        }

        [UseFiltering]
        public IQueryable<TransactionJournalDto> TransactionJournals([Service] IApplicationDbContext context)
        {
            var ownerId = "seeder";
            var transactionJournals = context.TransactionJournals
                .Where(x => x.OwnerUserId == ownerId);

            var result = _mapper.Map<IEnumerable<TransactionJournalDto>>(transactionJournals.ToList())
                .AsQueryable();

            return result;
        }
    }
}
