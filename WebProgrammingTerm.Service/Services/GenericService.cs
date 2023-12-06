using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using WebProgrammingTerm.Core.DTO;
using WebProgrammingTerm.Core.Models;
using WebProgrammingTerm.Core.Repositories;
using WebProgrammingTerm.Core.Services;
using WebProgrammingTerm.Core.UnitOfWorks;
using YemekTarifiApp.Core;

namespace WebProgrammingTerm.Service.Services;

public class GenericService<TEntity> : IGenericService<TEntity> where TEntity :Base
{
    private readonly IGenericRepository<TEntity?> _repository;
    private readonly IUnitOfWork _unitOfWork;


    public GenericService(IGenericRepository<TEntity?> repository, IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }


    public async Task<CustomResponseNoDataDto> Remove(string id)
    {
        var entity = await _repository.Where(x => x.Id == id && !x.IsDeleted).FirstOrDefaultAsync();
        if (entity is null )
        {
            return CustomResponseNoDataDto.Fail(404,ResponseMessages.UserNotFound);
        }
        entity.UpdatedAt =DateTime.Now;
        entity.UpdatedBy = entity.Id;
        _repository.Remove(entity);
        await _unitOfWork.CommitAsync();
        return CustomResponseNoDataDto.Success(200);
    }

    public IQueryable<TEntity?> Where(Expression<Func<TEntity?, bool>> expression)
    {
        var entities = _repository.Where(expression);
        return entities;
    }
    
    public async Task<bool> Update(TEntity? entity)
    {
        if (entity == null) 
            return false;
        
        entity.UpdatedBy = entity.Id;
        entity.UpdatedAt = DateTime.Now;
        _repository.Update(entity);
        await _unitOfWork.CommitAsync();
        return true;

    }
    
}