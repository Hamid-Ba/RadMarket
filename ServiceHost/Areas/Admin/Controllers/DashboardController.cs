using Microsoft.AspNetCore.Mvc;
using ServiceHost.Tools;

namespace ServiceHost.Areas.Admin.Controllers
{
    [AdminPermissionChecker(1)]
    public class DashboardController : AdminBaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
