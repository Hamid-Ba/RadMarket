using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Framework.Application;
using StoreManagement.Application.Contract.ProductAgg;

namespace ServiceHost.Areas.Admin.Controllers
{
    public class ProductController : AdminBaseController
    {
        private readonly IProductApplication _productApplication;

        public ProductController(IProductApplication productApplication) => _productApplication = productApplication;

        public async Task<IActionResult> Index(SearchStoreVM search) => View(await _productApplication.GetAll(search));

        [HttpGet]
        public IActionResult Confirm(long id) => PartialView("Confirm", new ChangeStatusProductVM(){Id = id});

        [HttpPost,ValidateAntiForgeryToken]
        public async Task<IActionResult> Confirm(ChangeStatusProductVM command)
        {
            command.State = ProductAcceptanceState.Accepted;
            command.Description = "تایید شده";
            var result = await _productApplication.ChangeStatus(command);
            if (result.IsSucceeded) TempData[SuccessMessage] = result.Message;
            return new JsonResult(result);
        }

        [HttpGet]
        public IActionResult DissConfirm(long id) => PartialView("DissConfirm", new ChangeStatusProductVM(){Id = id});

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> DissConfirm(ChangeStatusProductVM command)
        {
            command.State = ProductAcceptanceState.Rejected;
            var result = await _productApplication.ChangeStatus(command);
            if (result.IsSucceeded) TempData[SuccessMessage] = result.Message;
            return new JsonResult(result);
        }

        [HttpGet]
        public async Task<IActionResult> Detail(long id) => PartialView(await _productApplication.GetDetailBy(id));
    }
}