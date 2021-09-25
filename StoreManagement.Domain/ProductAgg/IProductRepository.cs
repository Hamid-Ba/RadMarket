using System.Collections.Generic;
using Framework.Domain;
using StoreManagement.Application.Contract.ProductAgg;
using System.Threading.Tasks;

namespace StoreManagement.Domain.ProductAgg
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<IEnumerable<ProductVM>> GetAll();
        Task<EditProductVM> GetDetailForEditBy(long id);
    }
}