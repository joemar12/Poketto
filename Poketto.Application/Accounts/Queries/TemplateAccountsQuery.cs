using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Poketto.Application.Common.Interfaces;

namespace Poketto.Application.Accounts.Queries
{
    public record TemplateAccountsQuery : IRequest<IQueryable<AccountDto>> { }

    public class TemplateAccountsQueryHandler : IRequestHandler<TemplateAccountsQuery, IQueryable<AccountDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public TemplateAccountsQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public Task<IQueryable<AccountDto>> Handle(TemplateAccountsQuery request, CancellationToken cancellationToken)
        {
            var ownerId = "seeder";
            var accounts = _context.Accounts
                .AsNoTrackingWithIdentityResolution()
                .Where(x => x.OwnerUserId == ownerId);

            var result = _mapper.Map<IEnumerable<AccountDto>>(accounts.ToList())
                .Where(x => x.ParentAccountId == Guid.Empty)
                .AsQueryable();

            return Task.FromResult(result);
        }
    }
}