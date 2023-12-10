using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using WebProgrammingTerm.Core;
using WebProgrammingTerm.Core.DTO;
using WebProgrammingTerm.Core.Mappers;
using WebProgrammingTerm.Core.Models;
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
    public OrderService( IUnitOfWork unitOfWork, IOrderRepository orderRepository, IProductService productService, ILocationService locationService) : base(orderRepository, unitOfWork)
    {
        _orderRepository = orderRepository;
        _productService = productService;
        _locationService = locationService;
        _unitOfWork = unitOfWork;

    }


    public async Task<CustomResponseDto<Order>> AddAsync(OrderAddDto orderAddDto, ClaimsIdentity claimsIdentity)
    {
        var createdBy = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier)?.Value;

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

        var locationEntity = await _locationService.Where(l => l != null && l.Id == orderAddDto.LocationId && !l.IsDeleted).FirstOrDefaultAsync();
        if (locationEntity is null)
            throw new Exception(ResponseMessages.LocationNotFound);
            
        
        var orderEntity = OrderMapper.ToOrder(orderAddDto);
        orderEntity.Location = locationEntity;
        orderEntity.Products.AddRange(productList);
        orderEntity.CreatedBy = createdBy;
        orderEntity.UpdatedBy = createdBy;
        await _orderRepository.AddAsync(orderEntity);
        await _unitOfWork.CommitAsync();

        return CustomResponseDto<Order>.Success(orderEntity, ResponseCodes.Created);
    }
}