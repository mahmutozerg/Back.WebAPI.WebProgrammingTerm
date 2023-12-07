using WebProgrammingTerm.Core.DTO;
using WebProgrammingTerm.Core.Models;

namespace WebProgrammingTerm.Core.Services;

public interface IProductDetailService:IGenericService<ProductDetail>
{
    Task<CustomResponseDto<ProductDetail>> UpdateAsync(ProductDetailUpdateDto productDetailUpdateDto,string updatedBy);
    Task<CustomResponseDto<ProductDetail>> AddAsync(ProductDetailAddDto productDetailAddDto, string createdBy);
}