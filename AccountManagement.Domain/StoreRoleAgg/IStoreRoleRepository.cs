using AccountManagement.Application.Contract.StoreRoleAgg;
using Framework.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AccountManagement.Domain.StoreRoleAgg
{
    public interface IStoreRoleRepository : IRepository<StoreRole>
    {
        StoreRole GetBy(long id);
        Task<StoreRole> GetAdminStoreRole(long storeId);
        Task<IEnumerable<StoreRoleVM>> GetAll(long storeId);
        Task<EditStoreRoleVM> GetDetailForEditBy(long id,long storeId);
    }
}
