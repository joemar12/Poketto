using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Poketto.Application.Common.Interfaces;

namespace Poketto.Application.Accounts.Commands
{
    public class AddAccountCommandValidator : AbstractValidator<AddAccountCommand>
    {
        private readonly IApplicationDbContext _dbContext;
        private readonly ICurrentUserService _currentUserService;
        public AddAccountCommandValidator(IApplicationDbContext dbContext, ICurrentUserService currentUserService)
        {
            _dbContext = dbContext;
            _currentUserService = currentUserService;

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Account name is required.")
                .MaximumLength(200).WithMessage("Account name must not exceed 200 characters.")
                .MustAsync(BeUniqueName).WithMessage("Account name must be unique.");

            RuleFor(x => x.Description)
                .MaximumLength(500).WithMessage("Description must not exceed 500 characters.");
        }

        public async Task<bool> BeUniqueName(string name, CancellationToken cancellationToken)
        {
            var currentUser = _currentUserService.GetCurrentUser();
            return await _dbContext.Accounts
                .Where(x => x.OwnerUserId == currentUser)
                .AllAsync(x => x.Name != name, cancellationToken);
        }
    }
}