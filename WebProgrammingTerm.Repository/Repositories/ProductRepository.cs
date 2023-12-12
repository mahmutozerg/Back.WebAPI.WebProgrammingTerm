using Microsoft.EntityFrameworkCore;
using SharedLibrary.Models;
using WebProgrammingTerm.Core.Repositories;

namespace WebProgrammingTerm.Repository.Repositories;

public class ProductRepository:GenericRepository<Product>,IProductRepository
{
    private readonly DbSet<Product> _products;

    public ProductRepository(AppDbContext context) : base(context)
    {
        _products = context.Set<Product>();
        
    }
}