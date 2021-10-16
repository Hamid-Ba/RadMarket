using Framework.Application;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AccountManagement.Application.Contract.AdminUserAgg
{
    public interface IAdminUserApplication
    {
        OperationResult Logout();
        Task<OperationResult> Delete(long id);
        Task<IEnumerable<AdminUserVM>> GetAll();
        Task<EditAdminUserVM> GetDetailForEditBy(long id);
        Task<OperationResult> Edit(EditAdminUserVM command);
        Task<OperationResult> Login(LoginAdminUserVM command);
        Task<OperationResult> Create(CreateAdminUserVM command);
        bool IsUserHasPermissions(long permissionId, long userId);
    }
}
