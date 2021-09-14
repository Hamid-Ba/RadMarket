using AccountManagement.Application.Contract.StorePermissionAgg;
using AccountManagement.Domain.StorePermissionAgg;
using Framework.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccountManagement.Infrastructure.EfCore.Repository
{
    public class StorePermissionRepository : Repository<StorePermission>, IStorePermissionRepository
    {
        private readonly AccountContext _context;

        public StorePermissionRepository(AccountContext context) : base(context) => _context = context;

        public async Task<IEnumerable<StorePermissionVM>> GetAll() => await _context.StorePermissions.Select(p => new StorePermissionVM
        {
            Id = p.Id,
            ParentId = p.ParentId,
            Title = p.Title
        }).AsNoTracking().ToListAsync();
        
    }
}
