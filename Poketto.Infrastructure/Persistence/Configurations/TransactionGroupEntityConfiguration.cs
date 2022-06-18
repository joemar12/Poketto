using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Poketto.Domain.Entities;

namespace Poketto.Infrastructure.Persistence.Configurations
{
    public class TransactionGroupEntityConfiguration : IEntityTypeConfiguration<TransactionGroup>
    {
        public void Configure(EntityTypeBuilder<TransactionGroup> builder)
        {
            builder.Property(x => x.Title)
                .HasMaxLength(300)
                .IsRequired();

            builder.Property(x => x.OwnerUserId)
                .HasMaxLength(100)
                .IsRequired();
        }
    }
}
