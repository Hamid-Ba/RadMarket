using Framework.Application.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using RadMarket.Query.Contracts.OrderAgg;
using ReflectionIT.Mvc.Paging;
using StoreManagement.Application.Contract.OrderAgg;
using System.Threading.Tasks;

namespace ServiceHost.Areas.User.Controllers
{
    public class OrderController : UserBaseController
    {
        private readonly IOrderQuery _orderQuery;
        private readonly IOrderApplication _orderApplication;

        public OrderController(IOrderQuery orderQuery, IOrderApplication orderApplication)
        {
            _orderQuery = orderQuery;
            _orderApplication = orderApplication;
        }

        public async Task<IActionResult> Index(string code,int pageIndex = 1)
        {
            var result = await _orderQuery.GetUserOrders(User.GetUserId(),code);

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

        public async Task<IActionResult> Detail(long orderId)
        {
            var result = await _orderQuery.GetUserItems(orderId, User.GetUserId());

            if(result == null)
            {
                TempData[ErrorMessage] = "مشکلی به وجود آمده";
                return RedirectToAction("Index");
            }

            ViewBag.Code = await _orderApplication.GetIssueTrackingBy(orderId);
            return View(result);
        }
    }
}