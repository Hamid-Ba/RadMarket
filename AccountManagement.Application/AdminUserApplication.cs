using AccountManagement.Application.Contract.AdminUserAgg;
using AccountManagement.Domain.AdminUserAgg;
using Framework.Application;
using Framework.Application.Hashing;
using System.Threading.Tasks;

namespace AccountManagement.Application
{
    public class AdminUserApplication : IAdminUserApplication
    {
        private readonly IPasswordHasher _passwordHasher;
        private readonly IAdminUserRepository _adminUserRepository;

        public AdminUserApplication(IPasswordHasher passwordHasher, IAdminUserRepository adminUserRepository)
        {
            _passwordHasher = passwordHasher;
            _adminUserRepository = adminUserRepository;
        }
        public async Task<OperationResult> Create(CreateAdminUserVM command)
        {
            OperationResult result = new();

            if (_adminUserRepository.Exists(a => a.Mobile == command.Mobile)) return result.Failed(ApplicationMessage.DuplicatedMobile);

            var password = _passwordHasher.Hash(command.Password);

            var user = new AdminUser(command.FirstName, command.LastName, command.Mobile, password);

            await _adminUserRepository.AddEntityAsync(user);
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

            user.Edit(command.FirstName, command.LastName, command.Mobile, newPassword);

            await _adminUserRepository.SaveChangesAsync();

            return result.Succeeded();
        }

        public async Task<EditAdminUserVM> GetDetailForEditBy(long id) => await _adminUserRepository.GetDetailForEditBy(id);
        
    }
}