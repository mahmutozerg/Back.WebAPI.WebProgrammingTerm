using WebProgrammingTerm.Core.DTO;
using WebProgrammingTerm.Core.Models;
using WebProgrammingTerm.Core.Repositories;
using WebProgrammingTerm.Core.Services;
using WebProgrammingTerm.Core.UnitOfWorks;

namespace WebProgrammingTerm.Service.Services;

public class OrderService:GenericService<Order>,IOrderService
{
    private readonly IOrderRepository _orderRepository;
    private readonly IUnitOfWork _unitOfWork;
    public OrderService( IUnitOfWork unitOfWork, IOrderRepository orderRepository) : base(orderRepository, unitOfWork)
    {
        _orderRepository = orderRepository;
        _unitOfWork = unitOfWork;

    }


    public Task<CustomResponseDto<UserFavorites>> AddAsync(OrderAddDto orderAddDto, string createdBy)
    {
        throw new NotImplementedException();
    }
}