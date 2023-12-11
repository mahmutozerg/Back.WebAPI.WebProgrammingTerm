using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebProgrammingTerm.Core.Models;

namespace WebProgrammingTerm.Repository.Configurations
{
    public class ProductDetailConfiguration : IEntityTypeConfiguration<ProductDetail>
    {
        public void Configure(EntityTypeBuilder<ProductDetail> builder)
        {
            builder.HasKey(pd => new { pd.ProductId, pd.DepotId });

            builder
                .HasOne(pd => pd.Product)
                .WithOne(p => p.ProductDetail)
                .HasForeignKey<ProductDetail>(pd => pd.ProductId);

            builder
                .HasOne(pd => pd.Depot)
                .WithMany()  // Use WithMany() to indicate that the 'Depot' can be referenced by multiple 'ProductDetails'
                .HasForeignKey(pd => pd.DepotId)
                .OnDelete(DeleteBehavior.Restrict); 
        }
    }
}