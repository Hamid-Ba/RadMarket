using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RadMarket.Query.Contracts.ProductAgg;

namespace ServiceHost.ViewComponents
{
    public class SiteHeaderViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }

    public class SiteFooterViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }

    public class ResponsiveMenuViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }

    public class ProductWithDiscountViewComponent : ViewComponent
    {
        private readonly IProductQuery _productQuery;

        public ProductWithDiscountViewComponent(IProductQuery productQuery) => _productQuery = productQuery;

        public async Task<IViewComponentResult> InvokeAsync() => View(await _productQuery.GetAllWhichHasDiscount());
    }

}