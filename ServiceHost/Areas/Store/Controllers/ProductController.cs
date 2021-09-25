using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
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

        public async Task<IActionResult> Index(string code, int pageIndex = 1)
        {
            var products = await _productApplication.GetAll();

            var model = PagingList.Create(products, 10, pageIndex);

            model.RouteValue = new RouteValueDictionary()
            {
                {"code", code}
            };

            //if (pageIndex > model.PageCount) return NotFound();

            return View(model);
        }

        public async Task<IActionResult> Create()
        {
            ViewBag.Categories = new SelectList(await _categoryApplication.GetAll(), "Id", "Name");
            return View();
        }
    }
}