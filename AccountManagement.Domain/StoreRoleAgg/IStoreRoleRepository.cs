using AccountManagement.Application.Contract.StoreRoleAgg;
using Framework.Domain;
using System.Threading.Tasks;

namespace AccountManagement.Domain.StoreRoleAgg
{
    public interface IStoreRoleRepository : IRepository<StoreRole>
    {
        Task<StoreRole> GetAdminStoreRole(long storeId);
        Task<EditStoreRoleVM> GetDetailForEditBy(long id);
    }
}
