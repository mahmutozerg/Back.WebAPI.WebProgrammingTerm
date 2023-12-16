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

    public static void Upadte(ref Depot depotEntity, DepotUpdateDto depotUpdateDto)
    {
        depotEntity.City = string.IsNullOrWhiteSpace(depotUpdateDto.City) ? depotEntity.City : depotUpdateDto.City;
        depotEntity.Street = string.IsNullOrWhiteSpace(depotUpdateDto.Street) ? depotEntity.Street : depotUpdateDto.Street;
        depotEntity.Country = string.IsNullOrWhiteSpace(depotUpdateDto.Country) ? depotEntity.Country : depotUpdateDto.Country;
        depotEntity.Contact = string.IsNullOrWhiteSpace(depotUpdateDto.Contact) ? depotEntity.Contact : depotUpdateDto.Contact;
    }
}