using AccountManagement.Application.Contract.AdminRolePermissionAgg;
using AccountManagement.Domain.AdminRolePermissionAgg;
using Framework.Application;
using System.Linq;
using System.Threading.Tasks;

namespace AccountManagement.Application
{
    public class AdminRolePermissionApplication : IAdminRolePermissionApplication
    {
        private readonly IAdminRolePermissionRepository _adminRolePermissionRepository;

        public AdminRolePermissionApplication(IAdminRolePermissionRepository adminRolePermissionRepository) => _adminRolePermissionRepository = adminRolePermissionRepository;

        public async Task<OperationResult> AddPermissionsToRole(long roleId, long[] permissionsId)
        {
            OperationResult result = new();

            var perviousPermissions = await _adminRolePermissionRepository.GetAllEntitiesAsync();

            if (permissionsId != null && permissionsId.Count() > 0)
            {
                foreach (var permission in perviousPermissions) if (permission.AdminRoleId == roleId) _adminRolePermissionRepository.DeleteEntity(permission);

                foreach(var permissionId in permissionsId)
                {
                    if (permissionId == 0) continue;
                    var rolePermission = new AdminRolePermission(roleId, permissionId);
                    await _adminRolePermissionRepository.AddEntityAsync(rolePermission);
                }

                await _adminRolePermissionRepository.SaveChangesAsync();
            }

            return result.Succeeded();
        }
    }
}