using WebProgrammingTerm.Core.DTO;
using WebProgrammingTerm.Core.Models;

namespace WebProgrammingTerm.Core.Mappers;

public static class ProductDetailMapper
{

    
    public static ProductDetail toProductDetail(ProductDetailAddDto productDetailAddDto )
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
 
    public static ProductDetailAddDto toProductDetail(ProductAddDto productDetailAddDto )
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
}