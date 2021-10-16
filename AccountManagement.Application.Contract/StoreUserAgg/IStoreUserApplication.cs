using Framework.Application;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AccountManagement.Application.Contract.StoreUserAgg
{
    public interface IStoreUserApplication
    {
        Task<long> GetStoreIdBy(long id);
        Task<string> GetAdminNameBy(long id);
        Task<IEnumerable<StoreUserVM>> GetAll();
        Task<OperationResult> Delete(long id,long storeId);
        Task<AddressStoreUserVM> GetAddressInfoBy(long id);
        Task<IEnumerable<StoreUserVM>> GetAll(long storeId);
        Task FillStoreId(long id,long storeId, string code);
        Task<OperationResult> Edit(EditStoreUserVM command);
        Task<OperationResult> Login(LoginStoreUserVM command);
        Task<EditStoreUserVM> GetDetailForEditBy(long id,long storeId);
        Task<(OperationResult,long)> Register(RegisterStoreUserVM command);
        Task<OperationResult> InitialStore(long id, long storeId,string storeCode);
    }
}
