using WebProgrammingTerm.Core.DTO;
using WebProgrammingTerm.Core.Models;

namespace WebProgrammingTerm.Core.Services;

public interface IProductService:IGenericService<Product>
{
    Task<CustomResponseDto<Product>> UpdateAsync(ProductUpdateDto productUpdateDto,string updatedBy);
    Task<CustomResponseDto<Product>> AddAsync(ProductAddDto productAddDto, string createdBy);

}