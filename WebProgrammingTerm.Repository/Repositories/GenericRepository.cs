using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using WebProgrammingTerm.Core.Models;
using WebProgrammingTerm.Core.Repositories;

namespace WebProgrammingTerm.Repository.Repositories;

public class GenericRepository<TEntity>:IGenericRepository<TEntity> where TEntity :Base
{
    private readonly DbSet<TEntity> _dbSet;

    public GenericRepository(AppDbContext context)
    {
        _dbSet = context.Set<TEntity>();
    }
    
    public IQueryable<TEntity?> Where(Expression<Func<TEntity?, bool>> expression)
    {
        var entities =  _dbSet.Where(expression);
        return entities;
    }

    public async Task<bool> AnyAsync(Expression<Func<TEntity?, bool>> expression)
    {
        return await _dbSet.AnyAsync(expression);
    }
    
    public void Update(TEntity? entity)
    {
        if (entity != null) 
            _dbSet.Update(entity);
    }

    public void Remove(TEntity entity)
    {
        entity.IsDeleted = true;
        _dbSet.Update(entity);
    }
}