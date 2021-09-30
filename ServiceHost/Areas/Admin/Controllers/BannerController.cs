using AdminManagement.Application.Contract.BannerAgg;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using StoreManagement.Application.Contract.StoreAgg;
using System.Threading.Tasks;

namespace ServiceHost.Areas.Admin.Controllers
{
    public class BannerController : AdminBaseController
    {
        private readonly IStoreApplication _storeApplication;
        private readonly IBannerApplication _bannerApplication;

        public BannerController(IStoreApplication storeApplication, IBannerApplication bannerApplication)
        {
            _storeApplication = storeApplication;
            _bannerApplication = bannerApplication;
        }

        public async Task<IActionResult> Index() => View(await _bannerApplication.GetAll());

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.Stores = new SelectList(await _storeApplication.GetAll(), "Id", "Name");
            return PartialView();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateBannerVM command)
        {
            ViewBag.Stores = new SelectList(await _storeApplication.GetAll(), "Id", "Name");
            var result = await _bannerApplication.Create(command);

            if (result.IsSucceeded) TempData[SuccessMessage] = result.Message;

            return new JsonResult(result);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(long id)
        {
            ViewBag.Stores = new SelectList(await _storeApplication.GetAll(), "Id", "Name");
            return PartialView(await _bannerApplication.GetDetailForEditBy(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditBannerVM command)
        {
            ViewBag.Stores = new SelectList(await _storeApplication.GetAll(), "Id", "Name");
            var result = await _bannerApplication.Edit(command);

            if (result.IsSucceeded) TempData[SuccessMessage] = result.Message;

            return new JsonResult(result);
        }

        [HttpGet]
        public async Task<IActionResult> ChangeStatus(long id)
        {
            var result = await _bannerApplication.ActiveOrDeActive(id);

            if (result.IsSucceeded) TempData[SuccessMessage] = result.Message;
            else TempData[ErrorMessage] = result.Message;

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Delete(long id) => PartialView(id);

        [ActionName("Delete")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PostDelete(long id)
        {
            var result = await _bannerApplication.Delete(id);

            if (result.IsSucceeded) TempData[SuccessMessage] = result.Message;

            return new JsonResult(result);
        }
    }
}
