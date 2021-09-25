using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Framework.Application.Authentication;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Routing;
using ReflectionIT.Mvc.Paging;
using StoreManagement.Application.Contract.CategoryAgg;
using StoreManagement.Application.Contract.ProductAgg;

namespace ServiceHost.Areas.Store.Controllers
{
    public class ProductController : StoreBaseController
    {
        private readonly IProductApplication _productApplication;
        private readonly ICategoryApplication _categoryApplication;

        public ProductController(IProductApplication productApplication, ICategoryApplication categoryApplication)
        {
            _productApplication = productApplication;
            _categoryApplication = categoryApplication;
        }

        public async Task<IActionResult> Index(SearchStoreVM search, int pageIndex = 1)
        {
            var products = await _productApplication.GetAll(User.GetStoreId(),search);

            var model = PagingList.Create(products, 10, pageIndex);

            ViewBag.Rows = (10 * pageIndex) - 9;

            model.RouteValue = new RouteValueDictionary()
            {
                {"search", search},
            };

            //if (pageIndex > model.PageCount) return NotFound();

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
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