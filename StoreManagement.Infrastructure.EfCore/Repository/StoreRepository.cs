using System.Collections.Generic;
using Framework.Infrastructure;
using Microsoft.EntityFrameworkCore;
using StoreManagement.Application.Contract.StoreAgg;
using StoreManagement.Domain.StoreAgg;
using System.Linq;
using System.Threading.Tasks;
using Framework.Application;

namespace StoreManagement.Infrastructure.EfCore.Repository
{
    public class StoreRepository : Repository<Store>, IStoreRepository
    {
        private readonly StoreContext _context;

        public StoreRepository(StoreContext context) : base(context) => _context = context;

        public async Task<string> GetStoreCode(long id) => (await _context.Stores.FirstOrDefaultAsync(s => s.Id == id)).UniqueCode;

        public async Task<string> GetStoreName(long id) => (await _context.Stores.FirstOrDefaultAsync(s => s.Id == id)).Name;

        public async Task<IEnumerable<StoreVM>> GetAll() => await _context.Stores.Select(s => new StoreVM()
        {
            Id = s.Id,
            StoreAdminUserId = s.StoreAdminUserId,
            Name = s.Name,
            MobileNumber = s.MobileNumber,
            PhoneNumber = s.PhoneNumber,
            UniqueCode = s.UniqueCode,
            Status = s.Status,
            StoreGivenStatusReason = s.StoreGivenStatusReason,
            CreationDate = s.CreationDate.ToFarsi()
        }).AsNoTracking().ToListAsync();

        public async Task<Store> GetStoreBy(string code) => await _context.Stores.FirstOrDefaultAsync(s => s.UniqueCode == code);

        public async Task<BankStoreVM> GetBankInfo(long id) => await _context.Stores.Select(s => new BankStoreVM()
        {
            Id = s.Id,
            AccountNumber = s.AccountNumber,
            CardNumber = s.CardNumber,
            ShabaNumber = s.ShabaNumber
        }).FirstOrDefaultAsync(s => s.Id == id);

        public async Task<AddressStoreVM> GetAddressInfo(long id) => await _context.Stores.Select(s => new AddressStoreVM()
        {
            Id = s.Id,
            Province = s.Province,
            City = s.City,
            Address = s.Address
        }).FirstOrDefaultAsync(s => s.Id == id);

        public async Task<EditStoreVM> GetDetailForEditBy(long id) => await _context.Stores.Select(s => new EditStoreVM
        {
            Id = s.Id,
            StoreAdminUserId = s.StoreAdminUserId,
            Name = s.Name,
            AccountNumber = s.AccountNumber,
            ShabaNumber = s.ShabaNumber,
            CardNumber = s.CardNumber,
            PhoneNumber = s.PhoneNumber,
            MobileNumber = s.MobileNumber,
            Description = s.Description,
            Province = s.Province,
            City = s.City,
            Address = s.Address
        }).FirstOrDefaultAsync(s => s.Id == id);

    }
}