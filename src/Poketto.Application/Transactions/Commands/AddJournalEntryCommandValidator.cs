using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Poketto.Application.Common.Interfaces;

namespace Poketto.Application.Transactions.Commands;

public class AddJournalEntryCommandValidator : AbstractValidator<AddJournalEntryCommand>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly ICurrentUserService _currentUserService;
    public AddJournalEntryCommandValidator(IApplicationDbContext dbContext, ICurrentUserService currentUserService)
    {
        _dbContext = dbContext;
        _currentUserService = currentUserService;

        RuleFor(x => x.RefCode)
            .NotEmpty().WithMessage("RefCode must not be empty.")
            .MaximumLength(100).WithMessage("RefCode must not exceed 100 characters.")
            .MustAsync(BeUniqueRefCode).WithMessage("RefCode must be unique.");

        RuleFor(x => x.Description)
            .MaximumLength(500).WithMessage("Description must not exceed 500 characters.");
    }

    public async Task<bool> BeUniqueRefCode(string refCode, CancellationToken cancellationToken)
    {
        var currentUser = _currentUserService.GetCurrentUser();
        return await _dbContext.JournalEntries
            .Where(x => x.OwnerUserId == currentUser)
            .AllAsync(x => x.RefCode != refCode);
    }
}
