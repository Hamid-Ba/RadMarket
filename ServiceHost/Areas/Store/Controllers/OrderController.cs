using AccountManagement.Application.Contract.UserAgg;
using Framework.Application.Authentication;
using Framework.Peresentation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using RadMarket.Query.Contracts.OrderAgg;
using ReflectionIT.Mvc.Paging;
using ServiceHost.Tools;
using StoreManagement.Application.Contract.OrderAgg;
using System.Threading.Tasks;

namespace ServiceHost.Areas.Store.Controllers
{
    [StorePermissionChecker(SellerPermissionHelper.Orders)]
    public class OrderController : StoreBaseController
    {
        private readonly IOrderQuery _orderQuery;
        private readonly IUserApplication _userApplication;
        private readonly IOrderApplication _orderApplication;

        public OrderController(IOrderQuery orderQuery, IUserApplication userApplication, IOrderApplication orderApplication)
        {
            _orderQuery = orderQuery;
            _userApplication = userApplication;
            _orderApplication = orderApplication;
        }

        public async Task<IActionResult> Index(string code, int pageIndex = 1)
        {
            var result = await _orderQuery.GetStoreOrders(User.GetStoreId(), code);

            if (result is null)
            {
                TempData[WarningMessage] = "هیچ سفارشی برای شما به ثبت نرسیده است";
                return RedirectToAction("Index", "Dashboard", new { area = "User" });
            }

            var model = PagingList.Create(result, 10, pageIndex);

            ViewBag.Rows = (10 * pageIndex) - 9;

            model.RouteValue = new RouteValueDictionary()
            {
                {"code", code},
            };

            return View(model);
        }

        public async Task<IActionResult> Detail(long itemId)
        {
            var result = await _orderQuery.GetUserItemsForStore(itemId, User.GetStoreId());

            if (result == null)
            {
                TempData[ErrorMessage] = "مشکلی به وجود آمده";
                return RedirectToAction("Index");
            }

            ViewBag.Address = await _userApplication.GetAddressInfoBy(result.UserId);
            ViewBag.Code = await _orderApplication.GetIssueTrackingBy(result.OrderId);
            ViewBag.Name = await _userApplication.GetUserFullNameBy(result.UserId);

            return View(result);
        }

        [HttpGet]
        public async Task<IActionResult> ChangeStatus(long itemId)
        {
            var result = await _orderApplication.GetDetailForChangeStatus(itemId);

            if (result == null)
            {
                TempData[ErrorMessage] = "مشکلی به وجود آمده";
                return RedirectToAction("Index");
            }

            ViewBag.Code = await _orderApplication.GetIssueTrackingBy(result.OrderId);
            ViewBag.Name = await _userApplication.GetUserFullNameBy(result.UserId);
            return View(await _orderApplication.GetDetailForChangeStatus(itemId));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangeStatus(ChangeOrderStatusVM command)
        {
            var result = await _orderApplication.ChangeOrderStatus(command);

            if (result.IsSucceeded)
            {
                TempData[SuccessMessage] = result.Message;
                return RedirectToAction("Index");
            }

            TempData[ErrorMessage] = result.Message;
            return View(command);
        }
    }
}
