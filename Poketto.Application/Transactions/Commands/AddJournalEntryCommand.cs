using AutoMapper;
using MediatR;
using Poketto.Application.Common.Interfaces;

namespace Poketto.Application.Transactions.Commands
{
    public class AddJournalEntryCommand : IRequest<JournalEntryDto>
    {

    }

    public class AddJournalEntryCommandHandler : IRequestHandler<AddJournalEntryCommand, JournalEntryDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUserService;

        public AddJournalEntryCommandHandler(IApplicationDbContext context, IMapper mapper, ICurrentUserService currentUserService)
        {
            _context = context;
            _mapper = mapper;
            _currentUserService = currentUserService;
        }
        public Task<JournalEntryDto> Handle(AddJournalEntryCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
