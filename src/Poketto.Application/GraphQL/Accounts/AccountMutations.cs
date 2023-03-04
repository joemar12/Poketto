using AutoMapper;
using MediatR;
using Poketto.Application.Accounts.Commands;
using Poketto.Application.GraphQL.Errors;

namespace Poketto.Application.GraphQL.Accounts;

[ExtendObjectType(OperationTypeNames.Mutation)]
public class AccountMutations
{
    private readonly IMapper _mapper;

    public AccountMutations(IMapper mapper)
    {
        _mapper = mapper;
    }

    [Error<AuthorizationError>]
    [Error<InternalServerError>]
    public async Task<AccountListPayload> InitializeFromTemplate([Service] ISender mediator)
    {
        var result = await mediator.Send(new InitializeAccountsFromTemplateCommand());
        return new AccountListPayload() { Accounts = result };
    }

    [Error<AuthorizationError>]
    [Error<InternalServerError>]
    public async Task<AccountListPayload> PurgeAccounts([Service] ISender mediator)
    {
        var result = await mediator.Send(new PurgeAccountsCommand());
        return new AccountListPayload() { Accounts = result };
    }

    [Error<AuthorizationError>]
    [Error<InternalServerError>]
    public async Task<AccountPayload> AddAccount(AccountInput input, [Service] ISender mediator)
    {
        var command = _mapper.Map<AddAccountCommand>(input);
        var result = await mediator.Send(command);

        return new AccountPayload() { Account = result };
    }

    [Error<AuthorizationError>]
    [Error<NotFoundError>]
    [Error<InternalServerError>]
    public async Task<AccountPayload> UpdateAccount(AccountInput input, [Service] ISender mediator)
    {
        var command = _mapper.Map<AddAccountCommand>(input);
        var result = await mediator.Send(command);
        return new AccountPayload() { Account = result };
    }

    [Error<AuthorizationError>]
    [Error<NotFoundError>]
    [Error<InternalServerError>]
    public async Task<AccountPayload> DeleteAccount(Guid input, [Service] ISender mediator)
    {
        var command = new DeleteAccountCommand() { Id = input };
        var result = await mediator.Send(command);

        return new AccountPayload() { Account = result };
    }
}