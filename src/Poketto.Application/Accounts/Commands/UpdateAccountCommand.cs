using AutoMapper;
using MediatR;
using Poketto.Application.Common.Exceptions;
using Poketto.Application.Common.Interfaces;
using Poketto.Application.Common.Security;
using Poketto.Domain.Enums;

namespace Poketto.Application.Accounts.Commands;

[Authorize(RequiredScopesConfigurationKey = "ChartOfAccountsReadWrite")]
public class UpdateAccountCommand : IRequest<AccountDto>
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public AccountType AccountType { get; set; }
    public Guid ParentAccountId { get; set; }
    public bool IsPlaceholder { get; set; }
}

public class UpdateAccountCommandhandler : IRequestHandler<UpdateAccountCommand, AccountDto>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly ICurrentUserService _currentUserService;

    public UpdateAccountCommandhandler(IApplicationDbContext context, IMapper mapper, ICurrentUserService currentUserService)
    {
        _context = context;
        _mapper = mapper;
        _currentUserService = currentUserService;
    }

    public async Task<AccountDto> Handle(UpdateAccountCommand request, CancellationToken cancellationToken)
    {
        var ownerId = _currentUserService.GetCurrentUser();
        var accountToUpdate = _context.Accounts.Where(x => x.Id == request.Id && x.OwnerUserId == ownerId).FirstOrDefault();
        if (accountToUpdate is not null)
        {
            _mapper.Map(request, accountToUpdate);
            await _context.SaveChangesAsync(cancellationToken);
            var result = _mapper.Map<AccountDto>(accountToUpdate);
            return result;
        }
        else
        {
            throw new NotFoundException("Account to update was not found");
        }
    }
}
