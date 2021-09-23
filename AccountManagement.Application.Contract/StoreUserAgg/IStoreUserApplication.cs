using Framework.Application;
using System.Threading.Tasks;

namespace AccountManagement.Application.Contract.StoreUserAgg
{
    public interface IStoreUserApplication
    {
        Task<string> GetAdminNameBy(long id);
        Task<OperationResult> Delete(long id);
        Task<EditStoreUserVM> GetDetailForEditBy(long id);
        Task FillStoreId(long id,long storeId, string code);
        Task<OperationResult> Edit(EditStoreUserVM command);
        Task<OperationResult> Login(LoginStoreUserVM command);
        Task<OperationResult> InitialStore(long id, long storeId,string storeCode);
        Task<(OperationResult,long)> Register(RegisterStoreUserVM command);
    }
}
