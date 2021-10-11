using System.Collections.Generic;
using Framework.Domain;
using StoreManagement.Application.Contract.ProductAgg;
using System.Threading.Tasks;

namespace StoreManagement.Domain.ProductAgg
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<ProductVM> GetDetailBy(long id);
        Task<EditProductVM> GetDetailForEditBy(long id);
        Task<bool> IsProductBelongToStore(long id, long storeId);
        Task<IEnumerable<ProductVM>> GetAll(SearchStoreVM search);
        Task<IEnumerable<ProductVM>> GetAll(long storeId, SearchStoreVM search);
        Task<string> GetProductSlugBy(long id);
        Task<long> GetProductStoreIdBy(long id);
    }
}