using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using SharedLibrary.DTO;
using SharedLibrary.Models;
using WebProgrammingTerm.Core;

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

    public async Task<CustomResponseDto<Depot>> UpdateAsync(DepotUpdateDto depotUpdateDto, ClaimsIdentity claimsIdentity)
    {
        var updatedBy = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var depotEntity = await _depotRepository.Where(d => d != null && d.Id == depotUpdateDto.TargetDepotId && !d.IsDeleted).SingleOrDefaultAsync();

        if (depotEntity is null)
            throw new Exception(ResponseMessages.DepotNotFound);
        
        depotEntity.City = string.IsNullOrWhiteSpace(depotUpdateDto.City) ? depotEntity.City : depotUpdateDto.City;
        depotEntity.Street = string.IsNullOrWhiteSpace(depotUpdateDto.Street) ? depotEntity.Street : depotUpdateDto.Street;
        depotEntity.Country = string.IsNullOrWhiteSpace(depotUpdateDto.Country) ? depotEntity.Country : depotUpdateDto.Country;
        depotEntity.Contact = string.IsNullOrWhiteSpace(depotUpdateDto.Contact) ? depotEntity.Contact : depotUpdateDto.Contact;
        depotEntity.UpdatedBy = updatedBy;
        _depotRepository.Update(depotEntity);
        await _unitOfWork.CommitAsync();
        return CustomResponseDto<Depot>.Success(depotEntity,ResponseCodes.Updated);
        
    }
    

}