using Poketto.Domain.Entities;
using Poketto.Domain.Enums;

namespace Poketto.Infrastructure.Persistence.Seeders;

public class EquityAccountsSeeder : IDataSeeder
{
    private readonly ApplicationDbContext _context;

    public EquityAccountsSeeder(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task SeedAsync()
    {
        if (_context != null && !_context.Accounts.Any(x => x.AccountType == AccountType.Equity))
        {
            var seedAccounts = new List<Account>()
            {
                new Account()
                {
                    Name = "Equity",
                    Description  = "Equity",
                    AccountType = AccountType.Equity,
                    OwnerUserId = "seeder",
                    IsPlaceholder = true,
                    ChildAccounts = new List<Account>()
                    {
                        new Account()
                        {
                            Name = "Opening Balances",
                            Description  = "Opening Balances",
                            AccountType = AccountType.Equity,
                            OwnerUserId = "seeder",
                        }
                    }
                },
            };
            _context.Accounts.AddRange(seedAccounts);
            await _context.SaveChangesAsync();
        }
    }
}
