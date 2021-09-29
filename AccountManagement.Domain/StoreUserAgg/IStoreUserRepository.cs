using AccountManagement.Application.Contract.StoreUserAgg;
using Framework.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AccountManagement.Domain.StoreUserAgg
{
    public interface IStoreUserRepository : IRepository<StoreUser>
    {
        Task<string> GetAdminName(long id);
        Task<IEnumerable<StoreUserVM>> GetAll();
        Task<StoreUser> GetUserBy(string mobile);
        Task<EditStoreUserVM> GetDetailForEditBy(long id);
        Task<AddressStoreUserVM> GetAddressInfoBy(long id);
        Task FillStoreId(long id,long storeId, string code);
    }
}
