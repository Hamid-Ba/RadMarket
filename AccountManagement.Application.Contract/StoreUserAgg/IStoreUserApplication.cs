using Framework.Application;
using System.Threading.Tasks;

namespace AccountManagement.Application.Contract.StoreUserAgg
{
    public interface IStoreUserApplication
    {
        Task<OperationResult> Delete(long id);
        Task<EditStoreUserVM> GetDetailForEditBy(long id);
        Task<OperationResult> Edit(EditStoreUserVM command);
        Task<OperationResult> Register(RegisterStoreUserVM command);
    }
}
