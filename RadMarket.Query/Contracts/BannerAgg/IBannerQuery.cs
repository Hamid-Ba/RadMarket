using System.Collections.Generic;
using System.Threading.Tasks;

namespace RadMarket.Query.Contracts.BannerAgg
{
    public interface IBannerQuery
    {
        Task<IEnumerable<BannerQueryVM>> GetForAdvertising(int take = 3);
    }
}
