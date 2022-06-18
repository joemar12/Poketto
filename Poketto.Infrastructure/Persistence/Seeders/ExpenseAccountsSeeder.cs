using Poketto.Domain.Entities;
using Poketto.Domain.Enums;

namespace Poketto.Infrastructure.Persistence.Seeders
{
    public class ExpenseAccountsSeeder : IDataSeeder
    {
        private readonly ApplicationDbContext _context;

        public ExpenseAccountsSeeder(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task SeedAsync()
        {
            if (_context != null && !_context.Accounts.Any(x => x.AccountType == AccountType.Expense))
            {
                var seedAccounts = new List<Account>()
                {
                    new Account()
                    {
                        Name = "Expenses",
                        Description  = "Expenses",
                        AccountType = AccountType.Expense,
                        OwnerUserId = "seeder",
                        IsPlaceholder = true,
                        ChildAccounts = new List<Account>()
                        {
                            new Account()
                            {
                                Name = "Utilities",
                                Description  = "Utilities",
                                AccountType = AccountType.Expense,
                                OwnerUserId = "seeder",
                                IsPlaceholder = true,
                                ChildAccounts = new List<Account>()
                                {
                                    new Account()
                                    {
                                        Name = "Home Internet",
                                        Description  = "Home Internet",
                                        AccountType = AccountType.Expense,
                                        OwnerUserId = "seeder"
                                    },
                                    new Account()
                                    {
                                        Name = "Mobile Plan",
                                        Description  = "Mobile Plan",
                                        AccountType = AccountType.Expense,
                                        OwnerUserId = "seeder"
                                    },
                                }
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
