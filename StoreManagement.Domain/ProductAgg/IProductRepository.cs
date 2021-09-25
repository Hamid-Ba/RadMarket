using System.Collections.Generic;
using Framework.Domain;
using StoreManagement.Application.Contract.ProductAgg;
using System.Threading.Tasks;

namespace StoreManagement.Domain.ProductAgg
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<IEnumerable<ProductVM>> GetAll(SearchStoreVM search);
        Task<EditProductVM> GetDetailForEditBy(long id);
        Task<IEnumerable<ProductVM>> GetAll(long storeId, SearchStoreVM search);
    }
}