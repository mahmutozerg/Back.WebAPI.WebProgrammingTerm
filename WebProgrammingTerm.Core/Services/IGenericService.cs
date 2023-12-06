using System.Linq.Expressions;
using WebProgrammingTerm.Core.DTO;

namespace WebProgrammingTerm.Core.Services;

public interface IGenericService<TEntity> where TEntity:class
{

    Task<CustomResponseNoDataDto> Remove(string userId);
    IQueryable<TEntity?> Where(Expression<Func<TEntity?, bool>> expression);
    Task<bool> Update(TEntity? entity);
}