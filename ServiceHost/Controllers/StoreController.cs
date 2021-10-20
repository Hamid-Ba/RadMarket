using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceHost.Controllers
{
    public class StoreController : Controller
    {
       
        public IActionResult Index(long id ,string name)
        {
            return View();
        }
    }
}
