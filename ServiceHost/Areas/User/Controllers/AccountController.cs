using System.Threading.Tasks;
using AccountManagement.Application.Contract.UserAgg;
using Framework.Application.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using RadMarket.Query.Contracts.ProvinceAgg;

namespace ServiceHost.Areas.User.Controllers
{
    public class AccountController : UserBaseController
    {
        private readonly IUserApplication _userApplication;
        private readonly IProvinceQuery _provinceQuery;

        public AccountController(IUserApplication userApplication, IProvinceQuery provinceQuery)
        {
            _userApplication = userApplication;
            _provinceQuery = provinceQuery;
        }

        [HttpGet]
        public async Task<IActionResult> Edit()
        {
            ViewBag.Provinces = new SelectList(await _provinceQuery.GetAll(), "Name", "Name");
            return View(await _userApplication.GetDetailForEditBy(User.GetUserId()));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditUserVM command)
        {
            if (ModelState.IsValid)
            {
                command.Id = User.GetUserId();
                var result = await _userApplication.Edit(command);

                if (result.IsSucceeded)
                {
                    TempData[SuccessMessage] = result.Message;
                    return RedirectToAction("Index", "Dashboard");
                }

                TempData[ErrorMessage] = result.Message;
            }

            return View(command);
        }
    }
}