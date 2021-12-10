using Framework.Application.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TicketManagement.Application.Contract.TicketAgg;

namespace ServiceHost.Areas.Admin.Controllers
{
    public class TicketController : AdminBaseController
    {
        private readonly ITicketApplication _ticketApplication;

        public TicketController(ITicketApplication ticketApplication) => _ticketApplication = ticketApplication;

        public async Task<IActionResult> Index() => View(await _ticketApplication.GetAll());

        [HttpGet]
        public async Task<IActionResult> Detail(long id) => PartialView(await _ticketApplication.GetMessages(id));
        
        [HttpGet]
        public IActionResult Close(long id) => PartialView("Close", id);

        [HttpPost("Close"), ValidateAntiForgeryToken]
        public async Task<IActionResult> CloseTicket(long id)
        {
            var result = await _ticketApplication.OpenOrClose(id, true);

            if (result.IsSucceeded) TempData[SuccessMessage] = result.Message;

            return new JsonResult(result);
        }

        [HttpGet]
        public IActionResult SendMessage(long id) => PartialView(new AddMessageTicketVM() { TicketId = id });

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SendMessage(AddMessageTicketVM command)
        {
            command.UserId = User.GetUserId();
            var result = await _ticketApplication.SendResponse(command);

            if (result.IsSucceeded) TempData[SuccessMessage] = result.Message;

            return new JsonResult(result);
        }

    }
}