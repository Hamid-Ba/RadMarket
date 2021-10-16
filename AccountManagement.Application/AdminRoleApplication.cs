using AccountManagement.Application.Contract.AdminRoleAgg;
using AccountManagement.Application.Contract.AdminRolePermissionAgg;
using AccountManagement.Domain.AdminRoleAgg;
using AccountManagement.Domain.AdminRolePermissionAgg;
using Framework.Application;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AccountManagement.Application
{
    public class AdminRoleApplication : IAdminRoleApplication
    {
        private readonly IAdminRoleRepository _adminRoleRepository;
        private readonly IAdminRolePermissionApplication _adminRolePermissionApplication;

        public AdminRoleApplication(IAdminRoleRepository adminRoleRepository, IAdminRolePermissionApplication adminRolePermissionApplication)
        {
            _adminRoleRepository = adminRoleRepository;
            _adminRolePermissionApplication = adminRolePermissionApplication;
        }

        public async Task<OperationResult> Create(CreateAdminRoleVM command)
        {
            OperationResult result = new();

            if (_adminRoleRepository.Exists(a => a.Name == command.Name))
                return result.Failed(ApplicationMessage.DuplicatedModel);

            var role = new AdminRole(command.Name, command.Description);

            await _adminRoleRepository.AddEntityAsync(role);
            await _adminRoleRepository.SaveChangesAsync();

            await _adminRolePermissionApplication.AddPermissionsToRole(role.Id, command.PermissionsId);

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

            await _adminRolePermissionApplication.AddPermissionsToRole(role.Id, command.PermissionsId);

            return result.Succeeded();
        }

        public async Task<IEnumerable<AdminRoleVM>> GetAll() => await _adminRoleRepository.GetAll();

        public async Task<EditAdminRoleVM> GetDetailForEditBy(long id) => await _adminRoleRepository.GetDetailForEditBy(id);
        
    }
}