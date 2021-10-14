using Framework.Domain;
using StoreManagement.Application.Contract.OrderAgg;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StoreManagement.Domain.OrderAgg
{
    public interface IOrderRepository : IRepository<Order>
    {
        Task<IEnumerable<OrderVM>> GetAll();
        Task<long> GetUserIdBy(long orderId);
        Task<Order> GetLastOpenOrderBy(long userId);
        Task<OrderVM> GetLastOpenOrderVMBy(long userId);
    }
}
