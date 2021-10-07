using Framework.Domain;
using StoreManagement.Application.Contract.OrderAgg;
using System.Threading.Tasks;

namespace StoreManagement.Domain.OrderAgg
{
    public interface IOrderRepository : IRepository<Order>
    {
        Task<OrderVM> GetLastOpenOrderBy(long userId);
    }
}
