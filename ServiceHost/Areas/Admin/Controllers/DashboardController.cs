using Microsoft.AspNetCore.Mvc;

namespace ServiceHost.Areas.Admin.Controllers
{
    public class DashboardController : AdminBaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
