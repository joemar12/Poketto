using Poketto.Domain.Entities;
using Poketto.Domain.Enums;

namespace Poketto.Infrastructure.Persistence.Seeders;

public class LiabilityAccountsSeeder : IDataSeeder
{
    private readonly ApplicationDbContext _context;

    public LiabilityAccountsSeeder(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task SeedAsync()
    {
        if (_context != null && !_context.Accounts.Any(x => x.AccountType == AccountType.Liability))
        {
            var seedAccounts = new List<Account>()
            {
                new Account()
                {
                    Name = "Liabilities",
                    Description  = "Liabilities",
                    AccountType = AccountType.Liability,
                    OwnerUserId = "seeder",
                    IsPlaceholder = true,
                    ChildAccounts = new List<Account>()
                    {
                        new Account()
                        {
                            Name = "Loans",
                            Description  = "Loans",
                            AccountType = AccountType.Liability,
                            OwnerUserId = "seeder",
                        },
                        new Account()
                        {
                            Name = "Credit Card",
                            Description  = "Credit Card",
                            AccountType = AccountType.Liability,
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
