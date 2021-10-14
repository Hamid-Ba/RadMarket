using AccountManagement.Application.Contract.UserAgg;
using Microsoft.AspNetCore.Mvc;
using StoreManagement.Application.Contract.OrderAgg;
using System.Threading.Tasks;

namespace ServiceHost.Areas.Admin.Controllers
{
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

        [HttpGet]
        public async Task<IActionResult> AddressInfo(long userId) => PartialView(await _userApplication.GetAddressInfoBy(userId));

        [HttpGet]
        public async Task<IActionResult> Items(long id) => PartialView(await _orderApplication.GetItems(id));
    }
}
