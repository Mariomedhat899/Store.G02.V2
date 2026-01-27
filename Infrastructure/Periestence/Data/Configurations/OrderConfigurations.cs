using Domain.Entites.Orders;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Periestence.Data.Configurations
{
    public class OrderConfigurations : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.OwnsOne(O => O.ShippingAddress);

            builder.HasOne(O => O.DeliveryMethod)
                   .WithMany()
                   .HasForeignKey(O => O.DeliveryMethodId);

            builder.HasMany(O => O.items)
                   .WithOne()
                   .OnDelete(DeleteBehavior.Cascade);

            builder.Property(O => O.SubTotal)
                   .HasColumnType("decimal(18,2)");

            
        }
    }
}
