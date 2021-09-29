﻿using AccountManagement.Application.Contract.StoreUserAgg;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ServiceHost.Areas.Admin.Controllers
{
    public class StoreUserController : AdminBaseController
    {
        private readonly IStoreUserApplication _storeUserApplication;

        public StoreUserController(IStoreUserApplication storeUserApplication) => _storeUserApplication = storeUserApplication;

        public async Task<IActionResult> Index() => View(await _storeUserApplication.GetAll());

        [HttpGet]
        public IActionResult Delete(long id) => PartialView(id);

        [ActionName("Delete")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PostDelete(long id)
        {
            var result = await _storeUserApplication.Delete(id);

            if (result.IsSucceeded) TempData[SuccessMessage] = result.Message;

            return new JsonResult(result);
        }

        [HttpGet]
        public async Task<IActionResult> AddressInfo(long id) => PartialView(await _storeUserApplication.GetAddressInfoBy(id));

    }
}
