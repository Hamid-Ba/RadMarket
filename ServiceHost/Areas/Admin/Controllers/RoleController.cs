using AccountManagement.Application.Contract.AdminPermissionAgg;
using AccountManagement.Application.Contract.AdminRoleAgg;
using AccountManagement.Infrastructure.Configuration;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ServiceHost.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceHost.Areas.Admin.Controllers
{
    [AdminPermissionChecker(AccountPermissionHelper.Roles)]
    public class RoleController : AdminBaseController
    {
        private readonly IAdminRoleApplication _adminRoleApplication;
        private readonly IAdminPermissionApplication _adminPermissionApplication;

        public RoleController(IAdminRoleApplication adminRoleApplication, IAdminPermissionApplication adminPermissionApplication)
        {
            _adminRoleApplication = adminRoleApplication;
            _adminPermissionApplication = adminPermissionApplication;
        }

        public async Task<IActionResult> Index() => View(await _adminRoleApplication.GetAll());

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.Permissions = new SelectList(await _adminPermissionApplication.GetAll(), "Id", "Title");
            return PartialView();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateAdminRoleVM command)
        {
            ViewBag.Permissions = new SelectList(await _adminPermissionApplication.GetAll(), "Id", "Title");

            var result = await _adminRoleApplication.Create(command);

            if (!result.IsSucceeded) TempData[ErrorMessage] = result.Message;

            return new JsonResult(result);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(long id)
        {
            ViewBag.Permissions = new SelectList(await _adminPermissionApplication.GetAll(), "Id", "Title");
            return PartialView(await _adminRoleApplication.GetDetailForEditBy(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditAdminRoleVM command)
        {
            ViewBag.Permissions = new SelectList(await _adminPermissionApplication.GetAll(), "Id", "Title");

            var result = await _adminRoleApplication.Edit(command);

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
            var result = await _adminRoleApplication.Delete(id);

            if (result.IsSucceeded) TempData[SuccessMessage] = result.Message;

            return new JsonResult(result);
        }

    }
}
