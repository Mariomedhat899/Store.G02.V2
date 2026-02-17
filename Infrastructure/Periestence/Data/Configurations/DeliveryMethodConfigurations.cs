using Domain.Entites.Orders;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Periestence.Data.Configurations
{
    public class DeliveryMethodConfigurations : IEntityTypeConfiguration<DeliveryMethod>
    {
        public void Configure(EntityTypeBuilder<DeliveryMethod> builder)
        {
            builder.Property(D => D.Price)
                   .HasColumnType("decimal(18,2)");

            builder.Property(D => D.ShortName)
                   .HasColumnType("varchar")
                   .HasMaxLength(128);
            builder.Property(D => D.Description)
                   .HasColumnType("varchar")
                   .HasMaxLength(256);
            builder.Property(D => D.DeliveryTime)
                   .HasColumnType("varchar")
                   .HasMaxLength(128);

        }
    }
}
