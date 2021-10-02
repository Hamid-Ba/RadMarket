using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceHost.Areas.Store.Controllers
{
    public class OrderController : StoreBaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
