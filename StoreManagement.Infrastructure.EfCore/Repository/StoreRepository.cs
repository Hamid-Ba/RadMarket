using Framework.Infrastructure;
using Microsoft.EntityFrameworkCore;
using StoreManagement.Application.Contract.StoreAgg;
using StoreManagement.Domain.StoreAgg;
using System.Linq;
using System.Threading.Tasks;

namespace StoreManagement.Infrastructure.EfCore.Repository
{
    public class StoreRepository : Repository<Store>, IStoreRepository
    {
        private readonly StoreContext _context;

        public StoreRepository(StoreContext context) : base(context) => _context = context;

        public async Task<string> GetStoreCode(long id) => (await _context.Stores.FirstOrDefaultAsync(s => s.Id == id)).UniqueCode;

        public async Task<Store> GetStoreBy(string code) => await _context.Stores.FirstOrDefaultAsync(s => s.UniqueCode == code);

        public async Task<EditStoreVM> GetDetailForEditBy(long id) => await _context.Stores.Select(s => new EditStoreVM
        {
            Id = s.Id,
            StoreAdminUserId = s.StoreAdminUserId,
            Name = s.Name,
            PhoneNumber = s.PhoneNumber,
            MobileNumber = s.MobileNumber,
            Description = s.Description,
            Address = s.Address
        }).FirstOrDefaultAsync(s => s.Id == id);

    }
}