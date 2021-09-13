using System.Collections.Generic;
using System.Threading.Tasks;

namespace AccountManagement.Application.Contract.AdminPermissionAgg
{
    public interface IAdminPermissionApplication
    {
        Task<IEnumerable<AdminPermissionVM>> GetAll();
    }
}
