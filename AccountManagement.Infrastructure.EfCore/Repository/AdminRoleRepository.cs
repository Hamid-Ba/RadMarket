using AccountManagement.Application.Contract.AdminRoleAgg;
using AccountManagement.Domain.AdminRoleAgg;
using Framework.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace AccountManagement.Infrastructure.EfCore.Repository
{
    public class AdminRoleRepository : Repository<AdminRole>, IAdminRoleRepository
    {
        private readonly AccountContext _context;

        public AdminRoleRepository(AccountContext context) : base(context) => _context = context;

        public async Task<EditAdminRoleVM> GetDetailForEditBy(long id) => await _context.AdminRoles.Select(a => new EditAdminRoleVM
        {
            Id = a.Id,
            Name = a.Name,
            Description = a.Description
        }).FirstOrDefaultAsync(a => a.Id == id);

    }
}
