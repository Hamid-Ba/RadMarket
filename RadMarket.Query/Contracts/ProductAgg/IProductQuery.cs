using System.Collections.Generic;
using System.Threading.Tasks;

namespace RadMarket.Query.Contracts.ProductAgg
{
    public interface IProductQuery
    {
        Task<ProductQueryVM> GetBy(long storeId,string slug);
        Task<IEnumerable<ProductQueryVM>> GetAll(string filter, string searchTitle = "", int take = 0);
        Task<IEnumerable<ProductQueryVM>> GetAllBy(long storeId,int take = 0);
        Task<IEnumerable<ProductQueryVM>> GetBy(long categoryId, long? productBeRemovedById);
    }
}
