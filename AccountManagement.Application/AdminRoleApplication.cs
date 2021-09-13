using AccountManagement.Application.Contract.AdminRoleAgg;
using AccountManagement.Domain.AdminRoleAgg;
using Framework.Application;
using System.Threading.Tasks;

namespace AccountManagement.Application
{
    public class AdminRoleApplication : IAdminRoleApplication
    {
        private readonly IAdminRoleRepository _adminRoleRepository;

        public AdminRoleApplication(IAdminRoleRepository adminRoleRepository) => _adminRoleRepository = adminRoleRepository;

        public async Task<OperationResult> Create(CreateAdminRoleVM command)
        {
            OperationResult result = new();

            if (_adminRoleRepository.Exists(a => a.Name == command.Name))
                return result.Failed(ApplicationMessage.DuplicatedModel);

            var role = new AdminRole(command.Name, command.Description);

            await _adminRoleRepository.AddEntityAsync(role);
            await _adminRoleRepository.SaveChangesAsync();

            return result.Succeeded();
        }

        public async Task<OperationResult> Delete(long id)
        {
            OperationResult result = new();

            var role = await _adminRoleRepository.GetEntityByIdAsync(id);

            if (role is null) return result.Failed(ApplicationMessage.NotExist);

            role.Delete();
            await _adminRoleRepository.SaveChangesAsync();

            return result.Succeeded();
        }

        public async Task<OperationResult> Edit(EditAdminRoleVM command)
        {
            OperationResult result = new();

            var role = await _adminRoleRepository.GetEntityByIdAsync(command.Id);

            if (role is null) return result.Failed(ApplicationMessage.NotExist);
            if (_adminRoleRepository.Exists(a => a.Name == command.Name && a.Id != command.Id))
                return result.Failed(ApplicationMessage.DuplicatedModel);

            role.Edit(command.Name, command.Description);
            await _adminRoleRepository.SaveChangesAsync();

            return result.Succeeded();
        }

        public async Task<EditAdminRoleVM> GetDetailForEditBy(long id) => await _adminRoleRepository.GetDetailForEditBy(id);
        
    }
}