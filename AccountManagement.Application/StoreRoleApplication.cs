using AccountManagement.Application.Contract.StoreRoleAgg;
using AccountManagement.Application.Contract.StoreRolePermissionAgg;
using AccountManagement.Domain.StoreRoleAgg;
using Framework.Application;
using System.Threading.Tasks;

namespace AccountManagement.Application
{
    public class StoreRoleApplication : IStoreRoleApplication
    {
        private readonly IStoreRoleRepository _storeRoleRepository;
        private readonly IStoreRolePermissionApplication _storeRolePermissionApplication;

        public StoreRoleApplication(IStoreRoleRepository storeRoleRepository, IStoreRolePermissionApplication storeRolePermissionApplication)
        {
            _storeRoleRepository = storeRoleRepository;
            _storeRolePermissionApplication = storeRolePermissionApplication;
        }

        public async Task<OperationResult> Create(CreateStoreRoleVM command)
        {
            OperationResult result = new();

            if (_storeRoleRepository.Exists(r => r.Name == command.Name)) return result.Failed(ApplicationMessage.DuplicatedModel);

            var role = new StoreRole(command.StoreId, command.Name, command.Description);

            await _storeRoleRepository.AddEntityAsync(role);
            await _storeRoleRepository.SaveChangesAsync();

            await _storeRolePermissionApplication.AddPermissionsToRole(role.Id, command.PermissionsId);

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

        public async Task<EditStoreRoleVM> GetDetailForEditBy(long id) => await _storeRoleRepository.GetDetailForEditBy(id);
        
    }
}
