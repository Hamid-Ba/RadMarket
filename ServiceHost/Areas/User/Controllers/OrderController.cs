﻿using Framework.Application.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using RadMarket.Query.Contracts.OrderAgg;
using ReflectionIT.Mvc.Paging;
using System.Threading.Tasks;

namespace ServiceHost.Areas.User.Controllers
{
    public class OrderController : UserBaseController
    {
        private readonly IOrderQuery _orderQuery;

        public OrderController(IOrderQuery orderQuery)
        {
            _orderQuery = orderQuery;
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
    }
}