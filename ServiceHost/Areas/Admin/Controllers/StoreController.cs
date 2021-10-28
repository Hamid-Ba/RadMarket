using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Framework.Domain;
using Microsoft.AspNetCore.Mvc.Rendering;
using RadMarket.Query.Contracts.ProvinceAgg;
using StoreManagement.Application.Contract.StoreAgg;
using ServiceHost.Tools;
using StoreManagement.Infrastructure.Configuration;

namespace ServiceHost.Areas.Admin.Controllers
{
    [AdminPermissionChecker(StorePermissionHelper.Stores)]
    public class StoreController : AdminBaseController
    {
        private readonly IProvinceQuery _provinceQuery;
        private readonly IStoreApplication _storeApplication;

        public StoreController(IStoreApplication storeApplication, IProvinceQuery provinceQuery)
        {
            _storeApplication = storeApplication;
            _provinceQuery = provinceQuery;
        }

        public async Task<IActionResult> Index() => View(await _storeApplication.GetAll());

        [HttpGet]
        [AdminPermissionChecker(StorePermissionHelper.EditStore)]
        public async Task<IActionResult> Edit(long id)
        {
            ViewBag.Provinces = new SelectList(await _provinceQuery.GetAll(), "Name", "Name");
            return PartialView(await _storeApplication.GetDetailForEditBy(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AdminPermissionChecker(StorePermissionHelper.EditStore)]
        public async Task<IActionResult> Edit(EditStoreVM command)
        {
            var result = await _storeApplication.Edit(command);

            if (result.IsSucceeded) TempData[SuccessMessage] = result.Message;
            else TempData[ErrorMessage] = result.Message;

            return new JsonResult(result);
        }

        [HttpGet("Confirm-Request")]
        [AdminPermissionChecker(StorePermissionHelper.StoreStatus)]
        public IActionResult Confirm(long id) => PartialView("Confirm", id);

        [HttpPost("Confirm-Request"), ValidateAntiForgeryToken]
        [Route("Confirm")]
        [AdminPermissionChecker(StorePermissionHelper.StoreStatus)]
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
        [AdminPermissionChecker(StorePermissionHelper.StoreStatus)]
        public IActionResult DissConfirm(long id) => PartialView("DissConfirm", new SpecifyStatusOfStoreVM() { Id = id });

        [HttpPost("DissConfirm-Request"), ValidateAntiForgeryToken]
        [AdminPermissionChecker(StorePermissionHelper.StoreStatus)]
        public async Task<IActionResult> DissConfirm(SpecifyStatusOfStoreVM command)
        {
            command.Status = StoreStatus.Rejected;

            var result = await _storeApplication.ChangeStatus(command);

            if (result.IsSucceeded) TempData[SuccessMessage] = result.Message;

            return new JsonResult(result);
        }

        [HttpGet]
        public async Task<IActionResult> AddressInfo(long id) => PartialView(await _storeApplication.GetAddressInfoBy(id));

        [HttpGet]
        public async Task<IActionResult> BankInfo(long id) => PartialView(await _storeApplication.GetBankInfoBy(id));

        [HttpGet]
        public IActionResult SendMessage(long id) => PartialView(new SendMessageStoreVM(){ Id = id });

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SendMessage(SendMessageStoreVM command)
        {
            var result = await _storeApplication.SendMessage(command);

            if (result.IsSucceeded) TempData[SuccessMessage] = result.Message;

            return new JsonResult(result);
        }
    }
}