using Microsoft.AspNetCore.Mvc;
using ServiceHost.Tools;

namespace ServiceHost.Areas.Store.Controllers
{
    [StorePermissionChecker(1)]
    public class DashboardController : StoreBaseController
    {
        public IActionResult Index() => View();

        public IActionResult Operators() => View();
    }
}
