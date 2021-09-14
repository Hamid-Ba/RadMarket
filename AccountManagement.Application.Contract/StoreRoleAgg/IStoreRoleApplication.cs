using Framework.Application;
using System.Threading.Tasks;

namespace AccountManagement.Application.Contract.StoreRoleAgg
{
    public interface IStoreRoleApplication
    {
        //Task<OperationResult> Delete(long id);
        Task<EditStoreRoleVM> GetDetailForEditBy(long id);
        Task<OperationResult> Edit(EditStoreRoleVM command);
        Task<OperationResult> Create(CreateStoreRoleVM command);
    }
}
