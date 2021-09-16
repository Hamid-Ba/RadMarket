using Framework.Application;
using System.Threading.Tasks;

namespace StoreManagement.Application.Contract.StoreAgg
{
    public interface IStoreApplication
    {
        Task<OperationResult> Delete(long id);
        Task<EditStoreVM> GetDetailForEditBy(long id);
        Task<OperationResult> Edit(EditStoreVM command);
        Task<OperationResult> Create(CreateStoreVM command);
        Task<OperationResult> ChangeStatus(SpecifyStatusOfStoreVM command);
    }
}
