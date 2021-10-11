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

        public async Task<IActionResult>  Index() => View(await _orderQuery.GetBy(User.GetUserId()));

        [HttpPost]
        public async Task<IActionResult> AddProductToOpenOrder(long productId,int count)
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
    }
}