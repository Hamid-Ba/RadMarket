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

    public class OfferProductsViewComponent : ViewComponent
    {
        private readonly IProductQuery _productQuery;

        public OfferProductsViewComponent(IProductQuery productQuery) => _productQuery = productQuery;

        public async Task<IViewComponentResult> InvokeAsync(bool discounts)
        {
            if(discounts) return View(await _productQuery.GetAllWhichHasDiscount(5));
            return View(await _productQuery.GetAllBestSells(7));
        } 
    }

}