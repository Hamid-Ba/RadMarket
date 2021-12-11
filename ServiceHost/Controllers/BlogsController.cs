using Microsoft.AspNetCore.Mvc;
using RadMarket.Query.Contracts.ArticleAgg;
using ReflectionIT.Mvc.Paging;
using System.Threading.Tasks;

namespace ServiceHost.Controllers
{
    public class BlogsController : BaseController
    {
        private readonly IArticleQuery _articleQuery;

        public BlogsController(IArticleQuery articleQuery) => _articleQuery = articleQuery;

        public async Task<IActionResult> Index(int pageIndex = 1)
        {
            var articles = await _articleQuery.GetAll();

            var model = PagingList.Create(articles, 6, pageIndex);

            ViewBag.Rows = (6 * pageIndex) - 5;

            return View(model);
        }

        [Route("Blogs/{slug}")]
        public async Task<IActionResult> Blog(string slug) => View(await _articleQuery.GetBy(slug));
        
    }
}