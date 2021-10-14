using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceHost.Areas.Admin.Controllers
{
    public class VisitorController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
