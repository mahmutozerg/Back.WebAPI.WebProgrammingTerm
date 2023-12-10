
using Microsoft.EntityFrameworkCore;
using WebProgrammingTerm.Core.Models;
using WebProgrammingTerm.Core.Repositories;

namespace WebProgrammingTerm.Repository.Repositories;

public class OrderRepository:GenericRepository<Order>,IOrderRepository
{
    private readonly DbSet<Order> _orders;
    public OrderRepository(AppDbContext context) : base(context)
    {
        _orders = context.Set<Order>();
    }
}