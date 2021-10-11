using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using DiscountManagement.Application.Contract.DiscountAgg;
using Framework.Application.Authentication;
using Microsoft.AspNetCore.Mvc.Rendering;
using ReflectionIT.Mvc.Paging;
using StoreManagement.Application.Contract.ProductAgg;
using StoreManagement.Application.Contract.StoreAgg;

namespace ServiceHost.Areas.Store.Controllers
{
    public class DiscountController : StoreBaseController
    {
        private readonly IStoreApplication _storeApplication;
        private readonly IProductApplication _productApplication;
        private readonly IDiscountApplication _discountApplication;

        public DiscountController(IStoreApplication storeApplication, IProductApplication productApplication, IDiscountApplication discountApplication)
        {
            _storeApplication = storeApplication;
            _productApplication = productApplication;
            _discountApplication = discountApplication;
        }

        public async Task<IActionResult> Index(int pageIndex = 1)
        {
            var storeHasStillCharged = await _storeApplication.IsAccountStillCharged(User.GetStoreId());
            if (!storeHasStillCharged.IsSucceeded)
            {
                TempData[WarningMessage] = storeHasStillCharged.Message;
                return RedirectToAction("Index", "Dashboard", new { area = "Store" });
            }

            var discounts = await _discountApplication.GetAllBy(User.GetStoreId());

            var model = PagingList.Create(discounts, 10, pageIndex);

            ViewBag.Rows = (10 * pageIndex) - 9;

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Create(long id)
        {
            var storeHasStillCharged = await _storeApplication.IsAccountStillCharged(User.GetStoreId());
            if (!storeHasStillCharged.IsSucceeded)
            {
                TempData[WarningMessage] = storeHasStillCharged.Message;
                return RedirectToAction("Index", "Dashboard", new { area = "Store" });
            }

            ViewBag.Products = new SelectList(await _productApplication.GetAll(User.GetStoreId(),null), "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateDiscountVM command)
        {
            ViewBag.Products = new SelectList(await _productApplication.GetAll(User.GetStoreId(), null), "Id", "Name");
            if (ModelState.IsValid)
            {
                command.StoreId = User.GetStoreId();
                var result = await _discountApplication.Create(command);

                if (result.IsSucceeded)
                {
                    TempData[SuccessMessage] = result.Message;
                    return RedirectToAction("Index");
                }

                TempData[ErrorMessage] = result.Message;
            }

            return View(command);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(long id)
        {
            var storeHasStillCharged = await _storeApplication.IsAccountStillCharged(User.GetStoreId());
            if (!storeHasStillCharged.IsSucceeded)
            {
                TempData[WarningMessage] = storeHasStillCharged.Message;
                return RedirectToAction("Index", "Dashboard", new { area = "Store" });
            }

            ViewBag.Products = new SelectList(await _productApplication.GetAll(User.GetStoreId(), null), "Id", "Name");
            return View(await _discountApplication.GetDetailForEditBy(id, User.GetStoreId()));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditDiscountVM command)
        {
            ViewBag.Products = new SelectList(await _productApplication.GetAll(User.GetStoreId(), null), "Id", "Name");
            if (ModelState.IsValid)
            {
                command.StoreId = User.GetStoreId();
                var result = await _discountApplication.Edit(command);

                if (result.IsSucceeded)
                {
                    TempData[SuccessMessage] = result.Message;
                    return RedirectToAction("Index");
                }

                TempData[ErrorMessage] = result.Message;
            }

            return View(command);
        }

        public async Task<IActionResult> Delete(long id)
        {
            var storeHasStillCharged = await _storeApplication.IsAccountStillCharged(User.GetStoreId());
            if (!storeHasStillCharged.IsSucceeded)
            {
                TempData[WarningMessage] = storeHasStillCharged.Message;
                return RedirectToAction("Index", "Dashboard", new { area = "Store" });
            }

            if (ModelState.IsValid)
            {
                var result = await _discountApplication.Delete(id);

                if (result.IsSucceeded) TempData[SuccessMessage] = result.Message;
                else TempData[ErrorMessage] = result.Message;
            }

            return RedirectToAction("Index");
        }

    }
}