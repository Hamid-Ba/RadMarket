using AccountManagement.Application.Contract.StorePermissionAgg;
using AccountManagement.Application.Contract.StoreRoleAgg;
using Framework.Application.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;

namespace ServiceHost.Areas.Store.Controllers
{
    public class StoreRoleController : StoreBaseController
    {
        private readonly IStoreRoleApplication _storeRoleApplication;
        private readonly IStorePermissionApplication _storePermissionApplication;

        public StoreRoleController(IStoreRoleApplication storeRoleApplication, IStorePermissionApplication storePermissionApplication)
        {
            _storeRoleApplication = storeRoleApplication;
            _storePermissionApplication = storePermissionApplication;
        }

        public async Task<IActionResult> Index() => View(await _storeRoleApplication.GetAll(User.GetStoreId()));

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.Permissions = new SelectList(await _storePermissionApplication.GetAll(), "Id", "Title");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateStoreRoleVM command)
        {
            command.StoreId = User.GetStoreId();
            if (ModelState.IsValid)
            {
                var result = await _storeRoleApplication.Create(command);

                if (result.IsSucceeded)
                {
                    TempData[SuccessMessage] = result.Message;
                    return RedirectToAction("Index");
                }
                TempData[ErrorMessage] = result.Message;
            }
            ViewBag.Permissions = new SelectList(await _storePermissionApplication.GetAll(), "Id", "Title");
            return View(command);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(long id)
        {
            ViewBag.Permissions = new SelectList(await _storePermissionApplication.GetAll(), "Id", "Title");
            return View(await _storeRoleApplication.GetDetailForEditBy(id,User.GetStoreId()));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditStoreRoleVM command)
        {
            command.StoreId = User.GetStoreId();
            if (ModelState.IsValid)
            {
                var result = await _storeRoleApplication.Edit(command);

                if (result.IsSucceeded)
                {
                    TempData[SuccessMessage] = result.Message;
                    return RedirectToAction("Index");
                }
                TempData[ErrorMessage] = result.Message;
            }
            ViewBag.Permissions = new SelectList(await _storePermissionApplication.GetAll(), "Id", "Title");
            return View(command);
        }

        public async Task<IActionResult> Delete(long id)
        {
            if (ModelState.IsValid)
            {
                var result = await _storeRoleApplication.Delete(id,User.GetStoreId());

                if (result.IsSucceeded) TempData[SuccessMessage] = result.Message;
                else TempData[ErrorMessage] = result.Message;
            }

            return RedirectToAction("Index");
        }

    }
}