using AccountManagement.Application.Contract.AdminRoleAgg;
using AccountManagement.Application.Contract.AdminUserAgg;
using AccountManagement.Infrastructure.Configuration;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ServiceHost.Tools;
using System.Threading.Tasks;

namespace ServiceHost.Areas.Admin.Controllers
{
    [AdminPermissionChecker(AccountPermissionHelper.AdminUsers)]
    public class AdminUserController : AdminBaseController
    {
        private readonly IAdminRoleApplication _adminRoleApplication;
        private readonly IAdminUserApplication _adminUserApplication;

        public AdminUserController(IAdminRoleApplication adminRoleApplication, IAdminUserApplication adminUserApplication)
        {
            _adminRoleApplication = adminRoleApplication;
            _adminUserApplication = adminUserApplication;
        }

        public async Task<IActionResult> Index() => View(await _adminUserApplication.GetAll());

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.Roles = new SelectList(await _adminRoleApplication.GetAll(), "Id", "Name");
            return PartialView();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateAdminUserVM command)
        {
            ViewBag.Roles = new SelectList(await _adminRoleApplication.GetAll(), "Id", "Name");

            var result = await _adminUserApplication.Create(command);

            if (!result.IsSucceeded) TempData[ErrorMessage] = result.Message;

            return new JsonResult(result);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(long id)
        {
            ViewBag.Roles = new SelectList(await _adminRoleApplication.GetAll(), "Id", "Name");
            return PartialView(await _adminUserApplication.GetDetailForEditBy(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditAdminUserVM command)
        {
            ViewBag.Roles = new SelectList(await _adminRoleApplication.GetAll(), "Id", "Name");

            var result = await _adminUserApplication.Edit(command);

            if (!result.IsSucceeded) TempData[ErrorMessage] = result.Message;

            return new JsonResult(result);
        }

        [HttpGet]
        public IActionResult Delete(long id) => PartialView(id);

        [ActionName("Delete")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PostDelete(long id)
        {
            var result = await _adminUserApplication.Delete(id);

            if (result.IsSucceeded) TempData[SuccessMessage] = result.Message;

            return new JsonResult(result);
        }
    }
}
