using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Poketto.Application.Common.Interfaces;
using Poketto.Application.Common.Security;

namespace Poketto.Application.Transactions
{
    [Authorize(RequiredScopesConfigurationKey = "ApplicationScopes:TransactionsRead")]
    public record TransactionJournalsQuery : IRequest<IQueryable<TransactionJournalDto>> { }

    public class TransactionJournalsQueryHandler : IRequestHandler<TransactionJournalsQuery, IQueryable<TransactionJournalDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUserService;

        public TransactionJournalsQueryHandler(IApplicationDbContext context, ICurrentUserService currentUserService, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
            _currentUserService = currentUserService;
        }

        public Task<IQueryable<TransactionJournalDto>> Handle(TransactionJournalsQuery request, CancellationToken cancellationToken)
        {
            var ownerId = _currentUserService.GetCurrentUser();
            var result = _context.TransactionJournals
                .AsNoTracking()
                .Where(x => x.OwnerUserId == ownerId)
                .ProjectTo<TransactionJournalDto>(_mapper.ConfigurationProvider);

            return Task.FromResult(result);
        }
    }
}
