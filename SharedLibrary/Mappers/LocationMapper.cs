using SharedLibrary.DTO;
using SharedLibrary.Models;

namespace SharedLibrary.Mappers;

public static class LocationMapper
{
    public static Location ToLocation(LocationDto locationDto     )
    {
        var location = new Location()
        {
            Id = Guid.NewGuid().ToString(),
            Address = locationDto.Address,
            CreatedAt = DateTime.Now,
            Title = locationDto.Title,
            City = locationDto.City,
            ZipCode = locationDto.ZipCode
 
        };

        return location;
    }

    public static void Update(ref Location locationEntity,  LocationUpdateDto locationUpdateDto)
    {
        locationEntity.City =  string.IsNullOrWhiteSpace(locationUpdateDto.City) ? locationEntity.City : locationUpdateDto.City;
        locationEntity.Address =  string.IsNullOrWhiteSpace(locationUpdateDto.Address) ? locationEntity.Address : locationUpdateDto.Address;
        locationEntity.ZipCode = locationUpdateDto.ZipCode == 0 ? locationEntity.ZipCode : locationUpdateDto.ZipCode;
        locationEntity.Title =  string.IsNullOrWhiteSpace(locationUpdateDto.Title) ? locationEntity.Title : locationUpdateDto.Title;
    }
}