using System.Collections.Generic;
using System.Threading.Tasks;

namespace RadMarket.Query.Contracts.StoreAgg
{
    public interface IStoreQuery
    {
        Task<IEnumerable<StoreQueryVM>> GetAll();
    }
}