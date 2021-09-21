using StoreManagement.Application.Contract.StoreAgg;
using System;
using System.Threading.Tasks;
using Framework.Domain;
using Framework.Application;
using StoreManagement.Domain.StoreAgg;

namespace StoreManagement.Application
{
    public class StoreApplication : IStoreApplication
    {
        private readonly IStoreRepository _storeRepository;

        public StoreApplication(IStoreRepository storeRepository) => _storeRepository = storeRepository;

        public async Task<OperationResult> ChangeStatus(SpecifyStatusOfStoreVM command)
        {
            OperationResult result = new();

            var store = await _storeRepository.GetEntityByIdAsync(command.Id);
            if (store is null) return result.Failed(ApplicationMessage.NotExist);

            store.ChangeStatus(command.Status, command.StoreGivenStatusReason);
            await _storeRepository.SaveChangesAsync();

            return result.Succeeded();
        }

        public async Task<(OperationResult, long)> Create(CreateStoreVM command)
        {
            OperationResult result = new();

            if (_storeRepository.Exists(s => (s.MobileNumber == command.MobileNumber || s.StoreAdminUserId == command.StoreAdminUserId)
            && s.Status == Framework.Domain.StoreStatus.Confirmed))
                return (result.Failed(ApplicationMessage.StoreOwnerHasAlreadyAStore), 0);

            var store = new Store(command.StoreAdminUserId, command.Name,command.AccountNumber,command.ShabaNumber,command.CardNumber,
                command.PhoneNumber, command.MobileNumber, 
                command.Province, command.City, command.Address,StoreStatus.UnderProgressed);

            await _storeRepository.AddEntityAsync(store);
            await _storeRepository.SaveChangesAsync();

            return (result.Succeeded(), store.Id);
        }

        public async Task<string> GetStoreCode(long id) => await _storeRepository.GetStoreCode(id);

        public async Task<OperationResult> Delete(long id)
        {
            OperationResult result = new();

            var store = await _storeRepository.GetEntityByIdAsync(id);
            if (store is null) return result.Failed(ApplicationMessage.NotExist);

            store.Delete();
            await _storeRepository.SaveChangesAsync();

            return result.Succeeded();
        }

        public async Task<OperationResult> Edit(EditStoreVM command)
        {
            OperationResult result = new();

            var store = await _storeRepository.GetEntityByIdAsync(command.Id);

            if (store is null) return result.Failed(ApplicationMessage.NotExist);
            if (_storeRepository.Exists(s => (s.MobileNumber == command.MobileNumber || s.StoreAdminUserId == command.StoreAdminUserId) && s.Id != command.Id))
                return result.Failed(ApplicationMessage.StoreOwnerHasAlreadyAStore);

            store.Edit(command.Name, command.PhoneNumber, command.MobileNumber, command.Description, command.Address);
            store.ChangeStatus(StoreStatus.UnderProgressed, "مشخصات فروشگاه تغییر یافت");

            await _storeRepository.SaveChangesAsync();

            return result.Succeeded();
        }

        public async Task<EditStoreVM> GetDetailForEditBy(long id) => await _storeRepository.GetDetailForEditBy(id);
    }
}