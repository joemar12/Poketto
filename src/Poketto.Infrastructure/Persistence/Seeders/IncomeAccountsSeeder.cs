using Poketto.Domain.Entities;
using Poketto.Domain.Enums;

namespace Poketto.Infrastructure.Persistence.Seeders
{
    public class IncomeAccountsSeeder : IDataSeeder
    {
        private readonly ApplicationDbContext _context;

        public IncomeAccountsSeeder(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task SeedAsync()
        {
            if (_context != null && !_context.Accounts.Any(x => x.AccountType == AccountType.Revenue))
            {
                var seedAccounts = new List<Account>()
                {
                    new Account()
                    {
                        Name = "Income",
                        Description  = "Income",
                        AccountType = AccountType.Revenue,
                        OwnerUserId = "seeder",
                        IsPlaceholder = true,
                        ChildAccounts = new List<Account>()
                        {
                            new Account()
                            {
                                Name = "Salary",
                                Description  = "Salary",
                                AccountType = AccountType.Revenue,
                                OwnerUserId = "seeder",
                            },
                            new Account()
                            {
                                Name = "Bonus",
                                Description  = "Bonus",
                                AccountType = AccountType.Revenue,
                                OwnerUserId = "seeder"
                            },
                        }
                    },
                };
                _context.Accounts.AddRange(seedAccounts);
                await _context.SaveChangesAsync();
            }
        }
    }
}
