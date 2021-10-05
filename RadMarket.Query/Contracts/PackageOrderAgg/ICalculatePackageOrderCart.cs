using Framework.Infrastructure;
using System.Threading.Tasks;

namespace RadMarket.Query.Contracts.PackageOrderAgg
{
    public interface ICalculatePackageOrderCart
    {
        Task<PackageOrderCartVM> CreateCart(PackageType type, long packageId, string discountCode,string discountPrice);
    }
}
