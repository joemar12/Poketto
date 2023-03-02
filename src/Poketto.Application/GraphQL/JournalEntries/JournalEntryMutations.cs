using AutoMapper;
using MediatR;
using Poketto.Application.GraphQL.Errors;
using Poketto.Application.Transactions.Commands;

namespace Poketto.Application.GraphQL.JournalEntries
{
    [ExtendObjectType(OperationTypeNames.Mutation)]
    public class JournalEntryMutations
    {
        private readonly IMapper _mapper;

        public JournalEntryMutations(IMapper mapper)
        {
            _mapper = mapper;
        }

        [Error<AuthorizationError>]
        [Error<InternalServerError>]
        public async Task<JournalEntryPayload> AddJournalEntry(JournalEntryInput input, [Service] ISender mediator)
        {
            var command = _mapper.Map<AddJournalEntryCommand>(input);
            var result = await mediator.Send(command);

            return new JournalEntryPayload() { JournalEntry = result };
        }
    }
}
