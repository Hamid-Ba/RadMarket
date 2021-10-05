using Framework.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using RadMarket.Query.Contracts.PackageOrderAgg;
using StoreManagement.Application.Contract.PackageOrderAgg;
using System.Threading.Tasks;

namespace ServiceHost.Areas.Store.Controllers
{
    public class PackageOrderController : StoreBaseController
    {
        private readonly ICalculatePackageOrderCart _cart;
        private readonly IPackageOrderApplication _packageOrderApplication;

        public PackageOrderController(ICalculatePackageOrderCart cart, IPackageOrderApplication packageOrderApplication)
        {
            _cart = cart;
            _packageOrderApplication = packageOrderApplication;
        }

        public async Task<IActionResult> Index(long packageId, PackageType type, string code,string discountPrice) => View(await _cart.CreateCart(type, packageId, code,discountPrice));
        
    }
}
