using AccountManagement.Application.Contract.AdminPermissionAgg;
using Framework.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AccountManagement.Domain.AdminPermissionAgg
{
    public interface IAdminPermissionRepository : IRepository<AdminPermission>
    {
        Task<IEnumerable<AdminPermissionVM>> GetAll();
    }
}
