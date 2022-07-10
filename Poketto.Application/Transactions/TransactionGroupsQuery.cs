using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Poketto.Application.Common.Interfaces;
using Poketto.Application.Common.Security;

namespace Poketto.Application.Transactions
{
    [Authorize(RequiredScopesConfigurationKey = "ApplicationScopes:TransactionsRead")]
    public record TransactionGroupsQuery : IRequest<IQueryable<TransactionGroupDto>> { }

    public class TransactionGroupsQueryHandler : IRequestHandler<TransactionGroupsQuery, IQueryable<TransactionGroupDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUserService;

        public TransactionGroupsQueryHandler(IApplicationDbContext context, ICurrentUserService currentUserService, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
            _currentUserService = currentUserService;
        }

        public Task<IQueryable<TransactionGroupDto>> Handle(TransactionGroupsQuery request, CancellationToken cancellationToken)
        {
            var ownerId = _currentUserService.GetCurrentUser();
            var result = _context.TransactionGroups
                .AsNoTracking()
                .Where(x => x.OwnerUserId == ownerId)
                .ProjectTo<TransactionGroupDto>(_mapper.ConfigurationProvider);

            return Task.FromResult(result);
        }
    }
}
