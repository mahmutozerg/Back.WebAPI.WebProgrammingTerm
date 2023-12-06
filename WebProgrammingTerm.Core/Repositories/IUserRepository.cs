﻿using WebProgrammingTerm.Core.Models;

namespace WebProgrammingTerm.Core.Repositories;

public interface IUserRepository:IGenericRepository<User>
{
    Task AddAsync(User user);

}