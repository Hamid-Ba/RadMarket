using Framework.Application.Authentication;
using Microsoft.AspNetCore.Mvc;
using RadMarket.Query.Contracts.OrderAgg;
using System.Threading.Tasks;

namespace ServiceHost.Areas.Store.Controllers
{
    public class AccountController : StoreBaseController
    {
        private readonly IOrderQuery _orderQuery;

        public AccountController(IOrderQuery orderQuery) => _orderQuery = orderQuery;

        public async Task<IActionResult> Index()
        {
            ViewBag.TotalSiteProfitAmount = await _orderQuery.GetTotalSiteProfit(User.GetStoreId());
            return View(await _orderQuery.GetUnPayedItems(User.GetStoreId()));
        }
    }
}
