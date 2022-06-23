using AutoMapper;
using AutoMapper.QueryableExtensions;
using HotChocolate.AspNetCore.Authorization;
using Poketto.Application.Common.Interfaces;

namespace Poketto.Application.GraphQL.Queries.Transactions
{
    [ExtendObjectType(OperationTypeNames.Query)]
    public class TransactionJournalExtensions
    {
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUserService;

        public TransactionJournalExtensions(IMapper mapper, ICurrentUserService currentUserService)
        {
            _mapper = mapper;
            _currentUserService = currentUserService;
        }

        [UseFiltering]
        [Authorize]
        public IQueryable<TransactionJournalDto> TransactionJournals([Service] IApplicationDbContext context)
        {
            var currentUser = _currentUserService.GetCurrentUser();
            var transactionJournals = context.TransactionJournals
                .Where(x => x.OwnerUserId == currentUser);

            var result = _mapper.Map<IEnumerable<TransactionJournalDto>>(transactionJournals.ToList())
                .AsQueryable();

            return result;
        }

        [UseFiltering]
        [Authorize]
        public IQueryable<TransactionGroupDto> TransactionGroups([Service] IApplicationDbContext context)
        {
            var currentUser = _currentUserService.GetCurrentUser();
            var transactionGroups = context.TransactionGroups
                .Where(x => x.OwnerUserId == currentUser)
                .ProjectTo<TransactionGroupDto>(_mapper.ConfigurationProvider);

            return transactionGroups;
        }

    }
}
