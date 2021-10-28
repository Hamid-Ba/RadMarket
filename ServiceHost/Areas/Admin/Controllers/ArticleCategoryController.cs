using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using BlogManagement.Application.Contract.ArticleCategoryAgg;
using ServiceHost.Tools;
using BlogManagement.Infrastructure.Configuration;

namespace ServiceHost.Areas.Admin.Controllers
{
    [AdminPermissionChecker(BlogPermissionHelper.BlogCategory)]
    public class ArticleCategoryController : AdminBaseController
    {
        private readonly IArticleCategoryApplication _articleCategoryApplication;

        public ArticleCategoryController(IArticleCategoryApplication articleCategoryApplication) => _articleCategoryApplication = articleCategoryApplication;

        public async Task<IActionResult> Index() => View(await _articleCategoryApplication.GetAll());

        [HttpGet]
        public IActionResult Create() => PartialView();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateArticleCategoryVM command)
        {
            var result = await _articleCategoryApplication.Create(command);

            if (result.IsSucceeded) TempData[SuccessMessage] = result.Message;

            return new JsonResult(result);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(long id) => PartialView(await _articleCategoryApplication.GetDetailForEditBy(id));

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditArticleCategoryVM command)
        {
            var result = await _articleCategoryApplication.Edit(command);

            if (result.IsSucceeded) TempData[SuccessMessage] = result.Message;

            return new JsonResult(result);
        }

        [HttpGet]
        public IActionResult Delete(long id) => PartialView(id);

        [ActionName("Delete")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PostDelete(long id)
        {
            var result = await _articleCategoryApplication.Delete(id);

            if (result.IsSucceeded) TempData[SuccessMessage] = result.Message;

            return new JsonResult(result);
        }
    }
}