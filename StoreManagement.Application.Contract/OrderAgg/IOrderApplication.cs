using Framework.Application;
using System.Threading.Tasks;

namespace StoreManagement.Application.Contract.OrderAgg
{
    public interface IOrderApplication
    {
        Task<long> CreateOrder(long userId);
        Task<long> PlaceOrder(CreateOrderVM command);
        Task<OrderVM> GetLastOpenedOrder(long userId);
        Task<OperationResult> AddProductToOpenOrder(AddOrderItemsVM command);
    }
}
