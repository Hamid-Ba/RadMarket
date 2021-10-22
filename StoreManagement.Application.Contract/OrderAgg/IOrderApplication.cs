using Framework.Application;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StoreManagement.Application.Contract.OrderAgg
{
    public interface IOrderApplication
    {
        Task<IEnumerable<OrderVM>> GetAll();
        Task<long> CreateOrder(long userId);
        Task<long> GetUserIdBy(long orderId);
        Task<string> GetIssueTrackingBy(long id);
        Task<OrderVM> GetLastOpenedOrder(long userId);
        Task<IEnumerable<OrderItemsVM>> GetItems(long id);
        Task<IEnumerable<OrderItemsVM>> GetAllPayedItems();
        Task<IEnumerable<OrderItemsVM>> GetAllUnPayedItems();
        Task<OperationResult> PlaceItem(PlaceItemVM command);
        Task<OperationResult> PlaceOrder(PlaceOrderVM command);
        Task<OperationResult> PayedProfitToSite(long[] itemsId);
        Task<OperationResult> DeleteItemBy(long userId,long itemId);
        Task<ChangeOrderStatusVM> GetDetailForChangeStatus(long itemId);
        Task<OperationResult> AddProductToOpenOrder(AddOrderItemsVM command);
        Task<OperationResult> ChangeOrderStatus(ChangeOrderStatusVM command);
    }
}
