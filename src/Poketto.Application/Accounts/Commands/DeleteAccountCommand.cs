using AutoMapper;
using MediatR;
using Poketto.Application.Common.Exceptions;
using Poketto.Application.Common.Interfaces;
using Poketto.Application.Common.Security;

namespace Poketto.Application.Accounts.Commands
{
    [Authorize(RequiredScopesConfigurationKey = "ApplicationScopes:ChartOfAccountsReadWrite")]
    public class DeleteAccountCommand : IRequest<AccountDto>
    {
        public Guid Id { get; set; }
    }

    public class DeleteAccountCommandhandler : IRequestHandler<DeleteAccountCommand, AccountDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUserService;

        public DeleteAccountCommandhandler(IApplicationDbContext context, IMapper mapper, ICurrentUserService currentUserService)
        {
            _context = context;
            _mapper = mapper;
            _currentUserService = currentUserService;
        }

        public async Task<AccountDto> Handle(DeleteAccountCommand request, CancellationToken cancellationToken)
        {
            var ownerId = _currentUserService.GetCurrentUser();
            var record = _context.Accounts
                .Where(x => x.OwnerUserId == ownerId && x.Id == request.Id)
                .FirstOrDefault();
            if (record != null)
            {
                _context.Accounts.Remove(record);
                await _context.SaveChangesAsync(cancellationToken);

                var result = _mapper.Map<AccountDto>(record);
                return result;
            }
            else
            {
                throw new NotFoundException("Account to delete was not found");
            }
        }
    }
}
