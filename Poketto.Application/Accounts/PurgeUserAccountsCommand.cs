using AutoMapper;
using MediatR;
using Poketto.Application.Common.Interfaces;
using Poketto.Application.Common.Security;
using Poketto.Domain.Entities;

namespace Poketto.Application.Accounts
{
    [Authorize(RequiredScopesConfigurationKey = "ApplicationScopes:ChartOfAccountsReadWrite")]
    public class PurgeUserAccountsCommand : IRequest<IQueryable<AccountDto>>
    { }

    public class PurgeUserAccountsCommandHandler : IRequestHandler<PurgeUserAccountsCommand, IQueryable<AccountDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUserService;

        public PurgeUserAccountsCommandHandler(IApplicationDbContext context, IMapper mapper, ICurrentUserService currentUserService)
        {
            _context = context;
            _mapper = mapper;
            _currentUserService = currentUserService;
        }

        public async Task<IQueryable<AccountDto>> Handle(PurgeUserAccountsCommand request, CancellationToken cancellationToken)
        {
            var ownerId = _currentUserService.GetCurrentUser();
            var userAccounts = _context.Accounts
                .Where(x => x.OwnerUserId == ownerId);

            foreach (var account in userAccounts.Where(x => (x.ChildAccounts == null ||
                                                             x.ChildAccounts.Count < 1) &&
                                                             !x.IsPlaceholder &&
                                                             x.JournalEntries.Count == 0))
            {
                _context.Accounts.Remove(account);
                if (account.ParentAccount != null)
                {
                    DeleteParentAccount(account.ParentAccount);
                }
            }

            await _context.SaveChangesAsync(new CancellationToken());

            var result = _mapper.Map<IEnumerable<AccountDto>>(userAccounts.ToList())
                .Where(x => x.ParentAccountId == Guid.Empty)
                .AsQueryable();

            return result;
        }

        private void DeleteParentAccount(Account account)
        {
            _context.Accounts.Remove(account);
            if (account.ParentAccount != null)
            {
                DeleteParentAccount(account.ParentAccount);
            }
        }
    }
}