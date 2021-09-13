using AccountManagement.Domain.AdminRolePermissionAgg;
using Framework.Infrastructure;

namespace AccountManagement.Infrastructure.EfCore.Repository
{
    public class AdminRolePermissionRepository : Repository<AdminRolePermission>, IAdminRolePermissionRepository
    {
        private readonly AccountContext _context;

        public AdminRolePermissionRepository(AccountContext context) : base(context) => _context = context;

    }
}