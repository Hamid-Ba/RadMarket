using Microsoft.AspNetCore.Mvc;

namespace ServiceHost.Areas.User.Controllers
{
    public class Dashboard : UserBaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
