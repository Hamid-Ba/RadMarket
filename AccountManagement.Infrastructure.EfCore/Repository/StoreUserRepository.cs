using AccountManagement.Application.Contract.StoreUserAgg;
using AccountManagement.Domain.StoreUserAgg;
using Framework.Application;
using Framework.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccountManagement.Infrastructure.EfCore.Repository
{
    public class StoreUserRepository : Repository<StoreUser>, IStoreUserRepository
    {
        private readonly AccountContext _context;

        public StoreUserRepository(AccountContext context) : base(context) => _context = context;

        public async Task FillStoreId(long id, long storeId, string code)
        {
            var user = await _context.StoreUser.FirstOrDefaultAsync(s => s.Id == id);
            user.FillStoreId(storeId, code);
        }

        public async Task<EditStoreUserVM> GetDetailForEditBy(long id, long storeId) => await _context.StoreUser.Where(s => s.StoreId == storeId)
            .Select(s => new EditStoreUserVM
            {
                Id = s.Id,
                StoreId = s.StoreId,
                StoreRoleId = s.StoreRoleId,
                FirstName = s.FirstName,
                LastName = s.LastName,
                Mobile = s.Mobile,
                Province = s.Province,
                City = s.City,
                Address = s.Address
            }).FirstOrDefaultAsync(s => s.Id == id);

        public async Task<string> GetAdminName(long id)
        {
            var user = await _context.StoreUser.FirstOrDefaultAsync(u => u.Id == id);
            return $"{user.FirstName} {user.LastName}";
        }

        public async Task<StoreUser> GetUserBy(string mobile) => await _context.StoreUser.FirstOrDefaultAsync(s => s.Mobile == mobile);

        public async Task<IEnumerable<StoreUserVM>> GetAll() => await _context.StoreUser.Select(s => new StoreUserVM
        {
            Id = s.Id,
            StoreId = s.StoreId,
            StoreRoleId = s.StoreRoleId,
            FirstName = s.FirstName,
            LastName = s.LastName,
            Mobile = s.Mobile,
            IsActive = s.IsActive,
            CreationDate = s.CreationDate.ToFarsi()
        }).AsNoTracking().ToListAsync();

        public async Task<AddressStoreUserVM> GetAddressInfoBy(long id) => await _context.StoreUser.Select(s => new AddressStoreUserVM
        {
            Id = s.Id,
            Address = s.Address,
            City = s.City,
            Province = s.Province
        }).FirstOrDefaultAsync(s => s.Id == id);

        public async Task<IEnumerable<StoreUserVM>> GetAll(long storeId)
        {
            var roles = await _context.StoreRole.Select(r => new { Id = r.Id, Name = r.Name }).ToListAsync();

            var result = await _context.StoreUser.Where(s => s.StoreId == storeId).Select(s => new StoreUserVM
            {
                Id = s.Id,
                StoreId = s.StoreId,
                StoreRoleId = s.StoreRoleId,
                FirstName = s.FirstName,
                LastName = s.LastName,
                Mobile = s.Mobile,
                IsActive = s.IsActive,
                CreationDate = s.CreationDate.ToFarsi()
            }).AsNoTracking().ToListAsync();

            result.ForEach(u => u.StoreRoleName = roles.Find(r => r.Id == u.StoreRoleId)?.Name);

            return result;
        }

        public async Task<long> GetStoreIdBy(long id) => (await _context.StoreUser.FirstOrDefaultAsync(s => s.Id == id)).StoreId;
        
    }
}