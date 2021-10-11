using System;
using System.Collections.Generic;
using System.Text;
using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.config
{
    public class ProductConfig : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(p => p.Id)
                .IsRequired()
                .ValueGeneratedOnAdd();
            builder.Property(p => p.Name).IsRequired()
                .HasMaxLength(120);
            builder.Property(p => p.Description).IsRequired()
                .HasMaxLength(200);
            builder.Property(p => p.Unit).IsRequired();
            builder.Property(p => p.Price).IsRequired();//.HasColumnType("decimal(18,5)");
            builder.Property(p => p.ImgPath).IsRequired();
            builder.HasOne(p => p.ProductBrand).WithMany()
                .HasForeignKey(p => p.ProductBrandID);
            builder.HasOne(p => p.ProductType).WithMany()
                .HasForeignKey(p => p.ProductTypeID);
        }
        public void Configure(EntityTypeBuilder<ProductBrand> builder)
        {
            builder.Property(p => p.Id).ValueGeneratedOnAdd();
        }
        public void Configure(EntityTypeBuilder<ProductType> builder)
        {
            builder.Property(p => p.Id).ValueGeneratedOnAdd();
        }
    }
}
