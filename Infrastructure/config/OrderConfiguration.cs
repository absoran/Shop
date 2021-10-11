using System;
using System.Collections.Generic;
using System.Text;
using Core.Entities.Order;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace Infrastructure.config
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            //builder.OwnsOne(o => o.OrderAddress, a =>
            //{
            //    a.WithOwner();
            //});
            builder.Property(p => p.PaymentIntentId).IsRequired();
            builder.Property(p => p.Status).IsRequired();
            builder.Property(p => p.BuyerMail).IsRequired();
            builder.Property(s => s.Status)
                   .HasConversion(
                       o => o.ToString(),
                       o => (OrderStatus)Enum.Parse(typeof(OrderStatus), o)
                   );
            builder.HasMany(o => o.OrderItems).WithOne().OnDelete(DeleteBehavior.Cascade);
        }
    }
}
