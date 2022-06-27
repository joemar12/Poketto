using AutoMapper;
using HotChocolate.AspNetCore.Authorization;
using Poketto.Application.Common.Interfaces;

namespace Poketto.Application.GraphQL.Queries.Accounts
{
    [ExtendObjectType(OperationTypeNames.Query)]
    public class ChartOfAccountsQueryExtensions
    {
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUserService;

        public ChartOfAccountsQueryExtensions(IMapper mapper, ICurrentUserService currentUserService)
        {
            _mapper = mapper;
            _currentUserService = currentUserService;
        }

        [UseFiltering]
        public IQueryable<AccountDto> TemplateAccounts([Service] IApplicationDbContext context)
        {
            var ownerId = "seeder";
            var accounts = context.Accounts
                .Where(x => x.OwnerUserId == ownerId);

            var result = _mapper.Map<IEnumerable<AccountDto>>(accounts.ToList())
                .Where(x => x.ParentAccountId == Guid.Empty)
                .AsQueryable();

            return result;
        }

        [UseFiltering]
        [Authorize]
        [RequiredScope(RequiredScopesConfigurationKey = "ApplicationScopes:ChartOfAccountsRead")]
        public IQueryable<AccountDto> UserChartOfAccounts([Service] IApplicationDbContext context)
        {
            var ownerId = _currentUserService.GetCurrentUser();
            var scopes = _currentUserService.GetCurrentUserScopes();
            var accounts = context.Accounts
                .Where(x => x.OwnerUserId == ownerId);

            var result = _mapper.Map<IEnumerable<AccountDto>>(accounts.ToList())
                .Where(x => x.ParentAccountId == Guid.Empty)
                .AsQueryable();

            return result;
        }
    }
}
