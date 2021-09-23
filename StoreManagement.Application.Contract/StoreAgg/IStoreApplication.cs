using System.Collections.Generic;
using Framework.Application;
using System.Threading.Tasks;

namespace StoreManagement.Application.Contract.StoreAgg
{
    public interface IStoreApplication
    {
        Task<string> GetStoreCode(long id);
        Task<IEnumerable<StoreVM>> GetAll();
        Task<OperationResult> Delete(long id);
        Task<EditStoreVM> GetDetailForEditBy(long id);
        Task<OperationResult> Edit(EditStoreVM command);
        Task<OperationResult> IsStoreConfirmedBy(long id);
        Task<OperationResult> IsStoreConfirmedBy(string code);
        Task<(OperationResult,long)> Create(CreateStoreVM command);
        Task<OperationResult> ChangeStatus(SpecifyStatusOfStoreVM command);
    }
}
