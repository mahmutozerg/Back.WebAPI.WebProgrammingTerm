using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SharedLibrary.DTO;
using WebProgrammingTerm.Core.DTO;
using WebProgrammingTerm.Core.Services;

namespace WebProgrammingTerm.API.Controllers;

[Authorize]
public class OrderController:CustomControllerBase
{
    private readonly IOrderService _orderService;

    public OrderController(IOrderService orderService)
    {
        _orderService = orderService;
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> Add(OrderAddDto orderAddDto)
    {
        return CreateActionResult(await _orderService.AddAsync(orderAddDto,(ClaimsIdentity)User.Identity));
    }
}