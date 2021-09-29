using AccountManagement.Application.Contract.UserAgg;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ServiceHost.Areas.Admin.Controllers
{
    public class UserController : AdminBaseController
    {
        private readonly IUserApplication _userApplication;

        public UserController(IUserApplication userApplication) => _userApplication = userApplication;

        public async Task<IActionResult> Index() => View(await _userApplication.GetAll());

        [HttpGet]
        public IActionResult Delete(long id) => PartialView(id);

        [ActionName("Delete")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PostDelete(long id)
        {
            var result = await _userApplication.Delete(id);

            if (result.IsSucceeded) TempData[SuccessMessage] = result.Message;

            return new JsonResult(result);
        }

        [HttpGet]
        public async Task<IActionResult> AddressInfo(long id) => PartialView(await _userApplication.GetAddressInfoBy(id));
    }
}
