using Microsoft.AspNetCore.Mvc;
using RadMarket.Query.Contracts.ProductAgg;
using System.Threading.Tasks;

namespace ServiceHost.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductQuery _productQuery;

        public ProductController(IProductQuery productQuery) => _productQuery = productQuery;

        [HttpGet("Product/{storeId}/{slug}")]
        public async Task<IActionResult>  Index(long storeId, string slug) => View(await _productQuery.GetBy(storeId, slug));
        
    }
}
