using Framework.Application.Authentication;
using Microsoft.AspNetCore.Mvc;
using RadMarket.Query.Contracts.Tickets;
using ReflectionIT.Mvc.Paging;
using System.Threading.Tasks;
using TicketManagement.Application.Contract.TicketAgg;

namespace ServiceHost.Areas.Store.Controllers
{
    public class TicketController : StoreBaseController
    {
        private readonly ITicketQuery _ticketQuery;
        private readonly ITicketApplication _ticketApplication;

        public TicketController(ITicketQuery ticketQuery, ITicketApplication ticketApplication)
        {
            _ticketQuery = ticketQuery;
            _ticketApplication = ticketApplication;
        }

        public IActionResult Index() => View();

        public async Task<IActionResult> Tickets(int pageIndex = 1)
        {
            var tickets = await _ticketQuery.GetUserTicketsBy(User.GetStoreId());

            var model = PagingList.Create(tickets, 10, pageIndex);
            model.Action = "Tickets";

            ViewBag.Rows = (10 * pageIndex) - 9;

            return View(model);
        }

        [HttpGet]
        public IActionResult Create() => View();

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateTicketVM command)
        {
            if (ModelState.IsValid)
            {
                command.UserId = User.GetStoreId();
                var result = await _ticketApplication.Create(command);

                if (result.IsSucceeded)
                {
                    TempData[SuccessMessage] = result.Message;
                    return RedirectToAction("Index");
                }

                TempData[ErrorMessage] = result.Message;
            }

            return View(command);
        }

        [HttpGet("ticket-detail/{id}")]
        public async Task<IActionResult> Detail(long id)
        {
            var ticket = await _ticketQuery.GetTicketDetailBy(id, User.GetStoreId());

            if (ticket is null)
            {
                TempData[ErrorMessage] = "شما دسترسی به تیکت بقیه ندارید";
                return RedirectToAction("Index");
            }

            ViewBag.StoreId = User.GetStoreId();
            return View(await _ticketQuery.GetTicketDetailBy(id, User.GetStoreId()));
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> SendMessage(AddMessageTicketVM command)
        {
            command.UserId = User.GetStoreId();
            command.SenderId = User.GetStoreId();

            if (ModelState.IsValid)
            {
                var result = await _ticketApplication.AddMessage(command);

                if (result.IsSucceeded)
                {
                    TempData[SuccessMessage] = result.Message;
                    return RedirectToAction("Detail", new { id = command.TicketId, area = "Store" });
                }

                TempData[ErrorMessage] = result.Message;
            }

            return RedirectToAction("Index");
        }
    }
}
