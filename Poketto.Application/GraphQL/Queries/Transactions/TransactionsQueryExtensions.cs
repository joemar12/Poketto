using AutoMapper;
using AutoMapper.QueryableExtensions;
using HotChocolate.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Poketto.Application.Common.Options;
using Poketto.Application.Common.Interfaces;
using Microsoft.AspNetCore.Http;

namespace Poketto.Application.GraphQL.Queries.Transactions
{
    [ExtendObjectType(OperationTypeNames.Query)]
    public class TransactionsQueryExtensions
    {
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUserService;
        private ApplicationScopes _scopes = new ApplicationScopes();

        public TransactionsQueryExtensions(IMapper mapper, ICurrentUserService currentUserService, IConfiguration config)
        {
            _mapper = mapper;
            _currentUserService = currentUserService;
            config.GetSection(ApplicationScopes.ConfigSectionName).Bind(_scopes);
        }

        [UseFiltering]
        [Authorize]
        [RequiredScope(RequiredScopesConfigurationKey = "ApplicationScopes:TransactionsRead")]
        public IQueryable<TransactionJournalDto> TransactionJournals([Service] IApplicationDbContext context,[Service] IHttpContextAccessor httpContextAccessor)
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
        [RequiredScope(RequiredScopesConfigurationKey = "ApplicationScopes:TransactionsRead")]
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
