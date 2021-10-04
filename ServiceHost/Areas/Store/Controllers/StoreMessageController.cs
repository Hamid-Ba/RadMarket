using Framework.Application.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using RadMarket.Query.Contracts.StoreTicketAgg;
using ReflectionIT.Mvc.Paging;
using StoreManagement.Application.Contract.StoreAgg;
using System.Threading.Tasks;
using TicketManagement.Application.Contract.StoreTicketAgg;

namespace ServiceHost.Areas.Store.Controllers
{
    public class StoreMessageController : StoreBaseController
    {
        private readonly IStoreTicketQuery _storeTicketQuery;
        private readonly IStoreApplication _storeApplication;
        private readonly IStoreTicketApplication _storeTicketApplication;

        public StoreMessageController(IStoreTicketQuery storeTicketQuery, IStoreApplication storeApplication, IStoreTicketApplication storeTicketApplication)
        {
            _storeTicketQuery = storeTicketQuery;
            _storeApplication = storeApplication;
            _storeTicketApplication = storeTicketApplication;
        }

        public async Task<IActionResult> Index(int pageIndex = 1)
        {
            var tickets = await _storeTicketQuery.GetAllBy(User.GetStoreId());

            var model = PagingList.Create(tickets, 10, pageIndex);

            ViewBag.Rows = (10 * pageIndex) - 9;

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult>  Create()
        {
            ViewBag.Stores = new SelectList(await _storeApplication.GetAll(), "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateStoreTicketVM command)
        {
            command.FirstStoreId = User.GetStoreId();
            if (ModelState.IsValid)
            {
                var result = await _storeTicketApplication.Create(command);

                if (result.IsSucceeded)
                {
                    TempData[SuccessMessage] = result.Message;
                    return RedirectToAction("Index");
                }

                TempData[ErrorMessage] = result.Message;
            }
            ViewBag.Stores = new SelectList(await _storeApplication.GetAll(), "Id", "Name");
            return View(command);
        }
    }
}
