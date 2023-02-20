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
            builder.Property(x => x.RefCode)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(x => x.Description)
                .HasMaxLength(500)
                .IsRequired();

            builder.Property(x => x.OwnerUserId)
                .HasMaxLength(100)
                .IsRequired();
        }
    }
}
