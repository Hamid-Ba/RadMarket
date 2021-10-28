using Framework.Application;
using Framework.Application.Authentication;
using Framework.Application.ZarinPal;
using Framework.Peresentation;
using Microsoft.AspNetCore.Mvc;
using RadMarket.Query.Contracts.OrderAgg;
using ServiceHost.Tools;
using StoreManagement.Application.Contract.OrderAgg;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceHost.Areas.Store.Controllers
{
    [StorePermissionChecker(SellerPermissionHelper.Financial)]
    public class AccountController : StoreBaseController
    {
        private readonly IOrderQuery _orderQuery;
        private readonly IZarinPalFactory _zarinPalFactory;
        private readonly IOrderApplication _orderApplication;

        public AccountController(IOrderQuery orderQuery, IZarinPalFactory zarinPalFactory, IOrderApplication orderApplication)
        {
            _orderQuery = orderQuery;
            _zarinPalFactory = zarinPalFactory;
            _orderApplication = orderApplication;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.TotalSiteProfitAmount = await _orderQuery.GetTotalSiteProfit(User.GetStoreId());
            return View(await _orderQuery.GetUnPayedItems(User.GetStoreId()));
        }

        [HttpGet]
        public async Task<IActionResult> CheckOut()
        {
            var unPayedAmount = await _orderQuery.GetTotalSiteProfit(User.GetStoreId());

            if (unPayedAmount <= 0)
            {
                TempData[WarningMessage] = "هیچ آیتمی برای پرداخت وجود ندارد";
                return RedirectToAction("Index");
            }

            var paymentResponse = _zarinPalFactory.CreatePaymentRequest(unPayedAmount.ToString(), User.GetMobilePhone(), "پرداخت درصد فروش به سایت", User.GetStoreId(), "PayedOrderCallBack");
            return Redirect(
                $"https://{_zarinPalFactory.Prefix}.zarinpal.com/pg/StartPay/{paymentResponse.Authority}");
        }

        [HttpGet("PayedOrderCallBack/{sId}")]
        public async Task<IActionResult> CallBack(long sId, [FromQuery] string authority, [FromQuery] string status)
        {
            var verificationResponse = _zarinPalFactory.CreateVerificationRequest(authority, (await _orderQuery.GetTotalSiteProfit(sId)).ToString());
            OperationResult result = new();

            result.Failed("تراکنش با مشکل مواجه شد! در صورت کسر مبلغ ، حداکثر تا 24 ساعت دیگر برگردانده می شود");

            if (verificationResponse.Status == 100 && status.ToLower() == "ok")
            {
                var unPayedItems = await _orderQuery.GetUnPayedItems(sId);

                var unPayedItemsId = unPayedItems.Select(i => i.Id).ToArray();
                await _orderApplication.PayedProfitToSite(unPayedItemsId);

                result.Succeeded($"هزینه درصد فروش شما به سایت پرداخت شد");

                TempData[SuccessMessage] = result.Message;

                return RedirectToAction("Index", "Account", new { area = "Store" });
            }

            TempData[ErrorMessage] = result.Message;
            return RedirectToAction("Index", "Account", new { area = "Store" });
        }
    }
}