using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Poketto.Domain.Entities;

namespace Poketto.Infrastructure.Persistence.Configurations;

public class JournalEntryItemEntityConfiguration : IEntityTypeConfiguration<JournalEntryItem>
{
    public void Configure(EntityTypeBuilder<JournalEntryItem> builder)
    {
        builder.ToTable("JournalEntryItems");
        builder.Property(x => x.Description)
            .HasMaxLength(500)
            .IsRequired();

        builder
            .HasOne(x => x.Account)
            .WithMany(x => x.JournalEntryItems)
            .HasForeignKey(x => x.AccountId);

        builder
            .HasOne(x => x.JournalEntry)
            .WithMany(x => x.JournalEntryItems)
            .HasForeignKey(x => x.JournalEntryId);
    }
}
