using AutoMapper;
using MediatR;
using Poketto.Application.Common.Interfaces;
using Poketto.Application.Common.Security;
using Poketto.Domain.Entities;
using Poketto.Domain.Enums;

namespace Poketto.Application.Accounts.Commands
{
    [Authorize(RequiredScopesConfigurationKey = "ChartOfAccountsReadWrite")]
    public class AddAccountCommand : IRequest<AccountDto>
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public AccountType AccountType { get; set; }
        public Guid ParentAccountId { get; set; }
        public bool IsPlaceholder { get; set; }
    }

    public class AddAccountCommandhandler : IRequestHandler<AddAccountCommand, AccountDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUserService;

        public AddAccountCommandhandler(IApplicationDbContext context, IMapper mapper, ICurrentUserService currentUserService)
        {
            _context = context;
            _mapper = mapper;
            _currentUserService = currentUserService;
        }

        public async Task<AccountDto> Handle(AddAccountCommand request, CancellationToken cancellationToken)
        {
            var ownerId = _currentUserService.GetCurrentUser();
            var newRecord = _mapper.Map<Account>(request);
            newRecord.OwnerUserId = ownerId ?? string.Empty;

            _context.Accounts.Add(newRecord);
            await _context.SaveChangesAsync(cancellationToken);

            var result = _mapper.Map<AccountDto>(newRecord);
            return result;
        }
    }
}