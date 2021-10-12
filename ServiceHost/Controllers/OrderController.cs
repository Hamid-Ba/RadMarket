﻿using Framework.Application.Authentication;
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

            var command = new CreateOrderVM
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