using Microsoft.AspNetCore.Mvc;
using RadMarket.Query.Contracts.CategoryAgg;
using RadMarket.Query.Contracts.ProductAgg;
using ReflectionIT.Mvc.Paging;
using System.Threading.Tasks;

namespace ServiceHost.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly IProductQuery _productQuery;
        private readonly ICategoryQuery _categoryQuery;

        public CategoriesController(ICategoryQuery categoryQuery, IProductQuery productQuery)
        {
            _productQuery = productQuery;
            _categoryQuery = categoryQuery;
        }

        public async Task<IActionResult> Index() => View(await _categoryQuery.GetAll());

        [Route("Incrideble-Offers")]
        public async Task<IActionResult> IncridebleOffers(int pageIndex = 1)
        {
            var products= await _productQuery.GetAll("Discount");

            var model = PagingList.Create(products, 6, pageIndex);

            return View(model);
        }

        [Route("Best-Offers")]
        public async Task<IActionResult> BestOffers(int pageIndex = 1)
        {
            var products = await _productQuery.GetAll("BestSells");

            var model = PagingList.Create(products, 6, pageIndex);

            return View(model);
        }

        [Route("Products")]
        public async Task<IActionResult> Products(int pageIndex = 1)
        {
            var products = await _productQuery.GetAll("Routine");

            var model = PagingList.Create(products, 6, pageIndex);

            return View(model);
        }

    }
}