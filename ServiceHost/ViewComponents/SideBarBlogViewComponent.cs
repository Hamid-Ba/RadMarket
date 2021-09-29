using Microsoft.AspNetCore.Mvc;
using RadMarket.Query.Contracts.ArticleAgg;
using System.Threading.Tasks;

namespace ServiceHost.ViewComponents
{
    public class SideBarBlogViewComponent : ViewComponent
    {
        private readonly IArticleQuery _articleQuery;

        public SideBarBlogViewComponent(IArticleQuery articleQuery) => _articleQuery = articleQuery;

        public async Task<IViewComponentResult> InvokeAsync() => View(await _articleQuery.GetForSideBar());
    }
}
