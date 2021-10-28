using Microsoft.AspNetCore.Mvc;
using ServiceHost.Tools;
using StoreManagement.Application.Contract.PackageAgg;
using StoreManagement.Infrastructure.Configuration;
using System.Threading.Tasks;

namespace ServiceHost.Areas.Admin.Controllers
{
    [AdminPermissionChecker(StorePermissionHelper.SellerPackages)]
    public class PackageController : AdminBaseController
    {
        private readonly IPackageApplication _packageApplication;

        public PackageController(IPackageApplication packageApplication) => _packageApplication = packageApplication;

        public async Task<IActionResult> Index() => View(await _packageApplication.GetAll());

        [HttpGet]
        public IActionResult Create() => PartialView();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreatePackageVM command)
        {
            var result = await _packageApplication.Create(command);

            if (result.IsSucceeded) TempData[SuccessMessage] = result.Message;

            return new JsonResult(result);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(long id) => PartialView(await _packageApplication.GetDetailForEditBy(id));

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditPackageVM command)
        {
            var result = await _packageApplication.Edit(command);

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
            var result = await _packageApplication.Delete(id);

            if (result.IsSucceeded) TempData[SuccessMessage] = result.Message;

            return new JsonResult(result);
        }
    }
}
