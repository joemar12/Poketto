using Poketto.Domain.Entities;
using Poketto.Domain.Enums;

namespace Poketto.Infrastructure.Persistence.Seeders;

public class AssetAccountsSeeder : IDataSeeder
{
    private readonly ApplicationDbContext _context;

    public AssetAccountsSeeder(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task SeedAsync()
    {
        if (_context != null && !_context.Accounts.Any(x => x.AccountType == AccountType.Asset))
        {
            var seedAccounts = new List<Account>()
            {
                new Account()
                {
                    Name = "Assets",
                    Description  = "Assets",
                    AccountType = AccountType.Asset,
                    OwnerUserId = "seeder",
                    IsPlaceholder = true,
                    ChildAccounts = new List<Account>()
                    {
                        new Account()
                        {
                            Name = "Current Assets",
                            Description  = "Current Assets",
                            AccountType = AccountType.Asset,
                            OwnerUserId = "seeder",
                            IsPlaceholder = true,
                            ChildAccounts = new List<Account>()
                            {
                                new Account()
                                {
                                    Name = "Cash on Hand",
                                    Description  = "Cash on Hand",
                                    AccountType = AccountType.Asset,
                                    OwnerUserId = "seeder"
                                },
                                new Account()
                                {
                                    Name = "Savings Account",
                                    Description  = "Savings Account",
                                    AccountType = AccountType.Asset,
                                    OwnerUserId = "seeder"
                                }
                            }
                        },
                        new Account()
                        {
                            Name = "Fixed Assets",
                            Description  = "Fixed Assets",
                            AccountType = AccountType.Asset,
                            OwnerUserId = "seeder",
                            IsPlaceholder = true,
                            ChildAccounts = new List<Account>()
                            {
                                new Account()
                                {
                                    Name = "Other Assets",
                                    Description  = "Other Assets",
                                    AccountType = AccountType.Asset,
                                    OwnerUserId = "seeder"
                                },
                            }
                        },
                        new Account()
                        {
                            Name = "Investments",
                            Description  = "Investments",
                            AccountType = AccountType.Asset,
                            OwnerUserId = "seeder",
                            IsPlaceholder = true,
                            ChildAccounts = new List<Account>()
                            {
                                new Account()
                                {
                                    Name = "Brokerage Accounts",
                                    Description  = "Brokerage Accounts",
                                    AccountType = AccountType.Asset,
                                    OwnerUserId = "seeder",
                                    IsPlaceholder = true,
                                    ChildAccounts = new List<Account>()
                                    {
                                        new Account()
                                        {
                                            Name = "Stocks",
                                            Description  = "Stocks",
                                            AccountType = AccountType.Asset,
                                            OwnerUserId = "seeder"
                                        },
                                    }
                                },
                            }
                        }
                    }
                },
            };
            _context.Accounts.AddRange(seedAccounts);
            await _context.SaveChangesAsync();
        }
    }
}
