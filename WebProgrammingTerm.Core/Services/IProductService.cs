using System.Security.Claims;
using SharedLibrary.DTO;
using SharedLibrary.Models;
using WebProgrammingTerm.Core.DTO;

namespace WebProgrammingTerm.Core.Services;

public interface IProductService:IGenericService<Product>
{
    Task<CustomResponseDto<Product>> UpdateAsync(ProductUpdateDto productUpdateDto,ClaimsIdentity claimsIdentity);
    Task<CustomResponseDto<Product>> AddAsync(ProductAddDto productAddDto,ClaimsIdentity claimsIdentity);

    Task<Product> GetProductWithCompany(string productId);
}