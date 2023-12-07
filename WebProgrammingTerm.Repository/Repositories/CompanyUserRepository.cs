using Microsoft.EntityFrameworkCore;
using WebProgrammingTerm.Core.Models;
using WebProgrammingTerm.Core.Repositories;

namespace WebProgrammingTerm.Repository.Repositories;

public class CompanyUserRepository:GenericRepository<CompanyUser>,ICompanyUserRepository
{
    private readonly DbSet<CompanyUser> _companyUsers;

    public CompanyUserRepository(AppDbContext context) : base(context)
    {
        _companyUsers = context.CompanyUser;
    }
    
    
}