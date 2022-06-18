using Microsoft.EntityFrameworkCore;
using Poketto.Domain.Entities;

namespace Poketto.Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<Account> Accounts { get; }
        DbSet<JournalEntry> Transactions { get; }
        DbSet<TransactionJournal> TransactionJournals { get; }
        DbSet<TransactionGroup> TransactionGroups { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
