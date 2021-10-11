using Framework.Infrastructure;
using StoreManagement.Domain.OrderAgg;

namespace StoreManagement.Infrastructure.EfCore.Repository
{
    public class OrderItemRepository : Repository<OrderItem>, IOrderItemRepository
    {
        private readonly StoreContext _context;

        public OrderItemRepository(StoreContext context) : base(context) => _context = context;
    }
}
