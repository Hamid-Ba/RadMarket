using Framework.Application;
using Framework.Application.Authentication;
using Framework.Application.ZarinPal;
using Framework.Infrastructure;
using Framework.Peresentation;
using Microsoft.AspNetCore.Mvc;
using RadMarket.Query.Contracts.PackageOrderAgg;
using ReflectionIT.Mvc.Paging;
using ServiceHost.Tools;
using StoreManagement.Application.Contract.PackageOrderAgg;
using StoreManagement.Application.Contract.StoreAgg;
using System.Threading.Tasks;

namespace ServiceHost.Areas.Store.Controllers
{
    [StorePermissionChecker(SellerPermissionHelper.OrderPackages)]
    public class PackageOrderController : StoreBaseController
    {
        private readonly ICalculatePackageOrderCart _cart;
        private readonly IZarinPalFactory _zarinPalFactory;
        private readonly IPackageOrderApplication _packageOrderApplication;

        public PackageOrderController(ICalculatePackageOrderCart cart, IZarinPalFactory zarinPalFactory, IPackageOrderApplication packageOrderApplication)
        {
            _cart = cart;
            _zarinPalFactory = zarinPalFactory;
            _packageOrderApplication = packageOrderApplication;
        }

        public async Task<IActionResult> OrderdPackages(int pageIndex = 1)
        {
            var packages = await _packageOrderApplication.GetAllBy(User.GetStoreId());

            var model = PagingList.Create(packages, 10, pageIndex);
            ViewBag.Rows = (10 * pageIndex) - 9;
            model.Action = "OrderdPackages";

            return View(model);
        }
        public async Task<IActionResult> Index(long packageId, PackageType type, string code, string discountPrice)
        {
            var cart = await _cart.CreateCart(type, packageId, code, discountPrice);

            if (cart is null)
            {
                TempData[ErrorMessage] = "به مشکل بر خوردید";
                return RedirectToAction("Index", "Dashboard");
            }
            return View(cart);
        }

        [HttpGet]
        public async Task<IActionResult> CheckOut(long packageId, PackageType type, string discountCode)
        {
            if (User.Identity.IsAuthenticated)
            {
                var cart = await _cart.CreateCart(type, packageId, discountCode, "0");

                if (cart is null)
                {
                    TempData[ErrorMessage] = "به مشکل بر خوردید";
                    return RedirectToAction("Index", "Dashboard");
                }

                var createPackageOrder = new CreatePackageOrderVM()
                {
                    StoreId = User.GetStoreId(),
                    Type = cart.Type,
                    PackageId = cart.PackageId,
                    PayAmount = cart.PayAmount,
                    DiscountPrice = cart.DiscountPrice,
                    TotalPrice = cart.TotalPrice,
                    MobileNumber = User.GetMobilePhone()
                };

                var packageOrderId = await _packageOrderApplication.PlaceOrder(createPackageOrder);

                var paymentResponse = _zarinPalFactory.CreatePaymentRequest(cart.PayAmount.ToString(), createPackageOrder.MobileNumber, cart.PackageName, packageOrderId, "StoreCallBack");
                return Redirect(
                    $"https://{_zarinPalFactory.Prefix}.zarinpal.com/pg/StartPay/{paymentResponse.Authority}");
            }
            TempData[WarningMessage] = "ابتدا باید لاگین کنید";
            return Redirect("/");
        }

        [HttpGet("StoreCallBack/{oId}")]
        public async Task<IActionResult> CallBack(long oId, [FromQuery] string authority, [FromQuery] string status)
        {
            var verificationResponse = _zarinPalFactory.CreateVerificationRequest(authority, (await _packageOrderApplication.GetPriceBy(oId)).ToString());
            OperationResult result = new OperationResult();

            result.Failed("تراکنش با مشکل مواجه شد! در صورت کسر مبلغ ، حداکثر تا 24 ساعت دیگر برگردانده می شود");

            if (verificationResponse.Status == 100 && status.ToLower() == "ok")
            {
                var issueTrackingNo = await _packageOrderApplication.PaymentSucceeded(oId, verificationResponse.RefID);
                result.Succeeded($"پکیج با موفقیت خریداری شد");

                TempData[SuccessMessage] = result.Message;

                return RedirectToAction("Index", "Dashboard", new { area = "Store" });
            }

            TempData[ErrorMessage] = result.Message;
            return RedirectToAction("Index", "Dashboard", new { area = "Store" });
        }
    }
}