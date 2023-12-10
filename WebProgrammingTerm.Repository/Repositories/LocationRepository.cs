using Microsoft.EntityFrameworkCore;
using WebProgrammingTerm.Core.Models;
using WebProgrammingTerm.Core.Repositories;

namespace WebProgrammingTerm.Repository.Repositories;

public class LocationRepository:GenericRepository<Location>,ILocationRepository
{
    private readonly DbSet<Location> _locations;
    public LocationRepository(AppDbContext context) : base(context)
    {
        _locations = context.Set<Location>();
    }
}