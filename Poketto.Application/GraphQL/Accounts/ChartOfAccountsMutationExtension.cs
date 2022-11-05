using AutoMapper;
using MediatR;
using Poketto.Application.Accounts;
using Poketto.Application.Common.Interfaces;
using Poketto.Domain.Entities;

namespace Poketto.Application.GraphQL.Accounts
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

        public async Task<AccountListPayload> InitializeFromTemplate([Service] IMediator _mediator)
        {
            try
            {
                var result = await _mediator.Send(new InitializeAccountsFromTemplateCommand());
                return new AccountListPayload() { Accounts = result };
            }
            catch (Exception)
            {
                return new AccountListPayload() { Error = "Accounts initialization failed." };
            }
        }

        public async Task<AccountListPayload> PurgeAccounts([Service] IMediator _mediator)
        {
            try
            {
                var result = await _mediator.Send(new PurgeAccountsCommand());
                return new AccountListPayload() { Accounts = result };
            }
            catch (Exception)
            {
                return new AccountListPayload() { Error = "Purge operation failed." };
            }
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
}