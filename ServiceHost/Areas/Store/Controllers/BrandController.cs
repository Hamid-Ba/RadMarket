using Framework.Application.Authentication;
using Microsoft.AspNetCore.Mvc;
using ReflectionIT.Mvc.Paging;
using StoreManagement.Application.Contract.BrandAgg;
using System.Threading.Tasks;

namespace ServiceHost.Areas.Store.Controllers
{
    public class BrandController : StoreBaseController
    {
        private readonly IBrandApplication _brandApplication;

        public BrandController(IBrandApplication brandApplication) => _brandApplication = brandApplication;

        public async Task<IActionResult> Index(int pageIndex = 1)
        {
            var products = await _brandApplication.GetAll(User.GetStoreId());
            var model = PagingList.Create(products, 10, pageIndex);

            ViewBag.Rows = (10 * pageIndex) - 9;

            return View(model);
        }

        [HttpGet]
        public IActionResult Create() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateBrandVM command)
        {
            command.StoreId = User.GetStoreId();

            if (ModelState.IsValid)
            {
                var result = await _brandApplication.Create(command);

                if (result.IsSucceeded)
                {
                    TempData[SuccessMessage] = result.Message;
                    return RedirectToAction("Index");
                }

                TempData[ErrorMessage] = result.Message;
            }

            return View(command);
        }

        [HttpGet]
        public async Task<IActionResult>  Edit(long id) => View(await _brandApplication.GetDetailForEditBy(id,User.GetStoreId()));

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditBrandVM command)
        {
            command.StoreId = User.GetStoreId();

            if (ModelState.IsValid)
            {
                var result = await _brandApplication.Edit(command);

                if (result.IsSucceeded)
                {
                    TempData[SuccessMessage] = result.Message;
                    return RedirectToAction("Index");
                }

                TempData[ErrorMessage] = result.Message;
            }

            return View(command);
        }

        public async Task<IActionResult> Delete(long id)
        {
            if (ModelState.IsValid)
            {
                var result = await _brandApplication.Delete(id,User.GetStoreId());

                if (result.IsSucceeded) TempData[SuccessMessage] = result.Message;
                else TempData[ErrorMessage] = result.Message;
            }

            return RedirectToAction("Index");
        }
    }
}