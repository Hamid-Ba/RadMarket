using Microsoft.EntityFrameworkCore;
using RadMarket.Query.Contracts.StoreAgg;
using StoreManagement.Infrastructure.EfCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RadMarket.Query.Queries
{
    public class StoreQuery : IStoreQuery
    {
        private readonly StoreContext _storeContext;

        public StoreQuery(StoreContext storeContext) => _storeContext = storeContext;

        public async Task<IEnumerable<StoreQueryVM>> GetAll() => await _storeContext.Stores.Where(s => s.ChargeExpiredDate >= DateTime.Now)
               .Select(s => new StoreQueryVM
               {
                   Id = s.Id,
                   Name = s.Name,
               }).AsNoTracking().ToListAsync();
    }
}