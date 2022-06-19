using Poketto.Application.Models;
using Poketto.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Poketto.Application.Common.Mapping.Utils
{
    public static class MappingExtensions
    {
        public static Expression<Func<Account, AccountDto>> GetAccountProjection(int maxDepth, int currentDepth = 0)
        {
            currentDepth++;
            Expression<Func<Account, AccountDto>> result = account => new AccountDto
            {
                Name = account.Name,
                Description = account.Description,
                AccountType = account.AccountType,
                IsPlaceholder = account.IsPlaceholder,
                ChildAccounts = currentDepth == maxDepth
                    ? new List<AccountDto>()
                    : account.ChildAccounts
                        .AsQueryable()
                        .Select(GetAccountProjection(maxDepth, currentDepth))
                        .OrderBy(x => x.Name)
                        .ToList()

            };
            return result;
        }
    }
}
