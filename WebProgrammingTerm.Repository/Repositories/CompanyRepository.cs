using Microsoft.EntityFrameworkCore;
using WebProgrammingTerm.Core.Models;
using WebProgrammingTerm.Core.Repositories;

namespace WebProgrammingTerm.Repository.Repositories;

public class CompanyRepository:GenericRepository<Company>,ICompanyRepository
{
    private readonly DbSet<Company> _companies;

    public CompanyRepository(AppDbContext context) : base(context)
    {
        _companies = context.Set<Company>();
    }
}
