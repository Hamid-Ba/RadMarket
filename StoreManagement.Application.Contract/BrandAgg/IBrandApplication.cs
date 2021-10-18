using Framework.Application;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StoreManagement.Application.Contract.BrandAgg
{
    public interface IBrandApplication
    {
        Task<IEnumerable<BrandVM>> GetAll(long storeId);
        Task<OperationResult> Edit(EditBrandVM command);
        Task<OperationResult> Delete(long id,long storeId);
        Task<OperationResult> Create(CreateBrandVM command);
        Task<EditBrandVM> GetDetailForEditBy(long id, long storeId);
    }
}
