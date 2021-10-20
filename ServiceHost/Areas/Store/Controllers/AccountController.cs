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

        public async Task<IActionResult> Index() => View(await _orderQuery.GetUnPayedItems(User.GetStoreId()));
        
    }
}
