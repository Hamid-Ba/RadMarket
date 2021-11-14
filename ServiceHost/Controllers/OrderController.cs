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

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var result = await _orderQuery.GetBy(User.GetUserId());

            if (result is null || result.Items.Count == 0)
            {
                TempData[WarningMessage] = "سبد خرید شما خالیست";
                return Redirect("/");
            }
            return View(result);
        }

        [HttpPost]
        [ActionName("Index")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PostIndex()
        {
            var basket = await _orderQuery.GetBy(User.GetUserId());

            //CheckCount
            foreach (var item in basket.Items)
            {
                var checkCount = await _productApplication.CheckCountOfProduct(item.ProductId, item.Count);
                if (!checkCount.IsSucceeded)
                {
                    TempData[ErrorMessage] = checkCount.Message;
                    return RedirectToAction("Index");
                }
            }

            //Place Item
            foreach (var item in basket.Items)
            {
                var commandItem = new PlaceItemVM
                {
                    Id = item.Id,
                    UserId = User.GetUserId(),
                    OrderId = item.OrderId,
                    ProductId = item.ProductId,
                    DiscountPrice = item.DiscountPrice,
                    PayAmount = item.PayAmount,
                    Count = item.Count,
                };

                var resultItem = await _orderApplication.PlaceItem(commandItem);

                if (!resultItem.IsSucceeded)
                {
                    TempData[ErrorMessage] = resultItem.Message;
                    return RedirectToAction("Index");
                }
            }

            //Place Order
            var command = new PlaceOrderVM
            {
                UserId = User.GetUserId(),
                TotalPrice = basket.TotalPrice,
                DiscountPrice = basket.DiscountPrice,
                PayAmount = basket.PayAmount,
                MobileNumber = User.GetMobilePhone(),
                PaymentMethod = Framework.Domain.PaymentMethodType.PayOffline
            };

            var result = await _orderApplication.PlaceOrder(command);

            if (result.IsSucceeded)
            {
                TempData[SuccessMessage] = result.Message;
                return Redirect("/");
            }

            TempData[ErrorMessage] = result.Message;
            return RedirectToAction("Index");
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

            var storeId = await _productApplication.GetProductStoreIdBy(command.ProductId);
            var slug = await _productApplication.GetProductSlugBy(command.ProductId);

            if (result.IsSucceeded) TempData[SuccessMessage] = result.Message;
            else TempData[ErrorMessage] = result.Message;

            return RedirectToAction("Index", "Product", new { storeId = storeId, slug = slug });
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