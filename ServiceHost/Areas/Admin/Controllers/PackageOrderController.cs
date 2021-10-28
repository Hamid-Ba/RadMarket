using Microsoft.AspNetCore.Mvc;
using ServiceHost.Tools;
using StoreManagement.Application.Contract.PackageOrderAgg;
using StoreManagement.Infrastructure.Configuration;
using System.Threading.Tasks;

namespace ServiceHost.Areas.Admin.Controllers
{
    [AdminPermissionChecker(StorePermissionHelper.PackagesOrder)]
    public class PackageOrderController : AdminBaseController
    {
        private readonly IPackageOrderApplication _packageOrderApplication;

        public PackageOrderController(IPackageOrderApplication packageOrderApplication) => _packageOrderApplication = packageOrderApplication;

        public async Task<IActionResult> Index() => View(await _packageOrderApplication.GetAll());

    }
}
