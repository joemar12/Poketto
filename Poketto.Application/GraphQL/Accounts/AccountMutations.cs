using AutoMapper;
using MediatR;
using Poketto.Application.Accounts.Commands;
using Poketto.Application.GraphQL.Errors;

namespace Poketto.Application.GraphQL.Accounts
{
    [ExtendObjectType(OperationTypeNames.Mutation)]
    public class AccountMutations
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public AccountMutations(IMapper mapper, IMediator mediator)
        {
            _mapper = mapper;
            _mediator = mediator;
        }

        [Error<AuthorizationError>]
        [Error<InternalServerError>]
        public async Task<AccountListPayload> InitializeFromTemplate([Service] IMediator _mediator)
        {
            var result = await _mediator.Send(new InitializeAccountsFromTemplateCommand());
            return new AccountListPayload() { Accounts = result };
        }

        [Error<AuthorizationError>]
        [Error<InternalServerError>]
        public async Task<AccountListPayload> PurgeAccounts([Service] IMediator _mediator)
        {
            var result = await _mediator.Send(new PurgeAccountsCommand());
            return new AccountListPayload() { Accounts = result };
        }

        [Error<AuthorizationError>]
        [Error<InternalServerError>]
        public async Task<AccountPayload> AddAccount(AccountInput input)
        {
            var command = _mapper.Map<AddAccountCommand>(input);
            var result = await _mediator.Send(command);

            return new AccountPayload() { Account = result };
        }

        [Error<AuthorizationError>]
        [Error<NotFoundError>]
        [Error<InternalServerError>]
        public async Task<AccountPayload> UpdateAccount(AccountInput input)
        {
            var command = _mapper.Map<AddAccountCommand>(input);
            var result = await _mediator.Send(command);
            return new AccountPayload() { Account = result };
        }

        [Error<AuthorizationError>]
        [Error<NotFoundError>]
        [Error<InternalServerError>]
        public async Task<AccountPayload> DeleteAccount(Guid input)
        {
            var command = new DeleteAccountCommand() { Id = input };
            var result = await _mediator.Send(command);

            return new AccountPayload() { Account = result };
        }
    }
}