using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace ServiceHost.Areas.User.Controllers
{
    [Area("User")]
    [Authorize(Roles = "ClientUser")]
    public class UserBaseController : Controller
    {
        protected string ErrorMessage = "ErrorMessage";
        protected string SuccessMessage = "SuccessMessage";
        protected string InfoMessage = "InfoMessage";
        protected string WarningMessage = "WarningMessage";
    }
}
