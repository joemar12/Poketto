using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Poketto.Domain.Entities;

namespace Poketto.Infrastructure.Persistence.Configurations
{
    public class TransactionJournalEntityConfiguration : IEntityTypeConfiguration<TransactionJournal>
    {
        public void Configure(EntityTypeBuilder<TransactionJournal> builder)
        {
            builder.Property(x => x.Description)
                .HasMaxLength(500)
                .IsRequired();

            builder.Property(x => x.OwnerUserId)
                .HasMaxLength(100)
                .IsRequired();

            builder
                .HasOne(x => x.TransactionGroup)
                .WithMany(x => x.TransactionJournals)
                .HasForeignKey(x => x.TransactionGroupId);
        }
    }
}
