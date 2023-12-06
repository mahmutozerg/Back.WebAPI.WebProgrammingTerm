using System.Linq.Expressions;
using WebProgrammingTerm.Core.DTO;
using WebProgrammingTerm.Core.Models;

namespace WebProgrammingTerm.Core.Services;

public interface IGenericService<TEntity> where TEntity:class
{

    Task<CustomResponseNoDataDto> Remove(string id,string updatedBy);
    Task<CustomResponseNoDataDto> AddAsync(TEntity entity);
    IQueryable<TEntity?> Where(Expression<Func<TEntity?, bool>> expression);
    Task<CustomResponseNoDataDto> UpdateAsync(TEntity entity,string updatedBy);
}