using AccountManagement.Application.Contract.StoreRoleAgg;
using AccountManagement.Domain.StoreRoleAgg;
using Framework.Application;
using Framework.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccountManagement.Infrastructure.EfCore.Repository
{
    public class StoreRoleRepository : Repository<StoreRole>, IStoreRoleRepository
    {
        private readonly AccountContext _context;

        public StoreRoleRepository(AccountContext context) : base(context) => _context = context;

        public async Task<StoreRole> GetAdminStoreRole(long storeId) => await _context.StoreRole.Where(r => r.StoreId == storeId).FirstOrDefaultAsync();

        public async Task<IEnumerable<StoreRoleVM>> GetAll(long storeId) => await _context.StoreRole.Where(s => s.StoreId == storeId).Select(r => new StoreRoleVM
        {
            Id = r.Id,
            StoreId = r.StoreId,
            Name = r.Name,
            Description = r.Description,
            CreationDate = r.CreationDate.ToFarsi()
        }).AsNoTracking().ToListAsync();

        public StoreRole GetBy(long id) => _context.StoreRole.Include(p => p.Permissions).FirstOrDefault(r => r.Id == id);

        public async Task<EditStoreRoleVM> GetDetailForEditBy(long id,long storeId)
        {
            var result = await _context.StoreRole.Where(s => s.StoreId == storeId).Select(r => new EditStoreRoleVM
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