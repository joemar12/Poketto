using Microsoft.EntityFrameworkCore;
using Poketto.Domain.Entities;

namespace Poketto.Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<Account> Accounts { get; }
        DbSet<JournalEntryItem> JournalEntryItems { get; }
        DbSet<JournalEntry> JournalEntries { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}