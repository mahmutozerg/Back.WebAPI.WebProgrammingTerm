﻿using Microsoft.EntityFrameworkCore;
using SharedLibrary.Models;
using WebProgrammingTerm.Core.Repositories;

namespace WebProgrammingTerm.Repository.Repositories;

public class UserRepository:GenericRepository<User>,IUserRepository
{
    private readonly DbSet<User> _users;

    public UserRepository(AppDbContext context) : base(context)
    {
        _users = context.Set<User>();
    }

}