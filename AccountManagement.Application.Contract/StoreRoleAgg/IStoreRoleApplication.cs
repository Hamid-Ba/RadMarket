using Framework.Application;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AccountManagement.Application.Contract.StoreRoleAgg
{
    public interface IStoreRoleApplication
    {
        Task<OperationResult> Delete(long id,long storeId);
        Task<IEnumerable<StoreRoleVM>> GetAll(long storeId);
        Task<OperationResult> Edit(EditStoreRoleVM command);
        Task<OperationResult> Create(CreateStoreRoleVM command);
        Task<EditStoreRoleVM> GetDetailForEditBy(long id,long storeId);
        Task<OperationResult> CreateStoreAdminRole(long storeId, long userId);
    }
}
