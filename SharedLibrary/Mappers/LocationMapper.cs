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
            PhoneNumber = locationDto.PhoneNumber,
            Country = locationDto.Country,
            No = locationDto.No,
            PostalCode = locationDto.PostalCode,
            Street = locationDto.Street,
            CreatedAt = DateTime.Now
 
        };

        return location;
    }

    public static void Update(ref Location locationEntity,  LocationUpdateDto locationUpdateDto)
    {
        locationEntity.Country =  string.IsNullOrWhiteSpace(locationUpdateDto.Country) ? locationEntity.Country : locationUpdateDto.Country;
        locationEntity.Street =  string.IsNullOrWhiteSpace(locationUpdateDto.Street) ? locationEntity.Street : locationUpdateDto.Street;
        locationEntity.PostalCode = locationUpdateDto.PostalCode == 0 ? locationEntity.PostalCode : locationUpdateDto.PostalCode;
        locationEntity.No = locationUpdateDto.No == 0 ? locationEntity.No : locationUpdateDto.No;
        locationEntity.PhoneNumber =  string.IsNullOrWhiteSpace(locationUpdateDto.PhoneNumber) ? locationEntity.PhoneNumber : locationUpdateDto.PhoneNumber;
    }
}