using StoreManagement.Application.Contract.StoreAgg;
using System;
using System.Collections.Generic;
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

        public async Task<OperationResult> SendMessage(SendMessageStoreVM command)
        {
            OperationResult result = new();

            var store = await _storeRepository.GetEntityByIdAsync(command.Id);

            if (store is null) return result.Failed(ApplicationMessage.NotExist);
            if (string.IsNullOrWhiteSpace(store.MobileNumber))
                return result.Failed("شماره موبایلی برای این شرکت تریف نشده است");

            //ToDo Send Message

            return result.Succeeded();
        }

        public async Task<OperationResult> ChangeStatus(SpecifyStatusOfStoreVM command)
        {
            OperationResult result = new();

            var store = await _storeRepository.GetEntityByIdAsync(command.Id);
            if (store is null) return result.Failed(ApplicationMessage.NotExist);

            if (command.Status == StoreStatus.Rejected && string.IsNullOrWhiteSpace(command.StoreGivenStatusReason))
                command.StoreGivenStatusReason = "شرکت رد شد";

            store.ChangeStatus(command.Status, command.StoreGivenStatusReason);
            await _storeRepository.SaveChangesAsync();

            return result.Succeeded();
        }

        public async Task<OperationResult> IsStoreConfirmedBy(long id)
        {
            OperationResult result = new();

            var store = await _storeRepository.GetEntityByIdAsync(id);

            if (store is null) return result.Failed(ApplicationMessage.NotExist);
            if (store.Status != StoreStatus.Confirmed) return result.Failed("شرکت شما تایید نشده است!");

            return result.Succeeded();
        }

        public async Task<OperationResult> IsStoreConfirmedBy(string code)
        {
            OperationResult result = new();

            var store = await _storeRepository.GetStoreBy(code);

            if (store is null) return result.Failed(ApplicationMessage.NotExist);
            if (store.Status != StoreStatus.Confirmed) return result.Failed("شرکت شما تایید نشده است!");

            return result.Succeeded();
        }

        public async Task<(OperationResult, long)> Create(CreateStoreVM command)
        {
            OperationResult result = new();

            if (_storeRepository.Exists(s => (s.MobileNumber == command.MobileNumber || s.StoreAdminUserId == command.StoreAdminUserId)
            && s.Status == Framework.Domain.StoreStatus.Confirmed))
                return (result.Failed(ApplicationMessage.StoreOwnerHasAlreadyAStore), 0);

            var store = new Store(command.StoreAdminUserId, command.Name, command.AccountNumber, command.ShabaNumber, command.CardNumber,
                command.PhoneNumber, command.MobileNumber,
                command.Province, command.City, command.Address, StoreStatus.UnderProgressed);

            await _storeRepository.AddEntityAsync(store);
            await _storeRepository.SaveChangesAsync();

            return (result.Succeeded(), store.Id);
        }

        public async Task<string> GetStoreCode(long id) => await _storeRepository.GetStoreCode(id);

        public async Task<string> GetStoreName(long id) => await _storeRepository.GetStoreName(id);

        public async Task<IEnumerable<StoreVM>> GetAll() => await _storeRepository.GetAll();

        public async Task<OperationResult> Delete(long id)
        {
            OperationResult result = new();

            var store = await _storeRepository.GetEntityByIdAsync(id);
            if (store is null) return result.Failed(ApplicationMessage.NotExist);

            store.Delete();
            await _storeRepository.SaveChangesAsync();

            return result.Succeeded();
        }

        public async Task<BankStoreVM> GetBankInfoBy(long id) => await _storeRepository.GetBankInfo(id);

        public async Task<AddressStoreVM> GetAddressInfoBy(long id) => await _storeRepository.GetAddressInfo(id);

        public async Task<OperationResult> Edit(EditStoreVM command)
        {
            OperationResult result = new();

            var store = await _storeRepository.GetEntityByIdAsync(command.Id);

            if (store is null) return result.Failed(ApplicationMessage.NotExist);
            if (_storeRepository.Exists(s => (s.MobileNumber == command.MobileNumber || s.StoreAdminUserId == command.StoreAdminUserId) && s.Id != command.Id))
                return result.Failed(ApplicationMessage.StoreOwnerHasAlreadyAStore);

            store.Edit(command.Name, command.PhoneNumber, command.MobileNumber, command.AccountNumber, command.ShabaNumber, command.CardNumber
                , command.Description, command.Province, command.City, command.Address);
            store.ChangeStatus(StoreStatus.UnderProgressed, "مشخصات فروشگاه تغییر یافت");

            await _storeRepository.SaveChangesAsync();

            return result.Succeeded();
        }

        public async Task<EditStoreVM> GetDetailForEditBy(long id) => await _storeRepository.GetDetailForEditBy(id);

        public Task<OperationResult> ProductCreated(long id)
        {
            throw new NotImplementedException();
        }

        public async Task<OperationResult> IsAbleToAddProduct(long id)
        {
            OperationResult result = new();

            var store = await _storeRepository.GetEntityByIdAsync(id);
            if (store is null) return result.Failed(ApplicationMessage.NotExist);

            if (!store.IsAbleToAddProduct()) return result.Failed("شارژ حساب شما برای اضافه کردن محصول به اتمام رسیده است");

            return result.Succeeded();
        }

        public async Task<OperationResult> IsAccountStillCharged(long id)
        {
            OperationResult result = new();

            var store = await _storeRepository.GetEntityByIdAsync(id);
            if (store is null) return result.Failed(ApplicationMessage.NotExist);

            if (!store.IsAccountStillCharged()) return result.Failed("شارژ حساب شما به اتمام رسیده است");

            return result.Succeeded("حساب شما هنوز دارای اعتبار هست");
        }

        public async Task<OperationResult> IsAccountStillAdtCharged(long id)
        {
            OperationResult result = new();

            var store = await _storeRepository.GetEntityByIdAsync(id);
            if (store is null) return result.Failed(ApplicationMessage.NotExist);

            if (!store.IsAccountStillAdtCharged()) return result.Failed("شارژ حساب شما برای تبلیغات به اتمام رسیده است");

            return result.Succeeded("شرکت و محصولات شما در حال تبلیغات هست");
        }
    }
}