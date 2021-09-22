using System.Linq;
using AccountManagement.Domain.UserAgg;
using Framework.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using AccountManagement.Application.Contract.UserAgg;

namespace AccountManagement.Infrastructure.EfCore.Repository
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        private readonly AccountContext _context;

        public UserRepository(AccountContext context) : base(context) => _context = context;

        public async Task<User> GetUserBy(string mobile) => await _context.User.FirstOrDefaultAsync(u => u.Mobile == mobile);

        public async Task<EditUserVM> GetDetailForEditBy(long id) => await _context.User.Select(u => new EditUserVM()
        {
            Id = u.Id,
            FirstName = u.FirstName,
            LastName = u.LastName,
            Province = u.Province,
            City = u.City,
            Address = u.Address
        }).FirstOrDefaultAsync(u => u.Id == id);
    }
}
