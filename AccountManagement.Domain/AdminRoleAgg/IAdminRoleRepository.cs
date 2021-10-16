using AccountManagement.Application.Contract.AdminRoleAgg;
using Framework.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AccountManagement.Domain.AdminRoleAgg
{
    public interface IAdminRoleRepository : IRepository<AdminRole>
    {
        AdminRole GetBy(long id);
        Task<IEnumerable<AdminRoleVM>> GetAll();
        Task<EditAdminRoleVM> GetDetailForEditBy(long id);
    }
}
