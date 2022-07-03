using AutoMapper;
using HotChocolate.AspNetCore.Authorization;
using Poketto.Application.Common.Interfaces;
using Poketto.Application.GraphQL.Queries.Accounts;
using Poketto.Domain.Entities;
using Poketto.Domain.Enums;

namespace Poketto.Application.GraphQL.Mutations.Accounts
{
    [ExtendObjectType(OperationTypeNames.Mutation)]
    [Authorize]
    [RequiredScope(RequiredScopesConfigurationKey = "ApplicationScopes:ChartOfAccountsReadWrite")]
    public class ChartOfAccountsMutationExtension
    {
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUserService;

        public ChartOfAccountsMutationExtension(IMapper mapper, ICurrentUserService currentUserService)
        {
            _mapper = mapper;
            _currentUserService = currentUserService;
        }

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

        public async Task<AccountPayload> AddAccount(AccountInput input, [Service] IApplicationDbContext context)
        {
            var ownerId = _currentUserService.GetCurrentUser();
            var accountToAdd = _mapper.Map<Account>(input);
            accountToAdd.OwnerUserId = ownerId ?? string.Empty;

            context.Accounts.Add(accountToAdd);
            await context.SaveChangesAsync(new CancellationToken());

            var result = _mapper.Map<AccountDto>(accountToAdd);

            return new AccountPayload() { Account = result };
        }

        public async Task<AccountPayload> UpdateAccount(AccountInput input, [Service] IApplicationDbContext context)
        {
            var ownerId = _currentUserService.GetCurrentUser();
            var accountToUpdate = context.Accounts.Where(x => x.Id == input.Id && x.OwnerUserId == ownerId).FirstOrDefault();
            if (accountToUpdate is not null)
            {
                _mapper.Map(input, accountToUpdate);
                await context.SaveChangesAsync(new CancellationToken());
                var result = _mapper.Map<AccountDto>(accountToUpdate);
                return new AccountPayload() { Account = result };
            }
            else
            {
                return new AccountPayload() { Error = "The account being modified was not found." };
            }
        }

        public async Task<AccountPayload> DeleteAccount(Guid input, [Service] IApplicationDbContext context)
        {
            var ownerId = _currentUserService.GetCurrentUser();
            var accountToDelete = context.Accounts.Where(x => x.Id == input && x.OwnerUserId == ownerId).FirstOrDefault();
            if (accountToDelete is not null)
            {
                context.Accounts.Remove(accountToDelete);
                await context.SaveChangesAsync(new CancellationToken());
                var result = _mapper.Map<AccountDto>(accountToDelete);
                return new AccountPayload() { Account = result };
            }
            else
            {
                return new AccountPayload() { Error = "The account being deleted was not found." };
            }
        }
    }

    public record AccountInput
    {
        public Guid? Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public AccountType AccountType { get; set; }
        public Guid? ParentAccountId { get; set; }
        public bool IsPlaceholder { get; set; }
    }

    public record AccountPayload
    {
        public AccountDto? Account { get; set; }
        public string? Error { get; set; }
    }
}
