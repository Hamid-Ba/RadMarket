using Framework.Domain;
using StoreManagement.Application.Contract.OrderAgg;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StoreManagement.Domain.OrderAgg
{
    public interface IOrderItemRepository : IRepository<OrderItem>
    {
        Task<ChangeOrderStatusVM> GetDetailForChangeStatus(long itemId);
        Task<IEnumerable<OrderItemsVM>> GetItems(long id);
        Task<IEnumerable<OrderItemsVM>> GetAllUnPayedItems();
        Task<IEnumerable<OrderItemsVM>> GetAllPayedItems();
    }
}
