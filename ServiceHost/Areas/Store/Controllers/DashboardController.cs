using Microsoft.AspNetCore.Mvc;

namespace ServiceHost.Areas.Store.Controllers
{
    public class DashboardController : StoreBaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
