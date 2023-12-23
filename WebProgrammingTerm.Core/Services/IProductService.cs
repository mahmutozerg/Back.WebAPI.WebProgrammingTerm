using System.Security.Claims;
using SharedLibrary.DTO;
using SharedLibrary.Models;

namespace WebProgrammingTerm.Core.Services;

public interface IProductService:IGenericService<Product>
{
    Task<CustomResponseDto<Product>> UpdateAsync(ProductUpdateDto productUpdateDto,ClaimsIdentity claimsIdentity);
    Task<CustomResponseDto<Product>> AddAsync(ProductAddDto productAddDto,ClaimsIdentity claimsIdentity);

    Task<Product> GetProductWithCompany(string productId);

    Task<CustomResponseListDataDto<ProductGetDto>> GetProductsByPage(int page);
    Task<CustomResponseListDataDto<ProductGetDto>> GetProductByName(int page,string name);
    
    Task<CustomResponseDto<ProductWCommentDto>> GetProductWithComments(string id);
    
    

}