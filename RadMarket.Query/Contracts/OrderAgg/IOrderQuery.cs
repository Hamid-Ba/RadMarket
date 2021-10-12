using System.Collections.Generic;
using System.Threading.Tasks;

namespace RadMarket.Query.Contracts.OrderAgg
{
    public interface IOrderQuery
    {
        Task<OrderQueryVM> GetBy(long userId);
        Task<List<ItemQueryVM>> GetUserOrders(long userId,string code);
        Task<List<StoreOrderQueryVM>> GetStoreOrders(long storeId,string code);
    }
}