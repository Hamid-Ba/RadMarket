﻿using Framework.Application.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StoreManagement.Application.Contract.OrderAgg;
using StoreManagement.Application.Contract.ProductAgg;
using System.Threading.Tasks;

namespace ServiceHost.Controllers
{
    [Authorize]
    public class OrderController : BaseController
    {
        private readonly IOrderApplication _orderApplication;
        private readonly IProductApplication _productApplication;

        public OrderController(IOrderApplication orderApplication, IProductApplication productApplication)
        {
            _orderApplication = orderApplication;
            _productApplication = productApplication;
        }

        public IActionResult Index()
        {
            return View();
        }

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