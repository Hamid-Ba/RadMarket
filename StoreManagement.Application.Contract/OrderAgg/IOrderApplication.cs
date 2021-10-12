using Framework.Application;
using System.Threading.Tasks;

namespace StoreManagement.Application.Contract.OrderAgg
{
    public interface IOrderApplication
    {
        Task<long> CreateOrder(long userId);
        Task<string> GetIssueTrackingBy(long id);
        Task<OrderVM> GetLastOpenedOrder(long userId);
        Task<OperationResult> PlaceOrder(CreateOrderVM command);
        Task<OperationResult> DeleteItemBy(long userId,long itemId);
        Task<OperationResult> AddProductToOpenOrder(AddOrderItemsVM command);
    }
}
