using System.Security.Claims;
using WebProgrammingTerm.Core.DTO;
using WebProgrammingTerm.Core.Models;

namespace WebProgrammingTerm.Core.Services;

public interface IProductService:IGenericService<Product>
{
    Task<CustomResponseDto<Product>> UpdateAsync(ProductUpdateDto productUpdateDto,ClaimsIdentity claimsIdentity);
    Task<CustomResponseDto<Product>> AddAsync(ProductAddDto productAddDto,ClaimsIdentity claimsIdentity);

    Task<Product> GetProductWithCompany(string productId);
}