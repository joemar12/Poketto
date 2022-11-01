using AutoMapper;
using MediatR;
using Poketto.Application.Common.Interfaces;
using Poketto.Application.Common.Security;
using Poketto.Domain.Entities;

namespace Poketto.Application.Accounts
{
    [Authorize(RequiredScopesConfigurationKey = "ApplicationScopes:ChartOfAccountsReadWrite")]
    public record InitializeUserAccountsFromTemplateCommand : IRequest<IQueryable<AccountDto>> { }

    public class InitializeUserAccountsFromTemplateCommandHandler : IRequestHandler<InitializeUserAccountsFromTemplateCommand, IQueryable<AccountDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUserService;

        public InitializeUserAccountsFromTemplateCommandHandler(IApplicationDbContext context, IMapper mapper, ICurrentUserService currentUserService)
        {
            _context = context;
            _mapper = mapper;
            _currentUserService = currentUserService;
        }

        public async Task<IQueryable<AccountDto>> Handle(InitializeUserAccountsFromTemplateCommand request, CancellationToken cancellationToken)
        {
            var ownerId = _currentUserService.GetCurrentUser();
            var templateAccounts = _context.Accounts
                .Where(x => x.OwnerUserId == "seeder");

            var userAccounts = new List<Account>();

            foreach (var templateAccount in templateAccounts.Where(x => x.ParentAccountId == null))
            {
                var userAccount = new Account()
                {
                    Name = templateAccount.Name,
                    Description = templateAccount.Description,
                    AccountType = templateAccount.AccountType,
                    IsPlaceholder = templateAccount.IsPlaceholder,
                    OwnerUserId = ownerId ?? string.Empty,
                    ChildAccounts = templateAccount.ChildAccounts != null && templateAccount.ChildAccounts.Count > 0 ?
                        MapAccountsFromTemplate(templateAccount.ChildAccounts, ownerId ?? string.Empty) :
                        null
                };
                userAccounts.Add(userAccount);
            }

            _context.Accounts.AddRange(userAccounts);
            await _context.SaveChangesAsync(new CancellationToken());

            var result = _mapper.Map<IEnumerable<AccountDto>>(userAccounts)
                .Where(x => x.ParentAccountId == Guid.Empty)
                .AsQueryable();

            return result;
        }

        private List<Account> MapAccountsFromTemplate(IEnumerable<Account> templateAccounts, string ownerId)
        {
            var userAccounts = new List<Account>();
            foreach (var templateAccount in templateAccounts)
            {
                var userAccount = new Account()
                {
                    Name = templateAccount.Name,
                    Description = templateAccount.Description,
                    AccountType = templateAccount.AccountType,
                    IsPlaceholder = templateAccount.IsPlaceholder,
                    OwnerUserId = ownerId ?? string.Empty,
                    ChildAccounts = templateAccount.ChildAccounts != null && templateAccount.ChildAccounts.Count > 0 ?
                        MapAccountsFromTemplate(templateAccount.ChildAccounts, ownerId ?? string.Empty) :
                        null
                };
                userAccounts.Add(userAccount);
            }
            return userAccounts;
        }
    }
}