using System.Linq.Expressions;
using SharedLibrary.DTO;


namespace WebProgrammingTerm.Core.Services;

public interface IGenericService<TEntity> where TEntity:class
{

    Task<CustomResponseNoDataDto> Remove(string id,string updatedBy);
    Task<CustomResponseDto<TEntity>> AddAsync(TEntity entity,string createdBy);
    IQueryable<TEntity?> Where(Expression<Func<TEntity?, bool>> expression);
    Task<CustomResponseNoDataDto> UpdateAsync(TEntity entity,string updatedBy);
}