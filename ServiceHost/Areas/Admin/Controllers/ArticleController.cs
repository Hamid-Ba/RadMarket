using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using BlogManagement.Application.Contract.ArticleAgg;
using BlogManagement.Application.Contract.ArticleCategoryAgg;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ServiceHost.Areas.Admin.Controllers
{
    public class ArticleController : AdminBaseController
    {
        private readonly IArticleApplication _articleApplication;
        private readonly IArticleCategoryApplication _articleCategoryApplication;

        public ArticleController(IArticleApplication articleApplication, IArticleCategoryApplication articleCategoryApplication)
        {
            _articleApplication = articleApplication;
            _articleCategoryApplication = articleCategoryApplication;
        }

        public async Task<IActionResult> Index() => View(await _articleApplication.GetAll());

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.Categories = new SelectList(await _articleCategoryApplication.GetAll(), "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateArticleVM command)
        {
            ViewBag.Categories = new SelectList(await _articleCategoryApplication.GetAll(), "Id", "Name");
            if (ModelState.IsValid)
            {
                var result = await _articleApplication.Create(command);

                if (result.IsSucceeded)
                {
                    TempData[SuccessMessage] = result.Message;
                    return RedirectToAction("Index", "Article");
                }

                TempData[ErrorMessage] = result.Message;
            }

            return View(command);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(long id)
        {
            ViewBag.Categories = new SelectList(await _articleCategoryApplication.GetAll(), "Id", "Name");
            return View(await  _articleApplication.GetDetailForEditBy(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditArticleVM command)
        {
            ViewBag.Categories = new SelectList(await _articleCategoryApplication.GetAll(), "Id", "Name");
            if (ModelState.IsValid)
            {
                var result = await _articleApplication.Edit(command);

                if (result.IsSucceeded)
                {
                    TempData[SuccessMessage] = result.Message;
                    return RedirectToAction("Index", "Article");
                }

                TempData[ErrorMessage] = result.Message;
            }

            return View(command);
        }
    }
}
