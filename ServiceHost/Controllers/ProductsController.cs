using Microsoft.AspNetCore.Mvc;
using RadMarket.Query.Contracts.ProductAgg;
using ReflectionIT.Mvc.Paging;
using System.Threading.Tasks;

namespace ServiceHost.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductQuery _productQuery;

        public ProductsController(IProductQuery productQuery)
        {
            _productQuery = productQuery;
        }

        [HttpGet("Incrideble-Offers")]
        public async Task<IActionResult> IncridebleOffers(int pageIndex = 1)
        {
            var products = await _productQuery.GetAll("Discount");

            var model = PagingList.Create(products, 6, pageIndex);
            model.Action = "IncridebleOffers";

            return View(model);
        }

        [HttpGet("Best-Offers")]
        public async Task<IActionResult> BestOffers(int pageIndex = 1)
        {
            var products = await _productQuery.GetAll("BestSells");

            var model = PagingList.Create(products, 6, pageIndex);
            model.Action = "BestOffers";

            return View(model);
        }


        [HttpGet("Products")]
        public async Task<IActionResult> Products(int pageIndex = 1)
        {
            var products = await _productQuery.GetAll("Routine");

            var model = PagingList.Create(products, 6, pageIndex);
            model.Action = "Products";

            return View(model);
        }
    }
}