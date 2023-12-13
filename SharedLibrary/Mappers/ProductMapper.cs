using System;
using SharedLibrary.DTO;
using SharedLibrary.Models;

namespace SharedLibrary.Mappers
{
    public static class ProductMapper
    {
        public static Product ToProduct(ProductAddDto productAddDto)
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

        public static ProductGetDto ToAddDto(Product product)
        {
            
            var productAddDto = new ProductGetDto()
            {
                Author = product.ProductDetail.Author,
                Category = product.Category,
                DepotId = product.ProductDetail.DepotId,
                DiscountRate = product.DiscountRate,
                ImagePath = product.ImagePath,
                Language = product.ProductDetail.Language,
                Name = product.Name,
                Price = product.Price,
                Page = product.ProductDetail.Page,
                PublishDate = product.ProductDetail.PublishDate,
                Publisher = product.ProductDetail.Publisher,
                Stock = product.Stock,
                Company = product.Company
            };
            return productAddDto;
            
        }
    }
}

