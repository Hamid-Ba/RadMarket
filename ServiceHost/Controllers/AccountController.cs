﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using RadMarket.Query.Contracts.ProvinceAgg;
using System.Threading.Tasks;
using AccountManagement.Application.Contract.AdminUserAgg;
using AccountManagement.Application.Contract.StoreUserAgg;
using AccountManagement.Application.Contract.UserAgg;
using Framework.Application.Authentication;
using StoreManagement.Application.Contract.StoreAgg;
using StoreManagement.Application.Contract.VisitorAgg;
using AccountManagement.Application.Contract.StoreRoleAgg;

namespace ServiceHost.Controllers
{
    public class AccountController : BaseController
    {
        private readonly IAuthHelper _authHelper;
        private readonly IProvinceQuery _provinceQuery;
        private readonly IUserApplication _userApplication;
        private readonly IStoreApplication _storeApplication;
        private readonly IVisitorApplication _visitorApplication;
        private readonly IStoreRoleApplication _storeRoleApplication;
        private readonly IStoreUserApplication _storeUserApplication;
        private readonly IAdminUserApplication _adminUserApplication;

        public AccountController(IAuthHelper authHelper, IProvinceQuery provinceQuery, IUserApplication userApplication, 
            IStoreApplication storeApplication, IVisitorApplication visitorApplication,
            IStoreRoleApplication storeRoleApplication, IStoreUserApplication storeUserApplication, 
            IAdminUserApplication adminUserApplication)
        {
            _authHelper = authHelper;
            _provinceQuery = provinceQuery;
            _userApplication = userApplication;
            _storeApplication = storeApplication;
            _visitorApplication = visitorApplication;
            _storeRoleApplication = storeRoleApplication;
            _storeUserApplication = storeUserApplication;
            _adminUserApplication = adminUserApplication;
        }

        #region Client User

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
                if (!string.IsNullOrWhiteSpace(command.MarketerCode))
                {
                    var visitorResult = await _visitorApplication.UserRegistered(command.MarketerCode);
                    if (!visitorResult.IsSucceeded)
                    {
                        TempData[ErrorMessage] = visitorResult.Message;
                        return View(command);
                    }
                }

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

        [HttpGet]
        public IActionResult UserLogin() => User.Identity != null && User.Identity.IsAuthenticated ? RedirectToAction("Index", "Home") : View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UserLogin(LoginUserVM command)
        {
            if (ModelState.IsValid)
            {
                var result = await _userApplication.Login(command);

                if (result.IsSucceeded)
                {
                    TempData[SuccessMessage] = result.Message;
                    return RedirectToAction("Index", "Home");
                }

                TempData[ErrorMessage] = result.Message;
            }

            return View(command);
        }

        [HttpGet]
        public IActionResult UserChangePassword() => User.Identity != null && User.Identity.IsAuthenticated ? RedirectToAction("Index", "Home") : View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UserChangePassword(string mobile)
        {
            if (string.IsNullOrWhiteSpace(mobile)) return View(mobile);

            var result = await _userApplication.ChangePassword(mobile);

            if (result.IsSucceeded) TempData[SuccessMessage] = "رمزعبور جدید برای شما ارسال شد";
            else TempData[ErrorMessage] = result.Message;

            return RedirectToAction("UserLogin");
        }

        #endregion

        #region Store User

        [HttpGet]
        public async Task<IActionResult> StoreRegister()
        {
            ViewBag.Provinces = new SelectList(await _provinceQuery.GetAll(), "Name", "Name");
            return User.Identity != null && User.Identity.IsAuthenticated ? RedirectToAction("Index", "Home") : View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> StoreRegister(CreateStoreVM command)
        {
            ViewBag.Provinces = new SelectList(await _provinceQuery.GetAll(), "Name", "Name");
            if (ModelState.IsValid)
            {
                var storeUser = new RegisterStoreUserVM
                {
                    StoreId = 0,
                    FirstName = command.FirstName,
                    LastName = command.LastName,
                    Mobile = command.MobileNumber,
                    Password = command.Password,
                    Province = command.Province,
                    City = command.City,
                    Address = command.Address
                };
                var result = await _storeUserApplication.Register(storeUser);

                if (result.Item1.IsSucceeded)
                {
                    TempData[SuccessMessage] = result.Item1.Message;
                    TempData[InfoMessage] = "پس از تایید مدیریت ،پیامی حاوی دستورالعمل برای شما ارسال خواهد شد";

                    //Define Store
                    command.StoreAdminUserId = result.Item2;
                    var resultStore = await _storeApplication.Create(command);

                    if (resultStore.Item1.IsSucceeded)
                    {
                        TempData[SuccessMessage] = resultStore.Item1.Message;

                        //Initial Store For Store Admin User
                        var storeCode = await _storeApplication.GetStoreCode(resultStore.Item2);
                        await _storeUserApplication.InitialStore(result.Item2, resultStore.Item2, storeCode);

                        //Give Store Admin Role To This User
                        await _storeRoleApplication.CreateStoreAdminRole(resultStore.Item2, result.Item2);
                    }
                    else TempData[ErrorMessage] = resultStore.Item1.Message;

                    return RedirectToAction("Index", "Home");
                }

                TempData[ErrorMessage] = result.Item1.Message;
            }

            return View(command);
        }

        [HttpGet]
        public IActionResult StoreLogin() => User.Identity != null && User.Identity.IsAuthenticated ? RedirectToAction("Index", "Home") : View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> StoreLogin(LoginStoreUserVM command)
        {
            if (ModelState.IsValid)
            {
                var isConfirmed = await _storeApplication.IsStoreConfirmedBy(command.StoreCode);

                if (!isConfirmed.IsSucceeded) TempData[ErrorMessage] = isConfirmed.Message;

                else
                {
                    var result = await _storeUserApplication.Login(command);

                    if (result.IsSucceeded)
                    {
                        TempData[SuccessMessage] = result.Message;
                        return RedirectToAction("Index", "Home");
                    }

                    TempData[ErrorMessage] = result.Message;
                }
            }

            return View(command);
        }

        [HttpGet]
        public IActionResult StoreChangePassword() => User.Identity != null && User.Identity.IsAuthenticated ? RedirectToAction("Index", "Home") : View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> StoreChangePassword(string mobile,string code)
        {
            if (string.IsNullOrWhiteSpace(mobile)) return View(mobile);

            var result = await _storeUserApplication.ChangePassword(mobile,code);

            if (result.IsSucceeded) TempData[SuccessMessage] = "رمزعبور جدید برای شما ارسال شد";
            else TempData[ErrorMessage] = result.Message;

            return RedirectToAction("StoreLogin");
        }

        #endregion

        #region Admin User

        [HttpGet]
        [Route("AdminLogin")]
        public IActionResult AdminLogin() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("AdminLogin")]
        public async Task<IActionResult> AdminLogin(LoginAdminUserVM command)
        {
            if (ModelState.IsValid)
            {
                var result = await _adminUserApplication.Login(command);

                if (result.IsSucceeded)
                {
                    TempData[SuccessMessage] = result.Message;
                    return RedirectToAction("Index", "Home");
                }

                TempData[ErrorMessage] = result.Message;
            }

            return View(command);
        }

        #endregion

        [HttpGet]
        public IActionResult Logout()
        {
            if (User.Identity.IsAuthenticated)
            {
                _authHelper.SignOut();
                TempData[SuccessMessage] = "با موفقیت خارج شدید";
            }
            else
                TempData[ErrorMessage] = "هنوز وارد نشده اید که";


            return RedirectToAction("Index", "Home");
        }
    }
}
