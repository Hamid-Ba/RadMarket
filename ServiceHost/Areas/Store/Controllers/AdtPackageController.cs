using Microsoft.AspNetCore.Mvc;
using StoreManagement.Application.Contract.AdtPackageAgg;
using System.Threading.Tasks;

namespace ServiceHost.Areas.Store.Controllers
{
    public class AdtPackageController : StoreBaseController
    {
        private readonly IAdtPackageApplication _adtPackageApplication;

        public AdtPackageController(IAdtPackageApplication adtPackageApplication) => _adtPackageApplication = adtPackageApplication;

        public async Task<IActionResult> Index() => View(await _adtPackageApplication.GetAll());
    }
}
