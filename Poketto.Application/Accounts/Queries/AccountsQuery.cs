using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Poketto.Application.Common.Interfaces;
using Poketto.Application.Common.Security;

namespace Poketto.Application.Accounts.Queries
{
    [Authorize(RequiredScopesConfigurationKey = "ApplicationScopes:ChartOfAccountsRead")]
    public record AccountsQuery : IRequest<IQueryable<AccountDto>> { }

    public class AccountsQueryHandler : IRequestHandler<AccountsQuery, IQueryable<AccountDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUserService;

        public AccountsQueryHandler(IApplicationDbContext context, ICurrentUserService currentUserService, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
            _currentUserService = currentUserService;
        }

        public Task<IQueryable<AccountDto>> Handle(AccountsQuery request, CancellationToken cancellationToken)
        {
            var ownerId = _currentUserService.GetCurrentUser();
            var accounts = _context.Accounts
                .AsNoTrackingWithIdentityResolution()
                .Where(x => x.OwnerUserId == ownerId);

            var result = _mapper.Map<IEnumerable<AccountDto>>(accounts.ToList())
                .Where(x => x.ParentAccountId == Guid.Empty)
                .AsQueryable();

            return Task.FromResult(result);
        }
    }
}