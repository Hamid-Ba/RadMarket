using Framework.Domain;
using StoreManagement.Application.Contract.StoreAgg;
using System.Threading.Tasks;

namespace StoreManagement.Domain.StoreAgg
{
    public interface IStoreRepository : IRepository<Store>
    {
        Task<string> GetStoreCode(long id);
        Task<Store> GetStoreBy(string code);
        Task<EditStoreVM> GetDetailForEditBy(long id);
    }
}
