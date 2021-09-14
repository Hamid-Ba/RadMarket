using System.Collections.Generic;
using System.Threading.Tasks;

namespace AccountManagement.Application.Contract.StorePermissionAgg
{
    public interface IStorePermissionApplication
    {
        Task<IEnumerable<StorePermissionVM>> GetAll();
    }
}
