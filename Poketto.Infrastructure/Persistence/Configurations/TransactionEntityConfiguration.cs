using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Poketto.Domain.Entities;

namespace Poketto.Infrastructure.Persistence.Configurations
{
    public class JournalEntryEntityConfiguration : IEntityTypeConfiguration<JournalEntry>
    {
        public void Configure(EntityTypeBuilder<JournalEntry> builder)
        {
            builder.ToTable("JournalEntries");
            builder
                .HasOne(x => x.Account)
                .WithMany(x => x.JournalEntries)
                .HasForeignKey(x => x.AccountId);

            builder
                .HasOne(x => x.TransactionJournal)
                .WithMany(x => x.JournalEntries)
                .HasForeignKey(x => x.TransactionJournalId);
        }
    }
}
