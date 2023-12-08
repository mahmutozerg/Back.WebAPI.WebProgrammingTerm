using WebProgrammingTerm.Core.DTO;
using WebProgrammingTerm.Core.Models;

namespace WebProgrammingTerm.Core.Services;

public interface IOrderService:IGenericService<Order>
{
     Task<CustomResponseDto<UserFavorites>> AddAsync(OrderAddDto orderAddDto, string createdBy);
}