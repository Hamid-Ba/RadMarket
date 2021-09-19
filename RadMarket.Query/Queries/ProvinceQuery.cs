using AdminManagement.Infrastructure.EfCore;
using Microsoft.EntityFrameworkCore;
using RadMarket.Query.Contracts.ProvinceAgg;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RadMarket.Query.Queries
{
    public class ProvinceQuery : IProvinceQuery
    {
        private readonly AdminContext _adminContext;

        public ProvinceQuery(AdminContext adminContext) => _adminContext = adminContext;

        public async Task<IEnumerable<ProvinceQueryVM>> GetAll() => await _adminContext.Provinces.Select(p => new ProvinceQueryVM
        {
            Id = p.Id,
            Name = p.Name
        }).AsNoTracking().ToListAsync();
    }
}
