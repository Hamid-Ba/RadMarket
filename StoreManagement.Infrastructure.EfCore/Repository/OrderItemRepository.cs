using Framework.Infrastructure;
using Microsoft.EntityFrameworkCore;
using StoreManagement.Application.Contract.OrderAgg;
using StoreManagement.Domain.OrderAgg;
using System.Linq;
using System.Threading.Tasks;

namespace StoreManagement.Infrastructure.EfCore.Repository
{
    public class OrderItemRepository : Repository<OrderItem>, IOrderItemRepository
    {
        private readonly StoreContext _context;

        public OrderItemRepository(StoreContext context) : base(context) => _context = context;

        public async Task<ChangeOrderStatusVM> GetDetailForChangeStatus(long itemId) => await _context.OrderItems.Include(o => o.Order).Select(o => new ChangeOrderStatusVM
        {
            ItemId = o.Id,
            Status = o.Status,
            IssueTracking = o.Order.IssueTracking,
            UserId = o.Order.UserId,
            OrderId = o.OrderId
        }
        ).FirstOrDefaultAsync(o => o.ItemId == itemId);

    }
}
