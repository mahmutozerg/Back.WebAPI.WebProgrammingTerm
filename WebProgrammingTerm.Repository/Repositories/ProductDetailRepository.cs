using Microsoft.EntityFrameworkCore;
using WebProgrammingTerm.Core.Models;
using WebProgrammingTerm.Core.Repositories;

namespace WebProgrammingTerm.Repository.Repositories;

public class ProductDetailRepository:GenericRepository<ProductDetail>,IProductDetailRepository
{
    private readonly DbSet<ProductDetail> _productDetails;
    public ProductDetailRepository(AppDbContext context) : base(context)
    {
        _productDetails = context.Set<ProductDetail>();
    }
}