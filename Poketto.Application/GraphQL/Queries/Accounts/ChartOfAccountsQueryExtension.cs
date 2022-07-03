using AutoMapper;
using HotChocolate.AspNetCore.Authorization;
using Poketto.Application.Common.Interfaces;

namespace Poketto.Application.GraphQL.Queries.Accounts
{
    [ExtendObjectType(OperationTypeNames.Query)]
    public class ChartOfAccountsQueryExtension
    {
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUserService;

        public ChartOfAccountsQueryExtension(IMapper mapper, ICurrentUserService currentUserService)
        {
            _mapper = mapper;
            _currentUserService = currentUserService;
        }

        [UseFiltering]
        public IQueryable<AccountDto> GetTemplateAccounts([Service] IApplicationDbContext context)
        {
            var ownerId = "seeder";
            var accounts = context.Accounts
                .Where(x => x.OwnerUserId == ownerId);

            var result = _mapper.Map<IEnumerable<AccountDto>>(accounts.ToList())
                .Where(x => x.ParentAccountId == Guid.Empty)
                .AsQueryable();

            return result;
        }

        [Authorize]
        [RequiredScope(RequiredScopesConfigurationKey = "ApplicationScopes:ChartOfAccountsRead")]
        [UseFiltering]
        public IQueryable<AccountDto> GetUserAccounts([Service] IApplicationDbContext context)
        {
            var ownerId = _currentUserService.GetCurrentUser();
            var accounts = context.Accounts
                .Where(x => x.OwnerUserId == ownerId);

            var result = _mapper.Map<IEnumerable<AccountDto>>(accounts.ToList())
                .Where(x => x.ParentAccountId == Guid.Empty)
                .AsQueryable();

            return result;
        }
    }
}
