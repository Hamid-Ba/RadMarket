using Microsoft.AspNetCore.Mvc;
using RadMarket.Query.Contracts.ProductAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceHost.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductQuery _productQuery;

        public ProductController(IProductQuery productQuery)
        {
            _productQuery = productQuery;
        }

        [HttpGet("{slug}")]
        public async Task<IActionResult>  Index(string slug) => View(await _productQuery.GetBy(slug));
        
    }
}
