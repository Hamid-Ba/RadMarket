using System.Collections.Generic;
using System.Threading.Tasks;

namespace RadMarket.Query.Contracts.OrderAgg
{
    public interface IOrderQuery
    {
        Task<OrderQueryVM> GetBy(long userId);
        Task<double> GetTotalSiteProfit(long storeId);
        Task<List<ItemQueryVM>> GetUserItems(long orderId, long userId);
        Task<List<OrderQueryVM>> GetUserOrders(long userId, string code);
        Task<ItemQueryVM> GetUserItemsForStore(long itemId, long storeId);
        Task<IEnumerable<StoreOrderQueryVM>> GetUnPayedItems(long storeId);
        Task<List<StoreOrderQueryVM>> GetStoreOrders(long storeId, string code);
    }
}