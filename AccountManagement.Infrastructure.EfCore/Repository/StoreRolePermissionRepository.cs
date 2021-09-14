using AccountManagement.Domain.StoreRolePermissionAgg;
using Framework.Infrastructure;

namespace AccountManagement.Infrastructure.EfCore.Repository
{
    public class StoreRolePermissionRepository : Repository<StoreRolePermission>, IStoreRolePermissionRepository
    {
        private readonly AccountContext _context;

        public StoreRolePermissionRepository(AccountContext context) : base(context) => _context = context;
    }
}
