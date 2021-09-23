using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AdminManagement.Application.Contract.ProvinceAgg;

namespace ServiceHost.Areas.Admin.Controllers
{
    public class ProvinceController : AdminBaseController
    {
        private readonly IProvinceApplication _provinceApplication;

        public ProvinceController(IProvinceApplication provinceApplication) => _provinceApplication = provinceApplication;

        public async Task<IActionResult> Index() => View(await _provinceApplication.GetAll());

        [HttpGet]
        public IActionResult Create() => PartialView();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateProvinceVM command)
        {
            var result = await _provinceApplication.Create(command);

            if (result.IsSucceeded) TempData[SuccessMessage] = result.Message;

            return new JsonResult(result);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(long id) => PartialView(await _provinceApplication.GetDetailForEditBy(id));

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditProvinceVM command)
        {
            var result = await _provinceApplication.Edit(command);

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
            var result = await _provinceApplication.Delete(id);

            if (result.IsSucceeded) TempData[SuccessMessage] = result.Message;

            return new JsonResult(result);
        }
    }
}