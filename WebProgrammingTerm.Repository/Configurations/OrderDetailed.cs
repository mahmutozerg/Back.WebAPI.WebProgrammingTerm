using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SharedLibrary.Models;

namespace WebProgrammingTerm.Repository.Configurations
{
    public class OrderDetailConfiguration : IEntityTypeConfiguration<OrderDetail>
    {
        public void Configure(EntityTypeBuilder<OrderDetail> builder)
        {
            builder.HasKey(od => new { od.OrderId });

            builder
                .HasOne(od => od.Order)
                .WithOne(o => o.OrderDetail)
                .HasForeignKey<OrderDetail>(od => od.OrderId)
                .OnDelete(DeleteBehavior.NoAction);  
        }
    }
}