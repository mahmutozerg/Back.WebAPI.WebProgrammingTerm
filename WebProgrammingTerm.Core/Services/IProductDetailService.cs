using System.Security.Claims;
using SharedLibrary.DTO;
using SharedLibrary.Models;


namespace WebProgrammingTerm.Core.Services;

public interface IProductDetailService:IGenericService<ProductDetail>
{
    Task<CustomResponseDto<ProductDetail>> UpdateAsync(ProductDetailUpdateDto productDetailUpdateDto,ClaimsIdentity claimsIdentity);
    Task<ProductDetail> CreateAsync(ProductDetailAddDto productDetailAddDto, ClaimsIdentity claimsIdentity);
}