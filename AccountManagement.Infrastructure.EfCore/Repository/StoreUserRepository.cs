﻿using AccountManagement.Application.Contract.StoreUserAgg;
using AccountManagement.Domain.StoreUserAgg;
using Framework.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace AccountManagement.Infrastructure.EfCore.Repository
{
    public class StoreUserRepository : Repository<StoreUser>, IStoreUserRepository
    {
        private readonly AccountContext _context;

        public StoreUserRepository(AccountContext context) : base(context) => _context = context;

        public async Task FillStoreId(long id,long storeId, string code)
        {
            var user = await _context.StoreUser.FirstOrDefaultAsync(s => s.Id == id);
            user.FillStoreId(storeId, code);
        }

        public async Task<EditStoreUserVM> GetDetailForEditBy(long id) => await _context.StoreUser.Select(s => new EditStoreUserVM
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
        
    }
}
