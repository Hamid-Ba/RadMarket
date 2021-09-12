using Framework.Domain;
using System.Threading.Tasks;

namespace AccountManagement.Domain.UserAgg
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User> GetUserBy(string mobile);
    }
}
