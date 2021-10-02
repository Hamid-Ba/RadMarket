using Framework.Infrastructure;
using Microsoft.EntityFrameworkCore;
using StoreManagement.Application.Contract.PackageAgg;
using StoreManagement.Domain.PackageAgg;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreManagement.Infrastructure.EfCore.Repository
{
    public class PackageRepository : Repository<Package>, IPackageRepository
    {
        private readonly StoreContext _context;

        public PackageRepository(StoreContext context) : base(context) => _context = context;

        public async Task<IEnumerable<PackageVM>> GetAll() => await _context.Packages.Select(p => new PackageVM
        {
            Id = p.Id,
            Cost = p.Cost,
            Title = p.Title,
            ImageName = p.ImageName,
            OrderCount = p.OrderCount,
            Description = p.Description,
            PackagesCount = p.PackagesCount
        }).AsNoTracking().ToListAsync();


        public async Task<EditPackageVM> GetDetailForEditBy(long id) => await _context.Packages.Select(p => new EditPackageVM
        {
            Id = p.Id,
            Cost = p.Cost,
            Title = p.Title,
            ImageName = p.ImageName,
            Description = p.Description,
            PackagesCount = p.PackagesCount,
        }).FirstOrDefaultAsync(p => p.Id == id);


        public async Task<PackageVM> GetPlanBy(long id) => await _context.Packages.Select(p => new PackageVM
        {
            Id = p.Id,
            Cost = p.Cost,
            Title = p.Title,
            ImageName = p.ImageName,
            OrderCount = p.OrderCount,
            Description = p.Description,
            PackagesCount = p.PackagesCount
        }).FirstOrDefaultAsync(p => p.Id == id);

    }
}