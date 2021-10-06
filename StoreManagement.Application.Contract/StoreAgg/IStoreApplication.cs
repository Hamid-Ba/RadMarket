using System.Collections.Generic;
using Framework.Application;
using System.Threading.Tasks;

namespace StoreManagement.Application.Contract.StoreAgg
{
    public interface IStoreApplication
    {
        Task<string> GetStoreCode(long id);
        Task<string> GetStoreName(long id);
        Task<IEnumerable<StoreVM>> GetAll();
        Task<OperationResult> Delete(long id);
        Task<BankStoreVM> GetBankInfoBy(long id);
        Task<EditStoreVM> GetDetailForEditBy(long id);
        Task<OperationResult> ProductCreated(long id);
        Task<AddressStoreVM> GetAddressInfoBy(long id);
        Task<OperationResult> Edit(EditStoreVM command);
        Task<OperationResult> IsAbleToAddProduct(long id);
        Task<OperationResult> IsStoreConfirmedBy(long id);
        Task<OperationResult> IsAccountStillCharged(long id);
        Task<OperationResult> IsAccountStillAdtCharged(long id);
        Task<OperationResult> IsStoreConfirmedBy(string code);
        Task<(OperationResult,long)> Create(CreateStoreVM command);
        Task<OperationResult> SendMessage(SendMessageStoreVM command);
        Task<OperationResult> ChangeStatus(SpecifyStatusOfStoreVM command);
    }
}