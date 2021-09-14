using AccountManagement.Application.Contract.StoreRolePermissionAgg;
using AccountManagement.Domain.StoreRolePermissionAgg;
using Framework.Application;
using System.Linq;
using System.Threading.Tasks;

namespace AccountManagement.Application
{
    public class StoreRolePermissionApplication : IStoreRolePermissionApplication
    {
        private readonly IStoreRolePermissionRepository _storeRolePermissionRepository;

        public StoreRolePermissionApplication(IStoreRolePermissionRepository storeRolePermissionRepository) => _storeRolePermissionRepository = storeRolePermissionRepository;

        public async Task<OperationResult> AddPermissionsToRole(long roleId, long[] permissionsId)
        {
            OperationResult result = new();

            var perviousRolePermissions = await _storeRolePermissionRepository.GetAllEntitiesAsync();

            if(permissionsId != null && permissionsId.Count() > 0)
            {
                foreach (var permission in perviousRolePermissions) if (permission.StoreRoleId == roleId) _storeRolePermissionRepository.DeleteEntity(permission);

                foreach(var permissionId in permissionsId)
                {
                    if (permissionId < 1) continue;
                    var rolePermission = new StoreRolePermission(roleId, permissionId);
                    await _storeRolePermissionRepository.AddEntityAsync(rolePermission);
                }

                await _storeRolePermissionRepository.SaveChangesAsync();
            }

            return result.Succeeded();
        }
    }
}