using Framework.Domain;
using System.Threading.Tasks;
using AccountManagement.Application.Contract.UserAgg;
using System.Collections.Generic;

namespace AccountManagement.Domain.UserAgg
{
    public interface IUserRepository : IRepository<User>
    {
        Task<IEnumerable<UserVM>> GetAll();
        Task<User> GetUserBy(string mobile);
        Task<EditUserVM> GetDetailForEditBy(long id);
        Task<AddressUserVM> GetAddressInfoBy(long id);
    }
}
