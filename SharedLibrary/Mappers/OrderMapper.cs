using SharedLibrary.DTO;
using SharedLibrary.Models;

namespace SharedLibrary.Mappers;

public static class OrderMapper
{
    public static Order ToOrder(OrderAddDto orderAddDto)
    {
        var order = new Order()
        {
            Id = Guid.NewGuid().ToString(),
            Shipment = orderAddDto.Shipment,
            LocationId = orderAddDto.LocationId,
            Products = new List<Product>()
        };

        return order;
    }
}