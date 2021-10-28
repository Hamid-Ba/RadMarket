using AccountManagement.Application.Contract.UserAgg;
using AccountManagement.Infrastructure.Configuration;
using Microsoft.AspNetCore.Mvc;
using ServiceHost.Tools;
using StoreManagement.Application.Contract.VisitorAgg;
using System.Threading.Tasks;

namespace ServiceHost.Areas.Admin.Controllers
{
    [AdminPermissionChecker(AccountPermissionHelper.Visitors)]
    public class VisitorController : AdminBaseController
    {
        private readonly IUserApplication _userApplication;
        private readonly IVisitorApplication _visitorApplication;

        public VisitorController(IUserApplication userApplication, IVisitorApplication visitorApplication)
        {
            _userApplication = userApplication;
            _visitorApplication = visitorApplication;
        }

        public async Task<IActionResult> Index() => View(await _visitorApplication.GetAll());

        public async Task<IActionResult> InvitedUser(string code) => PartialView(await _userApplication.GetAllBy(code));

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