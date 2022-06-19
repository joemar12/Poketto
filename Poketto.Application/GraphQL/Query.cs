using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Poketto.Application.Common.Interfaces;
using Poketto.Application.Common.Mapping.Utils;
using Poketto.Application.Models;
using Poketto.Domain.Entities;

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
        public IQueryable<AccountDto> Accounts([Service] IApplicationDbContext context)
        {
            var result = context.Accounts
                .Where(x => x.ParentAccountId == null)
                .Select(MappingExtensions.GetAccountProjection(10))
                .OrderBy(x => x.Name);

            return result;
        }
    }
}
