using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebProgrammingTerm.Core.Models;

namespace WebProgrammingTerm.Repository.Configurations;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.HasKey(o => o.Id);
        builder
            .HasOne(o => o.User)
            .WithMany(u => u.Orders)
            .HasForeignKey(o => o.UserId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasMany<Product>(o => o.Products).WithMany(p => p.Orders);
         
        builder
            .HasOne(o => o.OrderDetail)
            .WithOne(od => od.Order)        .HasForeignKey<OrderDetail>(od => od.OrderId);
        ;
    }
}