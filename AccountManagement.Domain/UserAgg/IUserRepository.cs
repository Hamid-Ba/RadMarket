using Framework.Domain;
using System.Threading.Tasks;
using AccountManagement.Application.Contract.UserAgg;

namespace AccountManagement.Domain.UserAgg
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User> GetUserBy(string mobile);
        Task<EditUserVM> GetDetailForEditBy(long id);
    }
}
