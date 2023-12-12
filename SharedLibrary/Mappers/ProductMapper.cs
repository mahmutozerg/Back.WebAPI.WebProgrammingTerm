using SharedLibrary.DTO;
using SharedLibrary.Models;

namespace SharedLibrary.Mappers;

public static class ProductMapper
{
    public static Product ToProduct(ProductAddDto productAddDto )
    {
        var product = new Product()
        {
            Id = Guid.NewGuid().ToString(),
             Stock = productAddDto.Stock,
            Price = productAddDto.Price,
            Name = productAddDto.Name,
            CreatedAt = DateTime.Now,
            DiscountRate = productAddDto.DiscountRate
       
        };

        return product;
    }

 
}