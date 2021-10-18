using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Framework.Application.Authentication;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Routing;
using ReflectionIT.Mvc.Paging;
using StoreManagement.Application.Contract.CategoryAgg;
using StoreManagement.Application.Contract.ProductAgg;
using StoreManagement.Application.Contract.StoreAgg;
using ServiceHost.Tools;

namespace ServiceHost.Areas.Store.Controllers
{
    [StorePermissionChecker(2)]
    public class ProductController : StoreBaseController
    {
        private readonly IStoreApplication _storeApplication;
        private readonly IProductApplication _productApplication;
        private readonly ICategoryApplication _categoryApplication;

        public ProductController(IStoreApplication storeApplication, IProductApplication productApplication, ICategoryApplication categoryApplication)
        {
            _storeApplication = storeApplication;
            _productApplication = productApplication;
            _categoryApplication = categoryApplication;
        }

        public async Task<IActionResult> Index(SearchStoreVM search, int pageIndex = 1)
        {
            var storeHasStillCharged = await _storeApplication.IsAccountStillCharged(User.GetStoreId());
            if(!storeHasStillCharged.IsSucceeded)
            {
                TempData[WarningMessage] = storeHasStillCharged.Message;
                return RedirectToAction("Index","Dashboard",new { area = "Store" });
            }

            var products = await _productApplication.GetAll(User.GetStoreId(), search);

            var model = PagingList.Create(products, 10, pageIndex);

            ViewBag.Rows = (10 * pageIndex) - 9;

            model.RouteValue = new RouteValueDictionary()
            {
                {"search", search},
            };

            return View(model);
        }

        [HttpGet]
        [StorePermissionChecker(3)]
        public async Task<IActionResult> Create()
        {
            var storeHasStillCharged = await _storeApplication.IsAccountStillCharged(User.GetStoreId());
            if (!storeHasStillCharged.IsSucceeded)
            {
                TempData[WarningMessage] = storeHasStillCharged.Message;
                return RedirectToAction("Index", "Dashboard", new { area = "Store" });
            }

            var storeIsAbleToAddProduct = await _storeApplication.IsAbleToAddProduct(User.GetStoreId());
            if (!storeIsAbleToAddProduct.IsSucceeded)
            {
                TempData[WarningMessage] = storeIsAbleToAddProduct.Message;
                return RedirectToAction("Index", "Dashboard", new { area = "Store" });
            }

            ViewBag.Categories = new SelectList(await _categoryApplication.GetAll(), "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateProductVM command)
        {
            ViewBag.Categories = new SelectList(await _categoryApplication.GetAll(), "Id", "Name");
            if (ModelState.IsValid)
            {
                command.StoreId = User.GetStoreId();
                var result = await _productApplication.Create(command);

                await _storeApplication.ProductCreated(command.StoreId);

                if (result.IsSucceeded)
                {
                    TempData[SuccessMessage] = result.Message;
                    return RedirectToAction("Index", "Product");
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

            if (!await _productApplication.IsProductBelongToStore(id, User.GetStoreId()))
                return RedirectToAction("NotFound", "Home", new { area = "" });

            ViewBag.Categories = new SelectList(await _categoryApplication.GetAll(), "Id", "Name");
            return View(await _productApplication.GetDetailForEditBy(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditProductVM command)
        {
            if (!await _productApplication.IsProductBelongToStore(command.Id, User.GetStoreId()))
                return RedirectToAction("NotFound", "Home", new { area = "" });

            ViewBag.Categories = new SelectList(await _categoryApplication.GetAll(), "Id", "Name");
            if (ModelState.IsValid)
            {
                command.StoreId = User.GetStoreId();
                var result = await _productApplication.Edit(command);

                if (result.IsSucceeded)
                {
                    TempData[SuccessMessage] = result.Message;
                    return RedirectToAction("Index", "Product");
                }

                TempData[ErrorMessage] = result.Message;
            }

            return View(command);
        }

    }
}