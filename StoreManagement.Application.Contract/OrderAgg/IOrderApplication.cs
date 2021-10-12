using Framework.Application;
using Framework.Domain;
using System.Threading.Tasks;

namespace StoreManagement.Application.Contract.OrderAgg
{
    public interface IOrderApplication
    {
        Task<long> CreateOrder(long userId);
        Task<string> GetIssueTrackingBy(long id);
        Task<OrderVM> GetLastOpenedOrder(long userId);
        Task<OperationResult> PlaceItem(PlaceItemVM command);
        Task<OperationResult> PlaceOrder(PlaceOrderVM command);
        Task<OperationResult> DeleteItemBy(long userId,long itemId);
        Task<ChangeOrderStatusVM> GetDetailForChangeStatus(long itemId);
        Task<OperationResult> AddProductToOpenOrder(AddOrderItemsVM command);
        Task<OperationResult> ChangeOrderStatus(ChangeOrderStatusVM command);
    }
}
