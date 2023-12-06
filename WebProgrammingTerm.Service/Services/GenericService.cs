using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using WebProgrammingTerm.Core;
using WebProgrammingTerm.Core.DTO;
using WebProgrammingTerm.Core.Models;
using WebProgrammingTerm.Core.Repositories;
using WebProgrammingTerm.Core.Services;
using WebProgrammingTerm.Core.UnitOfWorks;

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
            return CustomResponseNoDataDto.Fail(404,ResponseMessages.Notfound);
        
        entity.UpdatedAt =DateTime.Now;
        entity.UpdatedBy = entity.Id;
        _repository.Remove(entity);
        await _unitOfWork.CommitAsync();
        return CustomResponseNoDataDto.Success(200);
    }

    public async Task<CustomResponseNoDataDto> AddAsync(TEntity _entity)
    {
        var entity = await _repository.Where(x => x != null && x.Id == _entity.Id && !x.IsDeleted).FirstOrDefaultAsync();
        if (entity is not null )
            return CustomResponseNoDataDto.Fail(409,ResponseMessages.UserNotFound);

        await _repository.AddAsync(_entity);
        await _unitOfWork.CommitAsync();
        return CustomResponseNoDataDto.Success(201);
    }

    public IQueryable<TEntity?> Where(Expression<Func<TEntity?, bool>> expression)
    {
        var entities = _repository.Where(expression);
        return entities;
    }
    
    public async Task<CustomResponseNoDataDto> UpdateAsync(TEntity entity)
    {
        if (entity == null) 
            return CustomResponseNoDataDto.Fail(404,ResponseMessages.Notfound);
        
        entity.UpdatedBy = entity.Id;
        entity.UpdatedAt = DateTime.Now;
        _repository.Update(entity);
        await _unitOfWork.CommitAsync();
        return CustomResponseNoDataDto.Success(200);

    }
    
}