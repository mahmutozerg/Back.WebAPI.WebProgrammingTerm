using WebProgrammingTerm.Core.DTO;
using WebProgrammingTerm.Core.Models;

namespace WebProgrammingTerm.Core.Mappers;

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