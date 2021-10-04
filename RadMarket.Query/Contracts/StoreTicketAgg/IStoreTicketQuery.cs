using System.Collections.Generic;
using System.Threading.Tasks;

namespace RadMarket.Query.Contracts.StoreTicketAgg
{
    public interface IStoreTicketQuery
    {
        Task<IEnumerable<StoreTicketQueryVM>> GetAllBy(long storeId);
    }
}