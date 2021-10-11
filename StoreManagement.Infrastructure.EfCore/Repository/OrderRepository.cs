using Framework.Infrastructure;
using Microsoft.EntityFrameworkCore;
using StoreManagement.Application.Contract.OrderAgg;
using StoreManagement.Domain.OrderAgg;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreManagement.Infrastructure.EfCore.Repository
{
    public class OrderRepository : Repository<Order>, IOrderRepository
    {
        private readonly StoreContext _context;

        public OrderRepository(StoreContext context) : base(context) => _context = context;

        public async Task<OrderVM> GetLastOpenOrderVMBy(long userId) => await _context.Orders.Include(i => i.OrderItems).Where(o => o.UserId == userId && !o.IsPayed).Select(o => new OrderVM
        {
            Id = o.Id,
            UserId = o.UserId,
            Items = MapItems(o.OrderItems)
        }).AsNoTracking().FirstOrDefaultAsync();

        private static List<OrderItemsVM> MapItems(List<OrderItem> orderItems) => orderItems.Select(o => new OrderItemsVM
        {
            Id = o.Id,
            OrderId = o.OrderId,
            ProductId = o.ProductId,
            Count = o.Count,
            DiscountRate = o.DiscountRate,
            UnitPrice = o.UnitPrice
        }).ToList();

        public async Task<Order> GetLastOpenOrderBy(long userId) => await _context.Orders.Include(i => i.OrderItems).Where(o => o.UserId == userId && !o.IsPayed).FirstOrDefaultAsync();
    }
}
