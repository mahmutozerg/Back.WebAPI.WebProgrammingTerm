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

    public async Task<List<Product>> GetProductsByName(int page, string name)
    {
        var splitName = name.Split(" ");
        
        var products = new List<Product>();

        foreach (var namefrac in splitName)
        {
            var query = await _products
                .Where(p => p.Stock >= 0 && !p.IsDeleted &&
                            p.Name.Contains(namefrac))
                .Skip((20/splitName.Length) * (page - 1))
                .Take(20/splitName.Length)
                .Include(p => p.ProductDetail)
                .Include(p => p.Company)
                .AsNoTracking()
                .ToListAsync();
            
            products.AddRange(query);

        }

        return products.ToHashSet().ToList();;
    }
}