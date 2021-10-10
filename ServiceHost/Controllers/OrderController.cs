using Microsoft.AspNetCore.Mvc;
using StoreManagement.Application.Contract.OrderAgg;
using System.Threading.Tasks;

namespace ServiceHost.Controllers
{
    public class OrderController : BaseController
    {
        private readonly IOrderApplication _orderApplication;

        public OrderController(IOrderApplication orderApplication)
        {
            _orderApplication = orderApplication;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> AddProductToOpenOrder(AddOrderItemsVM command)
        {
            var result = await _orderApplication.AddProductToOpenOrder(command);

            if (result.IsSucceeded)
            {
                TempData[SuccessMessage] = result.Message;
                return View();
            }

            TempData[ErrorMessage] = result.Message;
            return View();
        }
    }
}
