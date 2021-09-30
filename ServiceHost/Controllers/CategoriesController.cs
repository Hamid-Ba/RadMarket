using Microsoft.AspNetCore.Mvc;
using RadMarket.Query.Contracts.CategoryAgg;
using RadMarket.Query.Contracts.ProductAgg;
using ReflectionIT.Mvc.Paging;
using System.Threading.Tasks;

namespace ServiceHost.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly ICategoryQuery _categoryQuery;

        public CategoriesController(ICategoryQuery categoryQuery) => _categoryQuery = categoryQuery;

        public async Task<IActionResult> Index() => View(await _categoryQuery.GetAll());

        [Route("category/{slug}")]
        public async Task<IActionResult> Category(string slug,int pageIndex = 1)
        {
            var result = await _categoryQuery.GetAllProducts(slug);            

            var model = PagingList.Create(result.Products, 6, pageIndex);

            ViewBag.Name = result.Name;
            ViewBag.Keyword = result.KeyWords;
            ViewBag.Meta = result.MetaDescription;

            return View(model);
        }

    }
}