using Domain.Entites.Products;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Periestence.Data.Configurations
{
    public class ProductConfigurations : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(p => p.Name)
                .HasColumnType("nvarchar")
                .HasMaxLength(256);
            builder.Property(p => p.Description)
                .HasColumnType("nvarchar")
                .HasMaxLength(512);
            builder.Property(p => p.PictureUrl)
                .HasColumnType("nvarchar")
                .HasMaxLength(256);
            builder.Property(p => p.Price)
                .HasColumnType("decimal(18,2)");

            builder.HasOne(P => P.Brand)
                .WithMany()
                .HasForeignKey(P => P.BrandId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(P => P.Type)
                .WithMany()
                .HasForeignKey(P => P.TypeId)
                .OnDelete(DeleteBehavior.NoAction);

        }
    }
}
