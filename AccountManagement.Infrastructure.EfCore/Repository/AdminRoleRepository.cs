using AccountManagement.Application.Contract.AdminRoleAgg;
using AccountManagement.Domain.AdminRoleAgg;
using Framework.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccountManagement.Infrastructure.EfCore.Repository
{
    public class AdminRoleRepository : Repository<AdminRole>, IAdminRoleRepository
    {
        private readonly AccountContext _context;

        public AdminRoleRepository(AccountContext context) : base(context) => _context = context;

        public async Task<IEnumerable<AdminRoleVM>> GetAll() => await _context.AdminRoles.Select(r => new AdminRoleVM
        {
            Id = r.Id,
            Name = r.Name,
            Description = r.Description
        }).AsNoTracking().ToListAsync();

        public async Task<EditAdminRoleVM> GetDetailForEditBy(long id)
        {
            var result = await _context.AdminRoles.Select(a => new EditAdminRoleVM
            {
                Id = a.Id,
                Name = a.Name,
                Description = a.Description,
            }).FirstOrDefaultAsync(a => a.Id == id);

            result.PermissionsId = await _context.AdminRolePermissions.Where(p => p.AdminRoleId == result.Id).Select(p => p.AdminPermissionId).ToArrayAsync();

            return result;
        }
    }
}