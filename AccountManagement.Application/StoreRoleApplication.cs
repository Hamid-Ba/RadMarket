using AccountManagement.Application.Contract.StoreRoleAgg;
using AccountManagement.Application.Contract.StoreRolePermissionAgg;
using AccountManagement.Domain.StorePermissionAgg;
using AccountManagement.Domain.StoreRoleAgg;
using AccountManagement.Domain.StoreUserAgg;
using Framework.Application;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccountManagement.Application
{
    public class StoreRoleApplication : IStoreRoleApplication
    {
        private readonly IStoreRoleRepository _storeRoleRepository;
        private readonly IStoreUserRepository _storeUserRepository;
        private readonly IStorePermissionRepository _storePermissionRepository;
        private readonly IStoreRolePermissionApplication _storeRolePermissionApplication;

        public StoreRoleApplication(IStoreRoleRepository storeRoleRepository, IStoreUserRepository storeUserRepository, 
            IStorePermissionRepository storePermissionRepository, IStoreRolePermissionApplication storeRolePermissionApplication)
        {
            _storeRoleRepository = storeRoleRepository;
            _storeUserRepository = storeUserRepository;
            _storePermissionRepository = storePermissionRepository;
            _storeRolePermissionApplication = storeRolePermissionApplication;
        }

        public async Task<OperationResult> Create(CreateStoreRoleVM command)
        {
            OperationResult result = new();

            if (_storeRoleRepository.Exists(r => r.Name == command.Name && r.StoreId == command.StoreId))
                return result.Failed(ApplicationMessage.DuplicatedModel);

            var role = new StoreRole(command.StoreId, command.Name, command.Description);

            await _storeRoleRepository.AddEntityAsync(role);
            await _storeRoleRepository.SaveChangesAsync();

            await _storeRolePermissionApplication.AddPermissionsToRole(role.Id, command.PermissionsId);

            return result.Succeeded();
        }

        public async Task<OperationResult> CreateStoreAdminRole(long storeId, long userId)
        {
            OperationResult result = new();

            var storePermissions = await _storePermissionRepository.GetAllEntitiesAsync();
            var storePermissionsId = storePermissions.Select(p => p.Id).ToArray();

            await Create(new CreateStoreRoleVM
            {
                StoreId = storeId,
                Name = "مدیر شرکت",
                Description = "این نقش متعلق به مدیر شرکت می باشد",
                PermissionsId = storePermissionsId
            });

            var storeAdminRole =await _storeRoleRepository.GetAdminStoreRole(storeId);

            var admin = await _storeUserRepository.GetEntityByIdAsync(userId);
            admin.GiveRole(storeAdminRole.Id);

            await _storeUserRepository.SaveChangesAsync();

            return result.Succeeded();
        }

        public async Task<OperationResult> Delete(long id, long storeId)
        {
            OperationResult result = new();

            var role = await _storeRoleRepository.GetEntityByIdAsync(id);
            if (role is null) return result.Failed(ApplicationMessage.NotExist);
            if (role.StoreId != storeId) return result.Failed(ApplicationMessage.NotExist);

            role.Delete();
            await _storeRoleRepository.SaveChangesAsync();

            return result.Succeeded();
        }

        public async Task<OperationResult> Edit(EditStoreRoleVM command)
        {
            OperationResult result = new();

            var role = await _storeRoleRepository.GetEntityByIdAsync(command.Id);

            if (role is null) return result.Failed(ApplicationMessage.NotExist);
            if (_storeRoleRepository.Exists(r => r.Name == command.Name && r.Id != command.Id)) return result.Failed(ApplicationMessage.DuplicatedModel);

            role.Edit(command.Name, command.Description);

            await _storeRoleRepository.SaveChangesAsync();
            await _storeRolePermissionApplication.AddPermissionsToRole(role.Id, command.PermissionsId);

            return result.Succeeded();
        }

        public async Task<IEnumerable<StoreRoleVM>> GetAll(long storeId) => await _storeRoleRepository.GetAll(storeId);

        public async Task<EditStoreRoleVM> GetDetailForEditBy(long id,long storeId) => await _storeRoleRepository.GetDetailForEditBy(id,storeId);

        public bool IsRoleHasThePermission(long roleId, long permissionId)
        {
            var role = _storeRoleRepository.GetBy(roleId);

            var permissions = role.Permissions.Select(p => p.StorePermissionId).ToList();

            foreach (var perId in permissions) if (perId == permissionId) return true;

            return false;
        }
    }
}
