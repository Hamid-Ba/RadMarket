using Microsoft.AspNetCore.Mvc;
using StoreManagement.Application.Contract.VisitorAgg;
using System.Threading.Tasks;

namespace ServiceHost.Areas.Admin.Controllers
{
    public class VisitorController : AdminBaseController
    {
        private readonly IVisitorApplication _visitorApplication;

        public VisitorController(IVisitorApplication visitorApplication) => _visitorApplication = visitorApplication;

        public async Task<IActionResult> Index() => View(await _visitorApplication.GetAll());

        [HttpGet]
        public IActionResult Create() => PartialView();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateVisitorVM command)
        {
            var result = await _visitorApplication.Create(command);

            if (result.IsSucceeded) TempData[SuccessMessage] = result.Message;

            return new JsonResult(result);
        }

        [HttpGet]
        public async Task<IActionResult>  Edit(long id) => PartialView(await _visitorApplication.GetDetailForEditBy(id));

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditVisitorVM command)
        {
            var result = await _visitorApplication.Edit(command);

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
            var result = await _visitorApplication.Delete(id);

            if (result.IsSucceeded) TempData[SuccessMessage] = result.Message;

            return new JsonResult(result);
        }
    }
}