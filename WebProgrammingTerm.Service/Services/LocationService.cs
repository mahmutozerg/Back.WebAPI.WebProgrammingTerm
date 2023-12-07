using Microsoft.EntityFrameworkCore;
using WebProgrammingTerm.Core;
using WebProgrammingTerm.Core.DTO;
using WebProgrammingTerm.Core.Mappers;
using WebProgrammingTerm.Core.Models;
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

    public async Task<CustomResponseDto<Location>> UpdateAsync(LocationUpdateDto locationUpdateDto, string updatedBy)
    {
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

    public async Task<CustomResponseDto<Location>> AddAsync(LocationDto locationDto, string createdBy)
    {
        var userEntity = await _userService
            .Where(u => u != null && u.Id == createdBy && !u.IsDeleted)
            .Include(u=>u.Locations)
            .FirstOrDefaultAsync();
        if (userEntity is null)
            throw new Exception(ResponseMessages.UserNotFound);

        if (userEntity.Locations.Count > 10)
            throw new Exception("Max Location Count Reached");
        
        var locationEntity = LocationMapper.ToLocation(locationDto);
        locationEntity.User = userEntity;
         locationEntity.CreatedBy = createdBy;
        locationEntity.UpdatedAt = DateTime.Now;
        locationEntity.UpdatedBy = createdBy;

        await _locationRepository.AddAsync(locationEntity);
        await _unitOfWork.CommitAsync();

        return CustomResponseDto<Location>.Success(locationEntity, ResponseCodes.Created);
    }
}