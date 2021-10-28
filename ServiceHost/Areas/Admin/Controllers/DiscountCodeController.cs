using DiscountManagement.Application.Contract.DiscountCodeAgg;
using DiscountManagement.Infrastructure.Configuration;
using Microsoft.AspNetCore.Mvc;
using ServiceHost.Tools;
using System.Threading.Tasks;

namespace ServiceHost.Areas.Admin.Controllers
{
    [AdminPermissionChecker(DiscountPermissionHelper.DiscountCode)]
    public class DiscountCodeController : AdminBaseController
    {
        private readonly IDiscountCodeApplication _discountCodeApplication;

        public DiscountCodeController(IDiscountCodeApplication discountCodeApplication) => _discountCodeApplication = discountCodeApplication;

        public async Task<IActionResult> Index() => View(await _discountCodeApplication.GetAll());

        [HttpGet]
        public IActionResult Create() => PartialView();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateDiscountCodeVM command)
        {
            var result = await _discountCodeApplication.Create(command);

            if (result.IsSucceeded) TempData[SuccessMessage] = result.Message;

            return new JsonResult(result);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(long id) => PartialView(await _discountCodeApplication.GetDetailForEditBy(id));

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditDiscountCodeVM command)
        {
            var result = await _discountCodeApplication.Edit(command);

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
            var result = await _discountCodeApplication.Delete(id);

            if (result.IsSucceeded) TempData[SuccessMessage] = result.Message;

            return new JsonResult(result);
        }
    }
}
