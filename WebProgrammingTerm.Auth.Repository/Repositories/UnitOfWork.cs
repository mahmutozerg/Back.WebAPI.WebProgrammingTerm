using Microsoft.EntityFrameworkCore;
using WebProgrammingTerm.Auth.Core.DTOs;

namespace WebProgrammingTerm.Auth.Repository.Repositories;

public class UnitOfWork:IUnitOfWork
{
    private readonly DbContext _context;

    public UnitOfWork(AppDbContext appDbContext)
    {
        _context = appDbContext;
    }

    public async Task CommitAsync()
    {
        await _context.SaveChangesAsync();
    }

    public void Commit()
    {
        _context.SaveChanges();
    }

}