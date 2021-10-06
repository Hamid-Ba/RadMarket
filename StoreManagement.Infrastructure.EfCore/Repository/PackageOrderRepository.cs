using Framework.Application;
using Framework.Infrastructure;
using Microsoft.EntityFrameworkCore;
using StoreManagement.Application.Contract.PackageOrderAgg;
using StoreManagement.Domain.PackageOrderAgg;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreManagement.Infrastructure.EfCore.Repository
{
    public class PackageOrderRepository : Repository<PackageOrder>, IPackageOrderRepository
    {
        private readonly StoreContext _context;

        public PackageOrderRepository(StoreContext context) : base(context) => _context = context;

        public async Task<IEnumerable<PackageOrderVM>> GetAll()
        {
            var stores = await _context.Stores.Select(s => new { Id = s.Id, Name = s.Name,Code = s.UniqueCode }).ToListAsync();

            var package = await _context.Packages.Select(p => new { Id = p.Id, Name = p.Title }).ToListAsync();
            var adtPackage = await _context.AdtPackages.Select(p => new { Id = p.Id, Name = p.Title }).ToListAsync();

            var result = await _context.PackageOrders.Select(p => new PackageOrderVM
            {
                Id = p.Id,
                PackageId = p.PackageId,
                StoreId = p.StoreId,
                Type = p.Type,
                DiscountPrice = p.DiscountPrice,
                IsPayed = p.IsPayed,
                MobileNumber = p.MobileNumber,
                PayAmount = p.PayAmount,
                RefId = p.RefId,
                TotalPrice = p.TotalPrice,
                CreationDate = p.CreationDate.ToFarsi()
            }).AsNoTracking().OrderByDescending(c => c.CreationDate).ToListAsync();

            result.ForEach(o => o.StoreName = stores.FirstOrDefault(s => s.Id == o.StoreId)?.Name);
            result.ForEach(o => o.StoreCode = stores.FirstOrDefault(s => s.Id == o.StoreId)?.Code);

            result.ForEach(o => o.PackageName = o.Type == PackageType.Products ? 
            package.FirstOrDefault(p => p.Id == o.PackageId)?.Name : adtPackage.FirstOrDefault(p => p.Id == o.PackageId)?.Name);

            return result;
        }

        public async Task<IEnumerable<PackageOrderVM>> GetAllBy(long storeId)
        {
            var stores = await _context.Stores.Select(s => new { Id = s.Id, Name = s.Name }).ToListAsync();

            var package = await _context.Packages.Select(p => new { Id = p.Id, Name = p.Title }).ToListAsync();
            var adtPackage = await _context.AdtPackages.Select(p => new { Id = p.Id, Name = p.Title }).ToListAsync();

            var result = await _context.PackageOrders.Where(s => s.StoreId == storeId).Select(p => new PackageOrderVM
            {
                Id = p.Id,
                PackageId = p.PackageId,
                StoreId = p.StoreId,
                Type = p.Type,
                DiscountPrice = p.DiscountPrice,
                IsPayed = p.IsPayed,
                MobileNumber = p.MobileNumber,
                PayAmount = p.PayAmount,
                RefId = p.RefId,
                TotalPrice = p.TotalPrice,
                CreationDate = p.CreationDate.ToFarsi()
            }).AsNoTracking().OrderByDescending(c => c.CreationDate).ToListAsync();

            result.ForEach(o => o.StoreName = stores.FirstOrDefault(s => s.Id == o.StoreId)?.Name);

            result.ForEach(o => o.PackageName = o.Type == PackageType.Products ?
            package.FirstOrDefault(p => p.Id == o.PackageId)?.Name : adtPackage.FirstOrDefault(p => p.Id == o.PackageId)?.Name);

            return result;
        }
    }
}