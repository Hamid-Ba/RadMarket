using AccountManagement.Infrastructure.EfCore;
using Framework.Application;
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
        private readonly AccountContext _accountContext;

        public OrderRepository(StoreContext context, AccountContext accountContext) : base(context)
        {
            _context = context;
            _accountContext = accountContext;
        }

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
            PayAmount = o.PayAmount,
            DiscountPrice = o.DiscountPrice
        }).ToList();

        public async Task<Order> GetLastOpenOrderBy(long userId) => await _context.Orders.Include(i => i.OrderItems).Where(o => o.UserId == userId && !o.IsPayed).FirstOrDefaultAsync();

        public async Task<long> GetUserIdBy(long orderId) => (await _context.Orders.FirstOrDefaultAsync(o => o.Id == orderId)).UserId;

        public async Task<IEnumerable<OrderVM>> GetAll()
        {
            var users = await _accountContext.User.Select(u => new { Id = u.Id, FullName = $"{u.FirstName} {u.LastName}" }).ToListAsync();

            var result = await _context.Orders.Where(o => o.IsPayed).Select(o => new OrderVM
            {
                Id = o.Id,
                UserId = o.UserId,
                IsPayed = o.IsPayed,
                DiscountPrice = o.DiscountPrice,
                MobileNumber = o.MobileNumber,
                PayAmount = o.PayAmount,
                PaymentMethod = o.PaymentMethod,
                IssueTrackingCode = o.IssueTracking,
                TotalPrice = o.TotalPrice,
                PalceOrderDate = o.PlaceOrderDate.ToFarsi(),
            }).AsNoTracking().ToListAsync();

            result.ForEach(r => r.UserFullName = users.Find(u => u.Id == r.UserId)?.FullName);

            return result;
        }

    }
}
