using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using AccountManagement.Application.Contract.StoreUserAgg;
using Framework.Domain;
using StoreManagement.Application.Contract.StoreAgg;

namespace ServiceHost.Areas.Admin.Controllers
{
    public class StoreController : AdminBaseController
    {
        private readonly IStoreApplication _storeApplication;

        public StoreController(IStoreApplication storeApplication)
        {
            _storeApplication = storeApplication;
        }

        public async Task<IActionResult> Index() => View(await _storeApplication.GetAll());

        [HttpGet("Confirm-Request")]
        public IActionResult Confirm(long id) => PartialView("Confirm", id);

        [HttpPost("Confirm-Request"), ValidateAntiForgeryToken]
        [Route("Confirm")]
        public async Task<IActionResult> ConfirmRequest(long id)
        {
            var result = await _storeApplication.ChangeStatus(new SpecifyStatusOfStoreVM()
            {
                Id = id,
                Status = StoreStatus.Confirmed,
                StoreGivenStatusReason = "شرکت تایید شد"
            });

            if (result.IsSucceeded) TempData[SuccessMessage] = result.Message;

            return new JsonResult(result);
        }

        [HttpGet("DissConfirm-Request")]
        public IActionResult DissConfirm(long id) => PartialView("DissConfirm", new SpecifyStatusOfStoreVM() { Id = id });

        [HttpPost("DissConfirm-Request"), ValidateAntiForgeryToken]
        public async Task<IActionResult> DissConfirm(SpecifyStatusOfStoreVM command)
        {
            command.Status = StoreStatus.Rejected;

            var result = await _storeApplication.ChangeStatus(command);

            if (result.IsSucceeded) TempData[SuccessMessage] = result.Message;

            return new JsonResult(result);
        }
    }
}