using Microsoft.EntityFrameworkCore;
using SharedLibrary.Models;
using WebProgrammingTerm.Core.Repositories;

namespace WebProgrammingTerm.Repository.Repositories;

public class UserRepository:GenericRepository<AppUser>,IUserRepository
{
    private readonly DbSet<AppUser> _users;

    public UserRepository(AppDbContext context) : base(context)
    {
        _users = context.Set<AppUser>();
    }

}