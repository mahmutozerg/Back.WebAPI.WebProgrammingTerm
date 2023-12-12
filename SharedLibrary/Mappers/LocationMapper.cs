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
}