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

    public async Task<CustomResponseDto<Depot>> UpdateAsync(DepotUpdateDto depotUpdateDto, string updatedBy)
    {
        var entity = await _depotRepository.Where(d => d != null && d.Id == depotUpdateDto.TargetDepotId && !d.IsDeleted).SingleOrDefaultAsync();

        if (entity is null)
            throw new Exception(ResponseMessages.DepotNotFound);
        
        entity.City = string.IsNullOrWhiteSpace(depotUpdateDto.City) ? entity.City : depotUpdateDto.City;
        entity.Street = string.IsNullOrWhiteSpace(depotUpdateDto.Street) ? entity.Street : depotUpdateDto.Street;
        entity.Country = string.IsNullOrWhiteSpace(depotUpdateDto.Country) ? entity.Country : depotUpdateDto.Country;
        entity.Contact = string.IsNullOrWhiteSpace(depotUpdateDto.Contact) ? entity.Contact : depotUpdateDto.Contact;
        entity.UpdatedBy = updatedBy;
        _depotRepository.Update(entity);
        await _unitOfWork.CommitAsync();
        return CustomResponseDto<Depot>.Success(entity,ResponseCodes.Updated);
        
    }
    

}