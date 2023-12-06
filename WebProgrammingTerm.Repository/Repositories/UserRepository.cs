using Microsoft.EntityFrameworkCore;
using WebProgrammingTerm.Core.Models;
using WebProgrammingTerm.Core.Repositories;

namespace WebProgrammingTerm.Repository.Repositories;

public class UserRepository:GenericRepository<User>,IUserRepository
{
    private readonly DbSet<User> _users;

    public UserRepository(AppDbContext context) : base(context)
    {
        _users = context.Set<User>();
    }

    public async Task AddAsync(User user)
    {
        if (user == null) throw new ArgumentNullException(nameof(user));

        await _users.AddAsync(user);
    }
}