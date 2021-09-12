using Framework.Application;
using System.Threading.Tasks;

namespace AccountManagement.Application.Contract.AdminUserAgg
{
    public interface IAdminUserApplication
    {
        Task<EditAdminUserVM> GetDetailForEditBy(long id);
        Task<OperationResult> Edit(EditAdminUserVM command);
        Task<OperationResult> Create(CreateAdminUserVM command);
    }
}
