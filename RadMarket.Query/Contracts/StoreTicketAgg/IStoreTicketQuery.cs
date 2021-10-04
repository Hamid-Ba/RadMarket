using System.Collections.Generic;
using System.Threading.Tasks;

namespace RadMarket.Query.Contracts.StoreTicketAgg
{
    public interface IStoreTicketQuery
    {
        Task<StoreTicketQueryVM> GetBy(long id,long storeId);
        Task<IEnumerable<StoreTicketQueryVM>> GetAllBy(long storeId);
    }
}