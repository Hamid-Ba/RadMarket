using System.Collections.Generic;
using Framework.Domain;
using StoreManagement.Application.Contract.CategoryAgg;
using System.Threading.Tasks;

namespace StoreManagement.Domain.CategoryAgg
{
    public interface ICategoryRepository : IRepository<Category>
    {
        Task<IEnumerable<CategoryVM>> GetAll();
        Task<EditCategoryVM> GetDetailForEditBy(long id);
    }
}
