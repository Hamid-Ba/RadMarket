using AccountManagement.Application.Contract.AdminPermissionAgg;
using AccountManagement.Domain.AdminPermissionAgg;
using Framework.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccountManagement.Infrastructure.EfCore.Repository
{
    public class AdminPermissionRepository : Repository<AdminPermission>, IAdminPermissionRepository
    {
        private readonly AccountContext _context;

        public AdminPermissionRepository(AccountContext context) : base(context) => _context = context;

        public async Task<IEnumerable<AdminPermissionVM>> GetAll() => await _context.AdminPermissions.Select(p => new AdminPermissionVM
        {
            Id = p.Id,
            Title = p.Title,
            ParentId = p.ParentId
        }).AsNoTracking().ToListAsync();
        
    }
}
