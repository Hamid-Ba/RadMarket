﻿using AccountManagement.Application.Contract.StoreUserAgg;
using AccountManagement.Domain.StoreUserAgg;
using Framework.Application;
using Framework.Application.Authentication;
using Framework.Application.Hashing;
using System;
using System.Threading.Tasks;

namespace AccountManagement.Application
{
    public class StoreUserApplication : IStoreUserApplication
    {
        private readonly IAuthHelper _authHelper;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IStoreUserRepository _storeUserRepository;

        public StoreUserApplication(IAuthHelper authHelper, IPasswordHasher passwordHasher, IStoreUserRepository storeUserRepository)
        {
            _authHelper = authHelper;
            _passwordHasher = passwordHasher;
            _storeUserRepository = storeUserRepository;
        }

        public async Task<OperationResult> ConfirmUser(long id, long storeId)
        {
            OperationResult result = new();

            var storeUser = await _storeUserRepository.GetEntityByIdAsync(id);
            if (storeUser is null) return result.Failed(ApplicationMessage.UserNotExist);

            storeUser.ActiveAccount();
            var storeCode = storeUser.FillStoreId(storeId);

            await _storeUserRepository.SaveChangesAsync();

            //ToDo send SMS

            return result.Succeeded();
        }

        public async Task<OperationResult> Delete(long id)
        {
            OperationResult result = new();

            var storeUser = await _storeUserRepository.GetEntityByIdAsync(id);
            if (storeUser is null) return result.Failed(ApplicationMessage.NotExist);

            storeUser.Delete();
            await _storeUserRepository.SaveChangesAsync();

            return result.Succeeded();
        }

        public async Task<OperationResult> Edit(EditStoreUserVM command)
        {
            OperationResult result = new();

            var storeUser = await _storeUserRepository.GetEntityByIdAsync(command.Id);

            if (storeUser is null) return result.Failed(ApplicationMessage.NotExist);
            if (_storeUserRepository.Exists(s => s.Mobile == command.Mobile && s.Id != command.Id))
                return result.Failed(ApplicationMessage.DuplicatedMobile);

            string newPassword = null;
            if (!string.IsNullOrWhiteSpace(command.Password)) newPassword = _passwordHasher.Hash(command.Password);

            storeUser.Edit(command.FirstName, command.LastName, command.Mobile, newPassword, command.City, command.Province, command.Address);
            await _storeUserRepository.SaveChangesAsync();

            return result.Succeeded();
        }

        public Task<EditStoreUserVM> GetDetailForEditBy(long id)
        {
            throw new NotImplementedException();
        }

        public async Task<OperationResult> Login(LoginStoreUserVM command)
        {
            OperationResult result = new();

            var storeUser = await _storeUserRepository.GetUserBy(command.Mobile);
            if (storeUser is null) return result.Failed(ApplicationMessage.UserNotExist);

            if (command.StoreCode != storeUser.StoreCode) return result.Failed(ApplicationMessage.WrongStoreCode);

            var verification = _passwordHasher.Check(storeUser.Password, command.Password);
            if (!verification.Verified) return result.Failed(ApplicationMessage.WrongPassword);

            var authVM = new StoreUserAuthVM(storeUser.Id, storeUser.StoreId, storeUser.StoreCode, $"{storeUser.FirstName} {storeUser.LastName}", storeUser.Mobile,
                storeUser.City, storeUser.Province, storeUser.Address, true);

            _authHelper.SignIn(authVM);

            return result.Succeeded();
        }

        public async Task<OperationResult> Register(RegisterStoreUserVM command)
        {
            OperationResult result = new();

            if (_storeUserRepository.Exists(s => s.Mobile == command.Mobile))
                return result.Failed(ApplicationMessage.DuplicatedMobile);

            var password = _passwordHasher.Hash(command.Password);

            var storeUser = new StoreUser(0, command.FirstName, command.LastName, command.Mobile, password, command.City, command.Province, command.Address);

            await _storeUserRepository.AddEntityAsync(storeUser);
            await _storeUserRepository.SaveChangesAsync();

            //send sms

            return result.Succeeded();
        }
    }
}