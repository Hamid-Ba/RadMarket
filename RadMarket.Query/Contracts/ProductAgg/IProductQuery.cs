using System.Collections.Generic;
using System.Threading.Tasks;

namespace RadMarket.Query.Contracts.ProductAgg
{
    public interface IProductQuery
    {
        Task<ProductQueryVM> GetBy(string slug);
        Task<IEnumerable<ProductQueryVM>> GetAll(string filter,int take = 0);
    }
}
