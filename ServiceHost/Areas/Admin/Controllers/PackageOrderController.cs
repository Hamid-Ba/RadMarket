using Microsoft.AspNetCore.Mvc;
using StoreManagement.Application.Contract.PackageOrderAgg;
using System.Threading.Tasks;

namespace ServiceHost.Areas.Admin.Controllers
{
    public class PackageOrderController : AdminBaseController
    {
        private readonly IPackageOrderApplication _packageOrderApplication;

        public PackageOrderController(IPackageOrderApplication packageOrderApplication) => _packageOrderApplication = packageOrderApplication;

        public async Task<IActionResult> Index() => View(await _packageOrderApplication.GetAll());

    }
}
