using AccountManagement.Application.Contract.UserAgg;
using Microsoft.AspNetCore.Mvc;
using ServiceHost.Tools;
using StoreManagement.Application.Contract.OrderAgg;
using StoreManagement.Infrastructure.Configuration;
using System.Threading.Tasks;

namespace ServiceHost.Areas.Admin.Controllers
{
    [AdminPermissionChecker(StorePermissionHelper.ProductsOrder)]
    public class OrderController : AdminBaseController
    {
        private readonly IUserApplication _userApplication;
        private readonly IOrderApplication _orderApplication;

        public OrderController(IUserApplication userApplication, IOrderApplication orderApplication)
        {
            _userApplication = userApplication;
            _orderApplication = orderApplication;
        }

        public async Task<IActionResult> Index() => View(await _orderApplication.GetAll());
        
        public async Task<IActionResult> PayedOrders() => View(await _orderApplication.GetAllPayedItems());

        public async Task<IActionResult> UnPayedOrders() => View(await _orderApplication.GetAllUnPayedItems());

        [HttpGet]
        public async Task<IActionResult> AddressInfo(long userId) => PartialView(await _userApplication.GetAddressInfoBy(userId));

        [HttpGet]
        public async Task<IActionResult> Items(long id) => PartialView(await _orderApplication.GetItems(id));
    }
}
