using AccountManagement.Application.Contract.AdminUserAgg;
using AccountManagement.Domain.AdminUserAgg;
using Framework.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace AccountManagement.Infrastructure.EfCore.Repository
{
    public class AdminUserRepository : Repository<AdminUser>, IAdminUserRepository
    {
        private readonly AccountContext _context;

        public AdminUserRepository(AccountContext context) : base(context) => _context = context;

        public async Task<EditAdminUserVM> GetDetailForEditBy(long id) => await _context.AdminUsers.Select(a => new EditAdminUserVM
        {
            Id = a.Id,
            FirstName = a.FirstName,
            LastName = a.LastName,
            Mobile = a.Mobile,
        }).FirstOrDefaultAsync(a => a.Id == id);

        public async Task<AdminUser> GetUserBy(string mobile) => await _context.AdminUsers.FirstOrDefaultAsync(a => a.Mobile == mobile);
        
    }
}
