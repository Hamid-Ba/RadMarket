using Microsoft.AspNetCore.Mvc;
using StoreManagement.Application.Contract.PackageAgg;
using System.Threading.Tasks;

namespace ServiceHost.Areas.Store.Controllers
{
    public class PackageController : StoreBaseController
    {
        private readonly IPackageApplication _packageApplication;

        public PackageController(IPackageApplication packageApplication)
        {
            _packageApplication = packageApplication;
        }

        public async Task<IActionResult> Index() => View(await _packageApplication.GetAll());
        
    }
}
