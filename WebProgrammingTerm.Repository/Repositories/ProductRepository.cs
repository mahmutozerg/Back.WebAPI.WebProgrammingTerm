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

    public async Task<List<Product>> GetProducstByPageAdmin(int page)
    {
        return await _products
            .Skip(20 * (page-1))
            .Take(20)
            .Include(p=>p.ProductDetail)
            .Include(p=>p.Company)
            .ToListAsync();    
    }

    public async Task<List<Product>> GetProductsByName(int page, string name)
    {
        var query = await _products
            .Where(p => p.Stock >= 0 && !p.IsDeleted &&
                        p.Name.Contains(name))
            .Skip((21) * (page - 1))
            .Take(21)
            .Include(p => p.ProductDetail)
            .Include(p => p.Company)
            .AsNoTracking()
            .ToListAsync();

        return query.ToHashSet().ToList();
    }

    public async Task<List<Product>> GetProductsByNameAdmin(int page,string name)
    {
        var query = await _products
            .Where(p => p.Stock >= 0 &&
                        p.Name.Contains(name))
            .Skip((21) * (page - 1))
            .Take(21)
            .Include(p => p.ProductDetail)
            .Include(p => p.Company)
            .AsNoTracking()
            .ToListAsync();

        return query.ToHashSet().ToList();    
        
    }
}