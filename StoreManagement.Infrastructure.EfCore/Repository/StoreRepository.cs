using System.Collections.Generic;
using Framework.Infrastructure;
using Microsoft.EntityFrameworkCore;
using StoreManagement.Application.Contract.StoreAgg;
using StoreManagement.Domain.StoreAgg;
using System.Linq;
using System.Threading.Tasks;
using Framework.Application;
using AccountManagement.Infrastructure.EfCore;

namespace StoreManagement.Infrastructure.EfCore.Repository
{
    public class StoreRepository : Repository<Store>, IStoreRepository
    {
        private readonly StoreContext _context;
        private readonly AccountContext _accountContext;

        public StoreRepository(StoreContext context, AccountContext accountContext) : base(context)
        {
            _context = context;
            _accountContext = accountContext;
        }

        public async Task<string> GetStoreCode(long id) => (await _context.Stores.FirstOrDefaultAsync(s => s.Id == id)).UniqueCode;

        public async Task<string> GetStoreName(long id) => (await _context.Stores.FirstOrDefaultAsync(s => s.Id == id)).Name;

        public async Task<IEnumerable<StoreVM>> GetAll()
        {
            var admins = await _accountContext.StoreUser.Select(s => new { Id = s.Id, StoreId = s.StoreId, Name = $"{s.FirstName} {s.LastName}" }).ToListAsync();

            var result = await _context.Stores.Select(s => new StoreVM()
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

            result.ForEach(s => s.StoreAdminName = admins.Find(a => a.StoreId == s.Id)?.Name);

            return result;
        }
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

        public async Task<long> GetStoreAdminId(long id) => (await _context.Stores.FirstOrDefaultAsync(s => s.Id == id)).StoreAdminUserId;
        
    }
}