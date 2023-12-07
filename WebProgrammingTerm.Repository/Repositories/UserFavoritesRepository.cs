using Microsoft.EntityFrameworkCore;
using WebProgrammingTerm.Core.Models;
using WebProgrammingTerm.Core.Repositories;

namespace WebProgrammingTerm.Repository.Repositories;

public class UserFavoritesRepository:GenericRepository<UserFavorites>,IUserFavoritesRepository
{
    private readonly DbSet<UserFavorites> _userFavorites;
    public UserFavoritesRepository(AppDbContext context) : base(context)
    {
        _userFavorites = context.Set<UserFavorites>();
    }
}