using Framework.Domain;
using StoreManagement.Application.Contract.ProductAgg;
using System.Threading.Tasks;

namespace StoreManagement.Domain.ProductAgg
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<EditProductVM> GetDetailForEditBy(long id);
    }
}