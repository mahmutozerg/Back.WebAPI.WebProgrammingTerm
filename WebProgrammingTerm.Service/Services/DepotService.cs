using Microsoft.EntityFrameworkCore;
using WebProgrammingTerm.Core;
using WebProgrammingTerm.Core.DTO;
using WebProgrammingTerm.Core.Mappers;
using WebProgrammingTerm.Core.Models;
using WebProgrammingTerm.Core.Repositories;
using WebProgrammingTerm.Core.Services;
using WebProgrammingTerm.Core.UnitOfWorks;

namespace WebProgrammingTerm.Service.Services;

public class DepotService:GenericService<Depot>,IDepotService
{
    private readonly IDepotRepository _depotRepository;
    private readonly IUnitOfWork _unitOfWork;
    public DepotService( IUnitOfWork unitOfWork, IDepotRepository depotRepository) : base(depotRepository, unitOfWork)
    {
        _unitOfWork = unitOfWork;
        _depotRepository = depotRepository;
    }

    public async Task<CustomResponseNoDataDto> UpdateAsync(DepotUpdateDto depotUpdateDto, string updatedBy)
    {
        var entity = await _depotRepository.Where(d => d != null && d.Id == depotUpdateDto.TargetDepotId && !d.IsDeleted).SingleOrDefaultAsync();

        if (entity is null)
            return CustomResponseNoDataDto.Fail(404,ResponseMessages.Notfound);
        
        entity.City = string.IsNullOrEmpty(depotUpdateDto.City) ? entity.City : depotUpdateDto.City;
        entity.Street = string.IsNullOrEmpty(depotUpdateDto.Street) ? entity.Street : depotUpdateDto.Street;
        entity.Country = string.IsNullOrEmpty(depotUpdateDto.Country) ? entity.Country : depotUpdateDto.Country;
        entity.Contact = string.IsNullOrEmpty(depotUpdateDto.Contact) ? entity.Contact : depotUpdateDto.Contact;
        entity.UpdatedBy = updatedBy;
        _depotRepository.Update(entity);
        await _unitOfWork.CommitAsync();
        return CustomResponseNoDataDto.Success(200);
        
    }
    
    public async Task<CustomResponseNoDataDto> AddAsync(DepotAddDto depotUpdateDto, string createdBy)
    {
        var entity = await _depotRepository.Where(d => d != null && d.Contact == depotUpdateDto.Contact && !d.IsDeleted).SingleOrDefaultAsync();

        if (entity is not null)
            return CustomResponseNoDataDto.Fail(404,ResponseMessages.Notfound);

        var depotEntity = DepotMapper.ToDepot(depotUpdateDto);
        depotEntity.CreatedBy = createdBy;
        depotEntity.UpdatedBy = createdBy;
        await _depotRepository.AddAsync(depotEntity);
        await _unitOfWork.CommitAsync();
        return CustomResponseNoDataDto.Success(200);
        
    }
}