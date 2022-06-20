using AutoMapper;
using Poketto.Application.Common.Interfaces;
using Poketto.Application.Models;

namespace Poketto.Application.GraphQL
{
    public class Query
    {
        private readonly IMapper _mapper;

        public Query(IMapper mapper)
        {
            _mapper = mapper;
        }

        [UseFiltering]
        public IQueryable<AccountDto> TemplateAccounts([Service] IApplicationDbContext context)
        {
            var ownerId = "seeder";
            var accounts = context.Accounts
                .Where(x => x.OwnerUserId == ownerId);

            var result = _mapper.Map<IEnumerable<AccountDto>>(accounts.ToList())
                .Where(x => x.ParentAccountId == Guid.Empty)
                .AsQueryable();

            return result;
        }
    }
}
