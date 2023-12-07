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
            Category = productDetailAddDto.Category,
            Page = productDetailAddDto.Page,
            PublishDate = productDetailAddDto.PublishDate,
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now,
        };

        return productDetail;
    }
 
}