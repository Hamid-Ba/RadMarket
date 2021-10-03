using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RadMarket.Query.Contracts.BannerAgg;
using RadMarket.Query.Contracts.ProductAgg;

namespace ServiceHost.ViewComponents
{
    public class SiteHeaderViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke() => View();
    }

    public class SiteFooterViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke() =>  View();
    }

    public class ResponsiveMenuViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke() => View();
    }

    public class OfferProductsViewComponent : ViewComponent
    {
        private readonly IProductQuery _productQuery;

        public OfferProductsViewComponent(IProductQuery productQuery) => _productQuery = productQuery;

        public async Task<IViewComponentResult> InvokeAsync(string filter)
        {
            switch (filter)
            {
                case "Discount":
                    return View(await _productQuery.GetAll(filter, 5));
                case "BestSells":
                    return View(await _productQuery.GetAll(filter, 7));
                case "Adt":
                    return View(await _productQuery.GetAll(filter, 7));
                default:
                    return View(await _productQuery.GetAll(filter, 3));
            }
        } 
    }

    public class RoutineProductsViewComponent : ViewComponent
    {
        private readonly IProductQuery _productQuery;

        public RoutineProductsViewComponent(IProductQuery productQuery) => _productQuery = productQuery;

        public async Task<IViewComponentResult> InvokeAsync(string filter)
        {
            switch (filter)
            {
                case "BestSells":
                    return View(await _productQuery.GetAll(filter, 3));
                default:
                    return View(await _productQuery.GetAll(filter, 3));
            }
        }
    }

    public class AdvertisingBannersViewComponent : ViewComponent
    {
        private readonly IBannerQuery _bannerQuery;

        public AdvertisingBannersViewComponent(IBannerQuery bannerQuery) => _bannerQuery = bannerQuery;

        public async Task<IViewComponentResult> InvokeAsync() => View(await _bannerQuery.GetForAdvertising());
    }
}