using DiscountManagement.Infrastructure.EfCore;
using Framework.Infrastructure;
using Microsoft.EntityFrameworkCore;
using RadMarket.Query.Contracts.PackageOrderAgg;
using StoreManagement.Infrastructure.EfCore;
using System.Linq;
using System.Threading.Tasks;

namespace RadMarket.Query.Queries
{
    public class CalculatePackageOrderCart : ICalculatePackageOrderCart
    {
        private readonly StoreContext _storeContext;
        private readonly DiscountContext _discountContext;

        public CalculatePackageOrderCart(StoreContext storeContext, DiscountContext discountContext)
        {
            _storeContext = storeContext;
            _discountContext = discountContext;
        }

        public async Task<PackageOrderCartVM> CreateCart(PackageType type, long packageId, string discountCode,string discountPrice)
        {
            var packages = await _storeContext.Packages.Select(p => new { Id = p.Id, Name = p.Title, Cost = p.Cost }).ToListAsync();
            var adtPackages = await _storeContext.AdtPackages.Select(p => new { Id = p.Id, Name = p.Title, Cost = p.Cost }).ToListAsync();

            var code = await _discountContext.DiscountCodes.FirstOrDefaultAsync(c => c.Code == discountCode);

            PackageOrderCartVM result;

            switch (type)
            {
                case PackageType.Products:
                    var package = packages.FirstOrDefault(p => p.Id == packageId);
                    result = new PackageOrderCartVM(packageId, package.Name, PackageType.Products, package.Cost, package.Cost);
                    break;

                default:
                    var adtPackage = adtPackages.FirstOrDefault(p => p.Id == packageId);
                    result = new PackageOrderCartVM(packageId, adtPackage.Name, PackageType.Adt, adtPackage.Cost, adtPackage.Cost);
                    break;
            }

            if (code != null && code.IsCodeStillWork() && discountPrice == "0")
            {
                result.CalculatePriceViaDiscount(code.Rate);
                code.CodeUsed();
                await _discountContext.SaveChangesAsync();
            }
            
            
            return result;
        }
    }
}