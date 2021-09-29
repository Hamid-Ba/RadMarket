using System.Collections.Generic;
using System.Threading.Tasks;

namespace RadMarket.Query.Contracts.CategoryAgg
{
    public interface ICategoryQuery
    {
        Task<IEnumerable<CategoryQueryVM>> GetAll();
    }
}
