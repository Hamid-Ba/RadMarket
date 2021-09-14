using AccountManagement.Application.Contract.StoreRoleAgg;
using AccountManagement.Domain.StoreRoleAgg;
using Framework.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace AccountManagement.Infrastructure.EfCore.Repository
{
    public class StoreRoleRepository : Repository<StoreRole>, IStoreRoleRepository
    {
        private readonly AccountContext _context;

        public StoreRoleRepository(AccountContext context) : base(context) => _context = context;

        public async Task<EditStoreRoleVM> GetDetailForEditBy(long id)
        {
            var result = await _context.StoreRole.Select(r => new EditStoreRoleVM
            {
                Id = r.Id,
                StoreId = r.StoreId,
                Name = r.Name,
                Description = r.Description
            }).FirstOrDefaultAsync(r => r.Id == id);

            result.PermissionsId = await _context.StoreRolePermissions.Where(r => r.StoreRoleId == result.Id).Select(p => p.StorePermissionId).ToArrayAsync();

            return result;
        }
    }
}