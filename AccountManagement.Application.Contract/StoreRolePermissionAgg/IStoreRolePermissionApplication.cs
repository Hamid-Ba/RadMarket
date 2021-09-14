using Framework.Application;
using System.Threading.Tasks;

namespace AccountManagement.Application.Contract.StoreRolePermissionAgg
{
    public interface IStoreRolePermissionApplication
    {
        Task<OperationResult> AddPermissionsToRole(long roleId, long[] permissionsId);
    }
}
