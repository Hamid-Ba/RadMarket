using System.Collections.Generic;
using Framework.Application;
using System.Threading.Tasks;

namespace StoreManagement.Application.Contract.ProductAgg
{
    public interface IProductApplication
    {
        Task<IEnumerable<ProductVM>> GetAll();
        Task<OperationResult> Delete(long id);
        Task<EditProductVM> GetDetailForEditBy(long id);
        Task<OperationResult> Edit(EditProductVM command);
        Task<OperationResult> Create(CreateProductVM command);
    }
}
