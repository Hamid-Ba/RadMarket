using Framework.Application.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RadMarket.Query.Contracts.OrderAgg;
using StoreManagement.Application.Contract.OrderAgg;
using StoreManagement.Application.Contract.ProductAgg;
using System.Threading.Tasks;

namespace ServiceHost.Controllers
{
    [Authorize]
    public class OrderController : BaseController
    {
        private readonly IOrderQuery _orderQuery;
        private readonly IOrderApplication _orderApplication;
        private readonly IProductApplication _productApplication;

        public OrderController(IOrderQuery orderQuery, IOrderApplication orderApplication, IProductApplication productApplication)
        {
            _orderQuery = orderQuery;
            _orderApplication = orderApplication;
            _productApplication = productApplication;
        }

        public async Task<IActionResult> Index()
        {
            var result = await _orderQuery.GetBy(User.GetUserId());

            if (result.Items.Count == 0)
            {
                TempData[ErrorMessage] = "سبد خرید شما خالیست";
                return Redirect("/");
            }
            return View(result);
        }
        [HttpPost]
        public async Task<IActionResult> AddProductToOpenOrder(long productId, int count)
        {
            var command = new AddOrderItemsVM
            {
                UserId = User.GetUserId(),
                ProductId = productId,
                Count = count
            };

            var result = await _orderApplication.AddProductToOpenOrder(command);

            if (result.IsSucceeded)
            {
                TempData[SuccessMessage] = result.Message;
                var storeId = await _productApplication.GetProductStoreIdBy(command.ProductId);
                var slug = await _productApplication.GetProductSlugBy(command.ProductId);
                return RedirectToAction("Index", "Product", new { storeId = storeId, slug = slug });
            }

            TempData[ErrorMessage] = result.Message;
            return Redirect("/");
        }

        public async Task<IActionResult> DeleteItem(long itemId)
        {
            var result = await _orderApplication.DeleteItemBy(User.GetUserId(), itemId);

            if (result.IsSucceeded) TempData[SuccessMessage] = result.Message;
            else TempData[ErrorMessage] = result.Message;

            return RedirectToAction("Index");
        }
    }
}