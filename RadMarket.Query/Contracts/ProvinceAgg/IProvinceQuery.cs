using System.Collections.Generic;
using System.Threading.Tasks;

namespace RadMarket.Query.Contracts.ProvinceAgg
{
    public interface IProvinceQuery
    {
        Task<IEnumerable<ProvinceQueryVM>> GetAll();
    }
}
