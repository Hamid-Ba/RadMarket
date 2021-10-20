using System.Collections.Generic;
using System.Threading.Tasks;

namespace RadMarket.Query.Contracts.StoreAgg
{
    public interface IStoreQuery
    {
        Task<StoreQueryVM> GetBy(long id);
        Task<IEnumerable<StoreQueryVM>> GetAll();
    }
}