using Framework.Domain;
using StoreManagement.Application.Contract.CategoryAgg;
using System.Threading.Tasks;

namespace StoreManagement.Domain.CategoryAgg
{
    public interface ICategoryRepository : IRepository<Category>
    {
        Task<EditCategoryVM> GetDetailForEditBy(long id);
    }
}
