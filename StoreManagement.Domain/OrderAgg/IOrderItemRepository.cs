using Framework.Domain;
using StoreManagement.Application.Contract.OrderAgg;
using System.Threading.Tasks;

namespace StoreManagement.Domain.OrderAgg
{
    public interface IOrderItemRepository : IRepository<OrderItem>
    {
        Task<ChangeOrderStatusVM> GetDetailForChangeStatus(long itemId);
    }
}
