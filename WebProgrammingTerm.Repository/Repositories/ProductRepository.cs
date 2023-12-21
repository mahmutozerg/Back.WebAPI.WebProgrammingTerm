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

    public async Task<List<Product>> GetProducstByPage(int page)
    {
        return await _products.Skip(20 * (page-1)).Take(20).Where(p=>p.Stock >=0 && !p.IsDeleted)
            .Include(p=>p.ProductDetail).Include(p=>p.Company).ToListAsync();
    }
}