using System.Collections.Generic;
using Framework.Application;
using System.Threading.Tasks;

namespace StoreManagement.Application.Contract.ProductAgg
{
    public interface IProductApplication
    {
        Task<ProductVM> GetDetailBy(long id);
        Task<OperationResult> Delete(long id);
        Task<string> GetProductSlugBy(long id);
        Task<long> GetProductStoreIdBy(long id);
        Task<EditProductVM> GetDetailForEditBy(long id);
        Task<OperationResult> Edit(EditProductVM command);
        Task<OperationResult> Create(CreateProductVM command);
        Task<bool> IsProductBelongToStore(long id, long storeId);
        Task<IEnumerable<ProductVM>> GetAll(SearchStoreVM search);
        Task<OperationResult> ChangeStatus(ChangeStatusProductVM command);
        Task<IEnumerable<ProductVM>> GetAll(long storeId, SearchStoreVM search);
    }
}
