using AccountManagement.Domain.UserAgg;
using Framework.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace AccountManagement.Infrastructure.EfCore.Repository
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        private readonly AccountContext _context;

        public UserRepository(AccountContext context) : base(context) => _context = context;

        public async Task<User> GetUserBy(string mobile) => await _context.User.FirstOrDefaultAsync(u => u.Mobile == mobile);

    }
}
