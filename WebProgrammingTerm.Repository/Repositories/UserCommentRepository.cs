using Microsoft.EntityFrameworkCore;
using WebProgrammingTerm.Core.Models;
using WebProgrammingTerm.Core.Repositories;

namespace WebProgrammingTerm.Repository.Repositories;

public class UserCommentRepository:GenericRepository<UserComments>,IUserCommentRepository
{
    private readonly DbSet<UserComments> _userComments;
    public UserCommentRepository(AppDbContext context) : base(context)
    {
        _userComments = context.Set<UserComments>();
    }
}