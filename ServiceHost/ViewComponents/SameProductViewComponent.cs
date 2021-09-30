using Microsoft.AspNetCore.Mvc;
using RadMarket.Query.Contracts.ProductAgg;
using System.Threading.Tasks;

namespace ServiceHost.ViewComponents
{
    public class SameProductViewComponent :ViewComponent
    {
        private readonly IProductQuery _productQuery;

        public SameProductViewComponent(IProductQuery productQuery) => _productQuery = productQuery;

        public async Task<IViewComponentResult> InvokeAsync(long categoryId) => View(await _productQuery.GetBy(categoryId));
    }
}
