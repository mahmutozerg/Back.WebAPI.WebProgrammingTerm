using System.Security.Claims;
using WebProgrammingTerm.Core.DTO;
using WebProgrammingTerm.Core.Models;

namespace WebProgrammingTerm.Core.Services;

public interface IOrderService:IGenericService<Order>
{
     Task<CustomResponseDto<Order>> AddAsync(OrderAddDto orderAddDto, ClaimsIdentity claimsIdentity);
}