using Microsoft.EntityFrameworkCore;
using SharedLibrary.Models;
using WebProgrammingTerm.Core.Repositories;

namespace WebProgrammingTerm.Repository.Repositories;

public class DepotRepository:GenericRepository<Depot>,IDepotRepository
{
    private readonly DbSet<Depot> _depots;
    public DepotRepository(AppDbContext context) : base(context)
    {
        _depots = context.Set<Depot>();
    }
}