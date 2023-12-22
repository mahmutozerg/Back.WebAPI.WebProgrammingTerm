using SharedLibrary.Models;

namespace WebProgrammingTerm.Core.Repositories;

public interface IProductRepository:IGenericRepository<Product>
{
    Task<List<Product>> GetProducstByPage(int page);

    Task<List<Product>> GetProductsByName(int page, string name);

}