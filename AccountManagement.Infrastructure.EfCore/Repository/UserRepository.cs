using AccountManagement.Domain.UserAgg;
using Framework.Infrastructure;

namespace AccountManagement.Infrastructure.EfCore.Repository
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        private readonly AccountContext _context;

        public UserRepository(AccountContext context) : base(context) => _context = context;
    }
}
