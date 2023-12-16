using SharedLibrary.DTO;
using SharedLibrary.Models;

namespace SharedLibrary.Mappers;

public static class ProductDetailMapper
{

    
    public static ProductDetail ToProductDetail(ProductDetailAddDto productDetailAddDto )
    {
        var productDetail = new ProductDetail()
        {
            Id = Guid.NewGuid().ToString(),
            Author = productDetailAddDto.Author,
            Publisher = productDetailAddDto.Publisher,
            Language = productDetailAddDto.Language,
            Size = productDetailAddDto.Size,
             Page = productDetailAddDto.Page,
            PublishDate = productDetailAddDto.PublishDate,
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now,
        };

        return productDetail;
    }
 
    public static ProductDetailAddDto ToProductDetail(ProductAddDto productDetailAddDto )
    {
        var productDetail = new ProductDetailAddDto()
        {
             Author = productDetailAddDto.Author,
             DepotId = productDetailAddDto.DepotId,
            Publisher = productDetailAddDto.Publisher,
            Language = productDetailAddDto.Language,
            Size = productDetailAddDto.Size,
            Page = productDetailAddDto.Page,
            PublishDate = productDetailAddDto.PublishDate,
            
 
        };

        return productDetail;
    }

    public static void Update(ProductDetailUpdateDto productDetailUpdateDto,ref ProductDetail productDetailEntity)
    {
        
        productDetailEntity.Author = string.IsNullOrWhiteSpace(productDetailUpdateDto.Author) ? productDetailEntity.Author : productDetailUpdateDto.Author;
        productDetailEntity.PublishDate = string.IsNullOrWhiteSpace(productDetailUpdateDto.PublishDate) ? productDetailEntity.PublishDate : productDetailUpdateDto.PublishDate;
        productDetailEntity.Publisher = string.IsNullOrWhiteSpace(productDetailUpdateDto.Publisher) ? productDetailEntity.Publisher : productDetailUpdateDto.Publisher;
        productDetailEntity.Language = string.IsNullOrWhiteSpace(productDetailUpdateDto.Language) ? productDetailEntity.Language : productDetailUpdateDto.Language;
        productDetailEntity.Size = string.IsNullOrWhiteSpace(productDetailUpdateDto.Size) ? productDetailEntity.Size : productDetailUpdateDto.Size;
        productDetailEntity.Page = string.IsNullOrWhiteSpace(productDetailUpdateDto.Page) ? productDetailEntity.Page : productDetailUpdateDto.Page;
        productDetailEntity.UpdatedAt = DateTime.Now;
    }
}