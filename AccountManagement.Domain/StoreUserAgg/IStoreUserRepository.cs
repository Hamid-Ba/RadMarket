using AccountManagement.Application.Contract.StoreUserAgg;
using Framework.Domain;
using System.Threading.Tasks;

namespace AccountManagement.Domain.StoreUserAgg
{
    public interface IStoreUserRepository : IRepository<StoreUser>
    {
        Task<EditStoreUserVM> GetDetailForEditBy(long id);
    }
}
