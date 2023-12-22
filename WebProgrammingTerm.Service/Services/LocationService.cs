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
        var locationEntity = await _locationRepository.Where(l => l != null && l.Id == locationUpdateDto.Id && l.UserId == updatedBy &&!l.IsDeleted).FirstOrDefaultAsync();

        if (locationEntity is null)
            throw new Exception(ResponseMessages.LocationNotFound);

        locationEntity.UserId = updatedBy;
        locationEntity.UpdatedBy = updatedBy;

        LocationMapper.Update(ref locationEntity,locationUpdateDto);
        
        _locationRepository.Update(locationEntity);
        await _unitOfWork.CommitAsync();
        return CustomResponseDto<Location>.Success(locationEntity, ResponseCodes.Updated);


    }

    public async Task<CustomResponseDto<Location>> AddAsync(LocationDto locationDto,ClaimsIdentity claimsIdentity)
    {
        var createdBy = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        var userEntity = await _userService.GetUserWithLocations(createdBy);
        
        var locationEntity = LocationMapper.ToLocation(locationDto);
        locationEntity.User = userEntity;
        locationEntity.CreatedBy = createdBy;
        locationEntity.UpdatedAt = DateTime.Now;
        locationEntity.UpdatedBy = createdBy;

        await _locationRepository.AddAsync(locationEntity);
        await _unitOfWork.CommitAsync();

        return CustomResponseDto<Location>.Success(locationEntity, ResponseCodes.Created);
    }

    public async Task<CustomResponseDto<List<Location>?>> GetLocationsAsync(ClaimsIdentity claimsIdentity)
    {
        var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        var locations = await _locationRepository.Where(l => l != null && l.UserId == userId && !l.IsDeleted).ToListAsync();
        return CustomResponseDto<List<Location>?>.Success(locations, ResponseCodes.Ok);
    }

    public async Task<CustomResponseNoDataDto> Delete(string id, ClaimsIdentity claimsIdentity)
    {
        var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var locationEntity = await _locationRepository.Where(l =>
            l != null && l.UserId == userId && l.Id == id  && !l.IsDeleted).SingleOrDefaultAsync();

        if (locationEntity is null)
            throw new Exception(ResponseMessages.LocationNotFound);


        locationEntity.UpdatedAt = DateTime.Now;
        locationEntity.UpdatedBy = userId;
        
        _locationRepository.Remove(locationEntity);

        await _unitOfWork.CommitAsync();

        return CustomResponseNoDataDto.Success(200);
    }
}