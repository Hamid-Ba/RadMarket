using AccountManagement.Application.Contract.AdminUserAgg;
using AccountManagement.Domain.AdminUserAgg;
using Framework.Application;
using Framework.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccountManagement.Infrastructure.EfCore.Repository
{
    public class AdminUserRepository : Repository<AdminUser>, IAdminUserRepository
    {
        private readonly AccountContext _context;

        public AdminUserRepository(AccountContext context) : base(context) => _context = context;

        public async Task<IEnumerable<AdminUserVM>> GetAll() => await _context.AdminUsers.Include(r => r.AdminRole).Select(u => new AdminUserVM
        {
            Id = u.Id,
            AdminRoleId = u.AdminRoleId,
            AdminRoleTitle = u.AdminRole.Name,
            FirstName = u.FirstName,
            LastName = u.LastName,
            Mobile = u.Mobile,
            CreationDate = u.CreationDate.ToFarsi()
        }).AsNoTracking().ToListAsync();
        

        public async Task<EditAdminUserVM> GetDetailForEditBy(long id) => await _context.AdminUsers.Select(a => new EditAdminUserVM
        {
            Id = a.Id,
            AdminRoleId = a.AdminRoleId,
            FirstName = a.FirstName,
            LastName = a.LastName,
            Mobile = a.Mobile,
        }).FirstOrDefaultAsync(a => a.Id == id);

        public async Task<AdminUser> GetUserBy(string mobile) => await _context.AdminUsers.FirstOrDefaultAsync(a => a.Mobile == mobile);
        
    }
}
