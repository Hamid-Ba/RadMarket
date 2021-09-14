using AccountManagement.Application.Contract.StorePermissionAgg;
using Framework.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AccountManagement.Domain.StorePermissionAgg
{
    public interface IStorePermissionRepository : IRepository<StorePermission>
    {
        Task<IEnumerable<StorePermissionVM>> GetAll();
    }
}
