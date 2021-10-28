using Framework.Application.Authentication;
using Framework.Peresentation;
using Microsoft.AspNetCore.Mvc;
using ServiceHost.Tools;
using StoreManagement.Application.Contract.PackageAgg;
using StoreManagement.Application.Contract.StoreAgg;
using System.Threading.Tasks;

namespace ServiceHost.Areas.Store.Controllers
{
    [StorePermissionChecker(SellerPermissionHelper.Packages)]
    public class PackageController : StoreBaseController
    {
        private readonly IStoreApplication _storeApplication;
        private readonly IPackageApplication _packageApplication;

        public PackageController(IStoreApplication storeApplication, IPackageApplication packageApplication)
        {
            _storeApplication = storeApplication;
            _packageApplication = packageApplication;
        }

        public async Task<IActionResult> Index()
        {
            var hasCharge = await _storeApplication.IsAccountStillCharged(User.GetStoreId());

            if (hasCharge.IsSucceeded)
            {
                TempData[WarningMessage] = hasCharge.Message;
                return RedirectToAction("Index", "Dashboard");
            }

            return View(await _packageApplication.GetAll());
        }
    }
}
