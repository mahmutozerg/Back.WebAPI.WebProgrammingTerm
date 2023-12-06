using WebProgrammingTerm.Core.Models;

namespace WebProgrammingTerm.Core.Repositories;

public interface ICompanyRepository:IGenericRepository<Company>
{
    Task AddAsync(Company entity);

}