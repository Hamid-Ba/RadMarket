using Microsoft.AspNetCore.Mvc;
using StoreManagement.Application.Contract.AdtPackageAgg;
using System.Threading.Tasks;

namespace ServiceHost.Areas.Admin.Controllers
{
    public class AdtPackageController : AdminBaseController
    {
        private readonly IAdtPackageApplication _adtPackageApplication;

        public AdtPackageController(IAdtPackageApplication packageApplication) => _adtPackageApplication = packageApplication;

        public async Task<IActionResult> Index() => View(await _adtPackageApplication.GetAll());

        [HttpGet]
        public IActionResult Create() => PartialView();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateAdtPackageVM command)
        {
            var result = await _adtPackageApplication.Create(command);

            if (result.IsSucceeded) TempData[SuccessMessage] = result.Message;

            return new JsonResult(result);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(long id) => PartialView(await _adtPackageApplication.GetDetailForEditBy(id));

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditAdtPackageVM command)
        {
            var result = await _adtPackageApplication.Edit(command);

            if (result.IsSucceeded) TempData[SuccessMessage] = result.Message;

            return new JsonResult(result);
        }

        [HttpGet]
        public IActionResult Delete(long id) => PartialView(id);

        [ActionName("Delete")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PostDelete(long id)
        {
            var result = await _adtPackageApplication.Delete(id);

            if (result.IsSucceeded) TempData[SuccessMessage] = result.Message;

            return new JsonResult(result);
        }
    }
}
