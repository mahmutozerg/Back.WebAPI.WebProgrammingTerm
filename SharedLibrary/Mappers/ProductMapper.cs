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

        public static void Update(ProductUpdateDto productUpdateDto, ref Product productEntity)
        {
            productEntity.Price = productUpdateDto.Price == 0f ? productEntity.Price : productUpdateDto.Price;
            productEntity.Name =  string.IsNullOrWhiteSpace(productUpdateDto.Name) ? productEntity.Name : productUpdateDto.Name;
            productEntity.ImagePath =  string.IsNullOrWhiteSpace(productUpdateDto.ImagePath) ? productEntity.ImagePath : productUpdateDto.ImagePath;
            productEntity.Stock = productUpdateDto.Stock == 0 ? productEntity.Stock : productUpdateDto.Stock;
            productEntity.DiscountRate = productUpdateDto.DiscountRate == 0f ? productEntity.DiscountRate : productUpdateDto.DiscountRate;

        }
    }
}

