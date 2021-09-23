using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Framework.Domain;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.TagHelpers.Cache;
using RadMarket.Query.Contracts.ProvinceAgg;
using StoreManagement.Application.Contract.StoreAgg;

namespace ServiceHost.Areas.Admin.Controllers
{
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
        public async Task<IActionResult> Edit(long id)
        {
            ViewBag.Provinces = new SelectList(await _provinceQuery.GetAll(), "Name", "Name");
            return PartialView(await _storeApplication.GetDetailForEditBy(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditStoreVM command)
        {
            var result = await _storeApplication.Edit(command);

            if (result.IsSucceeded) TempData[SuccessMessage] = result.Message;
            else TempData[ErrorMessage] = result.Message;

            return new JsonResult(result);
        }

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

        [HttpGet]
        public async Task<IActionResult> AddressInfo(long id) => PartialView(await _storeApplication.GetAddressInfoBy(id));

        [HttpGet]
        public async Task<IActionResult> BankInfo(long id) => PartialView(await _storeApplication.GetBankInfoBy(id));

    }
}