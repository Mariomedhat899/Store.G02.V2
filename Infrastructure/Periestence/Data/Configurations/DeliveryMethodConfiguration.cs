using Domain.Entites.Orders;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Periestence.Data.Configurations
{
    public class DeliveryMethodConfiguration : IEntityTypeConfiguration<DeliveryMethod>
    {
        public void Configure(EntityTypeBuilder<DeliveryMethod> builder)
        {
            builder.Property(DM => DM.Price)
                   .HasColumnType("decimal(18,2)");

            builder.Property(DM => DM.ShortName)
                   .HasColumnType("varchar")
                   .HasMaxLength(128);
            builder.Property(DM => DM.Description)
                   .HasColumnType("varchar")
                   .HasMaxLength(256);
            builder.Property(DM => DM.DeliveryTime)
                   .HasColumnType("varchar")
                   .HasMaxLength(128);
            builder.Property(DM => DM.Price)
                   .HasColumnType("decimal(18,2)");

        }
    }
}
