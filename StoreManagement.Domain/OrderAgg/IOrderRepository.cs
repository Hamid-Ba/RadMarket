using Framework.Domain;
using StoreManagement.Application.Contract.OrderAgg;
using System.Threading.Tasks;

namespace StoreManagement.Domain.OrderAgg
{
    public interface IOrderRepository : IRepository<Order>
    {
        Task<Order> GetLastOpenOrderBy(long userId);
        Task<OrderVM> GetLastOpenOrderVMBy(long userId);
    }
}
