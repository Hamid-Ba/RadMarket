using Framework.Infrastructure;
using Microsoft.EntityFrameworkCore;
using StoreManagement.Application.Contract.AdtPackageAgg;
using StoreManagement.Domain.AdtPackageAgg;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreManagement.Infrastructure.EfCore.Repository
{
    public class AdtPackageRepository : Repository<AdtPackage>, IAdtPackageRepository
    {
        private readonly StoreContext _context;

        public AdtPackageRepository(StoreContext context) : base(context) => _context = context;

        public async Task<AdtPackageVM> GetAdtBy(long id) => await _context.AdtPackages.Select(a => new AdtPackageVM
        {
            Id = a.Id,
            Cost = a.Cost,
            Type = a.Type,
            Title = a.Title,
            ImageName = a.ImageName,
            OrderCount = a.OrderCount,
            Description = a.Description
        }).FirstOrDefaultAsync(a => a.Id == id);

        public async Task<IEnumerable<AdtPackageVM>> GetAll() => await _context.AdtPackages.Select(a => new AdtPackageVM
        {
            Id = a.Id,
            Cost = a.Cost,
            Type = a.Type,
            Title = a.Title,
            ImageName = a.ImageName,
            OrderCount = a.OrderCount,
            Description = a.Description
        }).AsNoTracking().ToListAsync();

        public async Task<EditAdtPackageVM> GetDetailForEditBy(long id) => await _context.AdtPackages.Select(a => new EditAdtPackageVM
        {
            Id = a.Id,
            Cost = a.Cost,
            Type = a.Type,
            Title = a.Title,
            ImageName = a.ImageName,
            Description = a.Description
        }).FirstOrDefaultAsync(a => a.Id == id);
    }
}