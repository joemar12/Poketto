using AutoMapper;
using MediatR;
using Poketto.Application.Common.Interfaces;
using Poketto.Domain.Entities;
using Poketto.Domain.Enums;

namespace Poketto.Application.Transactions.Commands;

public class AddJournalEntryCommand : IRequest<JournalEntryDto>
{
    public string RefCode { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime JournalEntryDate { get; set; }
    public JournalEntryStatus Status { get; set; }
    public IList<JournalEntryItemDto> JournalEntryItems { get; set; } = new List<JournalEntryItemDto>();
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
    public async Task<JournalEntryDto> Handle(AddJournalEntryCommand request, CancellationToken cancellationToken)
    {
        var ownerId = _currentUserService.GetCurrentUser();
        var newRecord = _mapper.Map<JournalEntry>(request);
        newRecord.OwnerUserId = ownerId ?? string.Empty;

        _context.JournalEntries.Add(newRecord);
        await _context.SaveChangesAsync(cancellationToken);

        var result = _mapper.Map<JournalEntryDto>(newRecord);
        return result;
    }
}
