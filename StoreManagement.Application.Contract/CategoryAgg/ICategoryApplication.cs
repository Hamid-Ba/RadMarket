using System.Collections.Generic;
using Framework.Application;
using System.Threading.Tasks;

namespace StoreManagement.Application.Contract.CategoryAgg
{
    public interface ICategoryApplication
    {
        Task<OperationResult> Delete(long id);
        Task<IEnumerable<CategoryVM>> GetAll();
        Task<EditCategoryVM> GetDetailForEditBy(long id);
        Task<OperationResult> Edit(EditCategoryVM command);
        Task<OperationResult> Create(CreateCategoryVM command);
    }
}