using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Poketto.Application.Common.Interfaces;
using Poketto.Application.Common.Security;

namespace Poketto.Application.Transactions.Queries
{
    [Authorize(RequiredScopesConfigurationKey = "ApplicationScopes:TransactionsRead")]
    public record TransactionJournalsQuery : IRequest<IQueryable<JournalEntryDto>> { }

    public class TransactionJournalsQueryHandler : IRequestHandler<TransactionJournalsQuery, IQueryable<JournalEntryDto>>
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

        public Task<IQueryable<JournalEntryDto>> Handle(TransactionJournalsQuery request, CancellationToken cancellationToken)
        {
            var ownerId = _currentUserService.GetCurrentUser();
            var result = _context.JournalEntries
                .AsNoTracking()
                .Where(x => x.OwnerUserId == ownerId)
                .ProjectTo<JournalEntryDto>(_mapper.ConfigurationProvider);

            return Task.FromResult(result);
        }
    }
}