using Framework.Application;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AccountManagement.Application.Contract.AdminRoleAgg
{
    public interface IAdminRoleApplication
    {
        Task<OperationResult> Delete(long id);
        Task<IEnumerable<AdminRoleVM>> GetAll();
        Task<EditAdminRoleVM> GetDetailForEditBy(long id);
        Task<OperationResult> Edit(EditAdminRoleVM command);
        Task<OperationResult> Create(CreateAdminRoleVM command);
        bool IsRoleHasThePermission(long roleId, long permissionId);
    }
}
