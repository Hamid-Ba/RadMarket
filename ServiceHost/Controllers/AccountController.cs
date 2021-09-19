using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using RadMarket.Query.Contracts.ProvinceAgg;
using System.Threading.Tasks;
using AccountManagement.Application.Contract.UserAgg;

namespace ServiceHost.Controllers
{
    public class AccountController : BaseController
    {
        private readonly IProvinceQuery _provinceQuery;
        private readonly IUserApplication _userApplication;

        public AccountController(IProvinceQuery provinceQuery, IUserApplication userApplication)
        {
            _provinceQuery = provinceQuery;
            _userApplication = userApplication;
        }

        [HttpGet]
        public async Task<IActionResult> UserRegister()
        {
            ViewBag.Provinces = new SelectList(await _provinceQuery.GetAll(), "Name", "Name");
            return User.Identity != null && User.Identity.IsAuthenticated ? RedirectToAction("Index", "Home") : View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UserRegister(RegisterUserVM command)
        {
            ViewBag.Provinces = new SelectList(await _provinceQuery.GetAll(), "Name", "Name");
            if (ModelState.IsValid)
            {
                var result = await _userApplication.Register(command);

                if (result.IsSucceeded)
                {
                    TempData[SuccessMessage] = result.Message;
                    TempData[InfoMessage] = "جهت تکمیل ثبت نام کد فعال سازی برای شما ارسال شد";
                    return RedirectToAction("ActiveAccount", new ActiveAccountUserVM { Mobile = command.Mobile });
                }

                TempData[ErrorMessage] = result.Message;
            }

            return View(command);
        }

        [HttpGet]
        public IActionResult ActiveAccount(ActiveAccountUserVM command) => User.Identity != null && User.Identity.IsAuthenticated ? RedirectToAction("Index", "Home") : View(command);

        [ActionName("ActiveAccount")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PostActiveAccount(ActiveAccountUserVM command)
        {
            if (ModelState.IsValid)
            {
                var result = await _userApplication.ActiveAccount(command);

                if (result.IsSucceeded)
                {
                    TempData[SuccessMessage] = "حساب شما فعال شد";
                    return RedirectToAction("Index", "Home", new { area = "" });
                }

                TempData[ErrorMessage] = result.Message;
            }

            return View(command);
        }

    }
}
