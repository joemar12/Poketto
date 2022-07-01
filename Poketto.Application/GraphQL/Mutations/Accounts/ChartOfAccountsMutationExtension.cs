using AutoMapper;
using HotChocolate.AspNetCore.Authorization;
using Poketto.Application.Common.Interfaces;
using Poketto.Application.GraphQL.Queries.Accounts;
using Poketto.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poketto.Application.GraphQL.Mutations.Accounts
{
    [ExtendObjectType(OperationTypeNames.Mutation)]
    public class ChartOfAccountsMutationExtension
    {
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUserService;

        public ChartOfAccountsMutationExtension(IMapper mapper, ICurrentUserService currentUserService)
        {
            _mapper = mapper;
            _currentUserService = currentUserService;
        }

        [Authorize]
        [RequiredScope(RequiredScopesConfigurationKey = "ApplicationScopes:ChartOfAccountsReadWrite")]
        public async Task<IQueryable<AccountDto>> InitializeFromTemplate([Service] IApplicationDbContext context)
        {
            var ownerId = _currentUserService.GetCurrentUser();
            var templateAccounts = context.Accounts
                .Where(x => x.OwnerUserId == "seeder");

            var userAccounts = templateAccounts
                .ToList()
                .Select(x => new Account() 
                { 
                    Name = x.Name, 
                    AccountType = x.AccountType, 
                    IsPlaceholder = x.IsPlaceholder, 
                    ParentAccountId = x.ParentAccountId, 
                    OwnerUserId = ownerId ?? String.Empty
                });

            context.Accounts.AddRange(userAccounts);
            await context.SaveChangesAsync(new CancellationToken());

            var result = _mapper.Map<IEnumerable<AccountDto>>(userAccounts.ToList())
                .Where(x => x.ParentAccountId == Guid.Empty)
                .AsQueryable();

            return result;
        }

        [Authorize]
        [RequiredScope(RequiredScopesConfigurationKey = "ApplicationScopes:ChartOfAccountsReadWrite")]
        public async Task<IQueryable<AccountDto>> PurgeAccounts([Service] IApplicationDbContext context)
        {
            var ownerId = _currentUserService.GetCurrentUser();
            var userAccounts = context.Accounts
                .Where(x => x.OwnerUserId == ownerId);

            context.Accounts.RemoveRange(userAccounts);
            await context.SaveChangesAsync(new CancellationToken());

            var result = _mapper.Map<IEnumerable<AccountDto>>(userAccounts.ToList())
                .Where(x => x.ParentAccountId == Guid.Empty)
                .AsQueryable();

            return result;
        }
    }
}
