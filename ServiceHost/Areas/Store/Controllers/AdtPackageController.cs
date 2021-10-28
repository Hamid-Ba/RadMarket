using Framework.Application.Authentication;
using Framework.Peresentation;
using Microsoft.AspNetCore.Mvc;
using ServiceHost.Tools;
using StoreManagement.Application.Contract.AdtPackageAgg;
using StoreManagement.Application.Contract.StoreAgg;
using System.Threading.Tasks;

namespace ServiceHost.Areas.Store.Controllers
{
    [StorePermissionChecker(SellerPermissionHelper.AdtPackages)]
    public class AdtPackageController : StoreBaseController
    {
        private readonly IStoreApplication _storeApplication;
        private readonly IAdtPackageApplication _adtPackageApplication;

        public AdtPackageController(IStoreApplication storeApplication, IAdtPackageApplication adtPackageApplication)
        {
            _storeApplication = storeApplication;
            _adtPackageApplication = adtPackageApplication;
        }

        public async Task<IActionResult> Index()
        {
            var hasAdtCharge = await _storeApplication.IsAccountStillAdtCharged(User.GetStoreId());

            if (hasAdtCharge.IsSucceeded)
            {
                TempData[WarningMessage] = hasAdtCharge.Message;
                return RedirectToAction("Index", "Dashboard");
            }

            return View(await _adtPackageApplication.GetAll());
        }
    }
}
