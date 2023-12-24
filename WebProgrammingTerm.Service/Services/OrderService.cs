using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using SharedLibrary.DTO;
using SharedLibrary.Mappers;
using SharedLibrary.Models;
using WebProgrammingTerm.Core;

using WebProgrammingTerm.Core.Repositories;
using WebProgrammingTerm.Core.Services;
using WebProgrammingTerm.Core.UnitOfWorks;

namespace WebProgrammingTerm.Service.Services;

public class OrderService:GenericService<Order>,IOrderService
{
    private readonly IOrderRepository _orderRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IProductService _productService;
    private readonly ILocationService _locationService;
    private readonly IUserService _userService;
    public OrderService( IUnitOfWork unitOfWork, IOrderRepository orderRepository, IProductService productService, ILocationService locationService, IUserService userService) : base(orderRepository, unitOfWork)
    {
        _orderRepository = orderRepository;
        _productService = productService;
        _locationService = locationService;
        _userService = userService;
        _unitOfWork = unitOfWork;

    }


    public async Task<CustomResponseDto<Order>> AddAsync(OrderAddDto orderAddDto, ClaimsIdentity claimsIdentity)
    {
        var createdBy = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        
        var locationEntity = await _locationService.Where(l => l != null && l.Id == orderAddDto.LocationId && !l.IsDeleted).FirstOrDefaultAsync();
        
        if (locationEntity is null)
            throw new Exception(ResponseMessages.LocationNotFound);
        
        var userEntity = await _userService.GetUserWithOrders(createdBy);
        if (userEntity is null)
            throw new Exception(ResponseMessages.UserNotFound);
        
        var productList = new List<Product>();

        foreach (var productIdVariable in orderAddDto.ProductIdList.ToHashSet())
        {
            var product = await _productService.Where(p => p != null && p.Id == productIdVariable && !p.IsDeleted).FirstOrDefaultAsync();
            if (product is null)
            {
                productList.Clear();
                throw new Exception(ResponseMessages.ProductNotFound);
            }
            productList.Add(product);
        }
        
        
        var orderEntity = OrderMapper.ToOrder(orderAddDto);
        orderEntity.User = userEntity;
        orderEntity.Location = locationEntity;
        orderEntity.Products.AddRange(productList);
        orderEntity.CreatedBy = createdBy;
        orderEntity.UpdatedBy = createdBy;
        orderEntity.OrderDetail = new OrderDetail()
        {
            Tax = 0.18f,
            PaymentMethod = orderAddDto.PaymentCard
        };
        await _orderRepository.AddAsync(orderEntity);
        await _unitOfWork.CommitAsync();

        return CustomResponseDto<Order>.Success(orderEntity, ResponseCodes.Created);
    }

    public async Task<CustomResponseListDataDto<Order>> GetUserOrdersAsync(ClaimsIdentity claimsIdentity)
    {
        var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier)?.Value;


        var order = await _orderRepository
            .Where(o => o != null && o.UserId == userId && !o.IsDeleted)
            .Include(o=>o.OrderDetail)
            .Include(p=>p.Location)
            .Include(o=>o.Products)
            .ThenInclude(p=> p.ProductDetail)
            .OrderByDescending(o=>o.CreatedAt)
            .AsNoTracking()
            .ToListAsync();
        
        
        return CustomResponseListDataDto<Order>.Success(order,ResponseCodes.Ok);
    }
}