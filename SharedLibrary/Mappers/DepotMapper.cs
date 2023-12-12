using SharedLibrary.DTO;
using SharedLibrary.Models;

namespace SharedLibrary.Mappers;

public static class DepotMapper
{
    public static Depot ToDepot(DepotAddDto depotAddDto )
    {
        var company = new Depot()
        {
            Id = Guid.NewGuid().ToString(),
            Contact = depotAddDto.Contact,
            City = depotAddDto.City,
            Country = depotAddDto.Country,
            Street = depotAddDto.Street,
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now,
        };

        return company;
    }
    public static Depot ToDepot(DepotUpdateDto depotUpdateDto )
    {
        var company = new Depot()
        {
            Contact = depotUpdateDto.Contact,
            City = depotUpdateDto.City,
            Country = depotUpdateDto.Country,
            Street = depotUpdateDto.Street,
            UpdatedAt = DateTime.Now,
        };

        return company;
    }
}