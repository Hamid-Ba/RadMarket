using Microsoft.AspNetCore.Mvc;
using RadMarket.Query.Contracts.ProductAgg;
using RadMarket.Query.Contracts.StoreAgg;
using ReflectionIT.Mvc.Paging;
using System.Threading.Tasks;

namespace ServiceHost.Controllers
{
    public class StoreController : Controller
    {
        private readonly IStoreQuery _storeQuery;
        private readonly IProductQuery _productQuery;

        public StoreController(IStoreQuery storeQuery, IProductQuery productQuery)
        {
            _storeQuery = storeQuery;
            _productQuery = productQuery;
        }

        [HttpGet("store{id}/{name}")]
        public async Task<IActionResult> Index(long id, string name) => View(await _storeQuery.GetBy(id));

        [HttpGet("store{id}/{name}/products")]
        public async Task<IActionResult> Products(long id,string name,int pageIndex = 1)
        {
            var products = await _productQuery.GetAllBy(id,6);

            var model = PagingList.Create(products, 10, pageIndex);
            model.Action = "Products";

            ViewBag.StoreName = name;
            return View(model);
        }
    }
}
