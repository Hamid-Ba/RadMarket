using Microsoft.AspNetCore.Mvc;

namespace ServiceHost.Areas.Store.Controllers
{
    public class DashboardController : StoreBaseController
    {
        public IActionResult Index() => View();

        public IActionResult Operators() => View();
    }
}
