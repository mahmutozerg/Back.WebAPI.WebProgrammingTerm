using System.Security.Claims;
using SharedLibrary.DTO;
using SharedLibrary.Models;


namespace WebProgrammingTerm.Core.Services;

public interface IOrderService:IGenericService<Order>
{
     Task<CustomResponseDto<Order>> AddAsync(OrderAddDto orderAddDto, ClaimsIdentity claimsIdentity);
}