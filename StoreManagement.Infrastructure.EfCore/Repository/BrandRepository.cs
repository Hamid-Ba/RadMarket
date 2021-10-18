using Framework.Infrastructure;
using Microsoft.EntityFrameworkCore;
using StoreManagement.Application.Contract.BrandAgg;
using StoreManagement.Domain.BrandAgg;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreManagement.Infrastructure.EfCore.Repository
{
    public class BrandRepository : Repository<Brand>, IBrandRepository
    {
        private readonly StoreContext _context;

        public BrandRepository(StoreContext context) : base(context) => _context = context;

        public async Task<IEnumerable<BrandVM>> GetAll(long storeId) => await _context.Brands.Where(b => b.StoreId == storeId).Select(b => new BrandVM
        {
            Id = b.Id,
            StoreId = b.StoreId,
            Name = b.Name,
            ProductCount = b.Products.Count,
        }).AsNoTracking().ToListAsync();

        public async Task<EditBrandVM> GetDetailForEditBy(long id, long storeId) => await _context.Brands.Where(b => b.StoreId == storeId).Select(b => new EditBrandVM
        {
            Id = b.Id,
            Name = b.Name,
            StoreId = b.StoreId
        }).FirstOrDefaultAsync(b => b.Id == id);

    }
}
