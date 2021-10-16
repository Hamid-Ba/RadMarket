using AccountManagement.Application.Contract.StoreUserAgg;
using Framework.Application.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using RadMarket.Query.Contracts.ProvinceAgg;
using ReflectionIT.Mvc.Paging;
using StoreManagement.Application.Contract.StoreAgg;
using System.Threading.Tasks;

namespace ServiceHost.Areas.Store.Controllers
{
    public class StoreUserController : StoreBaseController
    {
        private readonly IProvinceQuery _provinceQuery;
        private readonly IStoreApplication _storeApplication;
        private readonly IStoreUserApplication _storeUserApplication;

        public StoreUserController(IProvinceQuery provinceQuery, IStoreApplication storeApplication, IStoreUserApplication storeUserApplication)
        {
            _provinceQuery = provinceQuery;
            _storeApplication = storeApplication;
            _storeUserApplication = storeUserApplication;
        }

        public async Task<IActionResult> Index(int pageIndex = 1)
        {
            var users = await _storeUserApplication.GetAll(User.GetStoreId());

            var model = PagingList.Create(users, 10, pageIndex);

            ViewBag.Rows = (10 * pageIndex) - 9;

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult>  Create()
        {
            ViewBag.Provinces = new SelectList(await _provinceQuery.GetAll(), "Name", "Name");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RegisterStoreUserVM command)
        {
            ViewBag.Provinces = new SelectList(await _provinceQuery.GetAll(), "Name", "Name");
            
            var result = await _storeUserApplication.Register(command);

            if (result.Item1.IsSucceeded)
            {
                var initial = await _storeUserApplication.InitialStore(result.Item2, User.GetStoreId(), User.GetStoreCode());

                if (initial.IsSucceeded)
                {
                    TempData[SuccessMessage] = initial.Message;
                    return RedirectToAction("Index");
                }
                TempData[ErrorMessage] = initial.Message;
            }
            else TempData[ErrorMessage] = result.Item1.Message;

            return View(command);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(long id)
        {
            ViewBag.Provinces = new SelectList(await _provinceQuery.GetAll(), "Name", "Name");
            return View(await _storeUserApplication.GetDetailForEditBy(id,User.GetStoreId()));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditStoreUserVM command)
        {
            ViewBag.Provinces = new SelectList(await _provinceQuery.GetAll(), "Name", "Name");

            var result = await _storeUserApplication.Edit(command);

            if (result.IsSucceeded)
            {
                TempData[SuccessMessage] = result.Message;
                return RedirectToAction("Index");
            }

            TempData[ErrorMessage] = result.Message;

            return View(command);
        }

        public async Task<IActionResult> Delete(long id)
        {
            if (ModelState.IsValid)
            {
                var adminId = await _storeApplication.GetStoreAdminId(User.GetStoreId());

                if(id == adminId)
                {
                    TempData[WarningMessage] = "مدیر سایت را نمی توانید حذف کنید";
                    return RedirectToAction("Index");
                }

                var result = await _storeUserApplication.Delete(id,User.GetStoreId());

                if (result.IsSucceeded) TempData[SuccessMessage] = result.Message;
                else TempData[ErrorMessage] = result.Message;
            }

            return RedirectToAction("Index");
        }
    }
}