using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using SharedLibrary.DTO;
using SharedLibrary.Mappers;
using SharedLibrary.Models;
using WebProgrammingTerm.Core;

using WebProgrammingTerm.Core.Repositories;
using WebProgrammingTerm.Core.Services;
using WebProgrammingTerm.Core.UnitOfWorks;

namespace WebProgrammingTerm.Service.Services;

public class LocationService:GenericService<Location>,ILocationService
{
    private readonly ILocationRepository _locationRepository;
    private readonly IUserService _userService;
    private IUnitOfWork _unitOfWork;
    public LocationService(IUnitOfWork unitOfWork, ILocationRepository locationRepository, IUserService userService) : base(locationRepository, unitOfWork)
    {
        _unitOfWork = unitOfWork;
        _locationRepository = locationRepository;
        _userService = userService;
    }

    public async Task<CustomResponseDto<Location>> UpdateAsync(LocationUpdateDto locationUpdateDto, ClaimsIdentity claimsIdentity)
    {
        var updatedBy = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var locationEntity = await _locationRepository.Where(p => p.Id == locationUpdateDto.Id && !p.IsDeleted).FirstOrDefaultAsync();

        if (locationEntity is null)
            throw new Exception(ResponseMessages.LocationNotFound);

        locationEntity.UserId = updatedBy;
        locationEntity.Country =  string.IsNullOrWhiteSpace(locationUpdateDto.Country) ? locationEntity.Country : locationUpdateDto.Country;
        locationEntity.Street =  string.IsNullOrWhiteSpace(locationUpdateDto.Street) ? locationEntity.Street : locationUpdateDto.Street;
        locationEntity.PostalCode = locationUpdateDto.PostalCode == 0 ? locationEntity.PostalCode : locationUpdateDto.PostalCode;
        locationEntity.No = locationUpdateDto.No == 0 ? locationEntity.No : locationUpdateDto.No;
        locationEntity.PhoneNumber =  string.IsNullOrWhiteSpace(locationUpdateDto.PhoneNumber) ? locationEntity.PhoneNumber : locationUpdateDto.PhoneNumber;

        locationEntity.UpdatedBy = updatedBy;

        _locationRepository.Update(locationEntity);
        await _unitOfWork.CommitAsync();
        return CustomResponseDto<Location>.Success(locationEntity, ResponseCodes.Updated);


    }

    public async Task<CustomResponseDto<Location>> AddAsync(LocationDto locationDto,ClaimsIdentity claimsIdentity)
    {
        var createdBy = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        var userEntity = await _userService.GetUserWithLocations(createdBy);
        
        var locationEntity = LocationMapper.ToLocation(locationDto);
        locationEntity.AppUser = userEntity;
        locationEntity.CreatedBy = createdBy;
        locationEntity.UpdatedAt = DateTime.Now;
        locationEntity.UpdatedBy = createdBy;

        await _locationRepository.AddAsync(locationEntity);
        await _unitOfWork.CommitAsync();

        return CustomResponseDto<Location>.Success(locationEntity, ResponseCodes.Created);
    }
}