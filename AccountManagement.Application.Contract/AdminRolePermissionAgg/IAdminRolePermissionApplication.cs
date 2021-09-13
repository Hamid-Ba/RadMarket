using Framework.Application;
using System.Threading.Tasks;

namespace AccountManagement.Application.Contract.AdminRolePermissionAgg
{
    public interface IAdminRolePermissionApplication
    {
        Task<OperationResult> AddPermissionsToRole(long roleId, long[] permissionsId);
    }
}
