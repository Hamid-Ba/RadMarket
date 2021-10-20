using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RadMarket.Query.Contracts.StoreAgg;
using ReflectionIT.Mvc.Paging;
using ServiceHost.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceHost.Controllers
{
    public class HomeController : BaseController
    {
        private readonly IStoreQuery _storeQuery;
        private readonly ILogger<HomeController> _logger;

        public HomeController(IStoreQuery storeQuery, ILogger<HomeController> logger)
        {
            _storeQuery = storeQuery;
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Stores(int pageIndex = 1)
        {
            var stores = await _storeQuery.GetAll();

            var model = PagingList.Create(stores, 10, pageIndex);
            model.Action = "Stores";

            return View(model);
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        [Route("NotFound")]
        public IActionResult NotFound()
        {
            TempData[WarningMessage] = "صفحه مورد نظر پیدا نشد!";
            return View();
        }
    }
}
