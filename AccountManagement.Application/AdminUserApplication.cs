using AccountManagement.Application.Contract.AdminUserAgg;
using AccountManagement.Domain.AdminUserAgg;
using Framework.Application;
using Framework.Application.Authentication;
using Framework.Application.Hashing;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AccountManagement.Application
{
    public class AdminUserApplication : IAdminUserApplication
    {
        private readonly IAuthHelper _authHelper;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IAdminUserRepository _adminUserRepository;

        public AdminUserApplication(IAuthHelper authHelper, IPasswordHasher passwordHasher, IAdminUserRepository adminUserRepository)
        {
            _authHelper = authHelper;
            _passwordHasher = passwordHasher;
            _adminUserRepository = adminUserRepository;
        }

        public OperationResult Logout()
        {
            OperationResult result = new();

            _authHelper.SignOut();

            return result.Succeeded();
        }

        public async Task<OperationResult> Create(CreateAdminUserVM command)
        {
            OperationResult result = new();

            if (_adminUserRepository.Exists(a => a.Mobile == command.Mobile)) return result.Failed(ApplicationMessage.DuplicatedMobile);

            var password = _passwordHasher.Hash(command.Password);

            var user = new AdminUser(command.AdminRoleId,command.FirstName, command.LastName, command.Mobile, password);

            await _adminUserRepository.AddEntityAsync(user);
            await _adminUserRepository.SaveChangesAsync();

            return result.Succeeded();
        }

        public async Task<OperationResult> Delete(long id)
        {
            OperationResult result = new();

            var user = await _adminUserRepository.GetEntityByIdAsync(id);
            if (user is null) return result.Failed(ApplicationMessage.UserNotExist);

            user.Delete();
            await _adminUserRepository.SaveChangesAsync();

            return result.Succeeded();
        }

        public async Task<OperationResult> Edit(EditAdminUserVM command)
        {
            OperationResult result = new();

            var user = await _adminUserRepository.GetEntityByIdAsync(command.Id);

            if (user is null) return result.Failed(ApplicationMessage.UserNotExist);
            if (_adminUserRepository.Exists(a => a.Mobile == command.Mobile && a.Id != command.Id)) return result.Failed(ApplicationMessage.DuplicatedMobile);

            string newPassword = null;
            if (!string.IsNullOrWhiteSpace(command.Password)) newPassword = _passwordHasher.Hash(command.Password);

            user.Edit(command.AdminRoleId,command.FirstName, command.LastName, command.Mobile, newPassword);

            await _adminUserRepository.SaveChangesAsync();

            return result.Succeeded();
        }

        public async Task<OperationResult> Login(LoginAdminUserVM command)
        {
            OperationResult result = new();

            var user = await _adminUserRepository.GetUserBy(command.Mobile);
            if (user is null) return result.Failed(ApplicationMessage.UserNotExist);

            var userAuthVm = new AdminUserAuthVM(user.Id,user.AdminRoleId, $"{user.FirstName} {user.LastName}", user.Mobile, true);
            
            _authHelper.SignIn(userAuthVm);

            return result.Succeeded();
        }

        public async Task<EditAdminUserVM> GetDetailForEditBy(long id) => await _adminUserRepository.GetDetailForEditBy(id);

        public async Task<IEnumerable<AdminUserVM>> GetAll() => await _adminUserRepository.GetAll();
    }
}