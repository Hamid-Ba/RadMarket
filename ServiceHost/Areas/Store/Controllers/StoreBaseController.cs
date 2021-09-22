using Microsoft.AspNetCore.Mvc;

namespace ServiceHost.Areas.Store.Controllers
{
    [Area("Store")]
    public class StoreBaseController : Controller
    {
        protected string ErrorMessage = "ErrorMessage";
        protected string SuccessMessage = "SuccessMessage";
        protected string InfoMessage = "InfoMessage";
        protected string WarningMessage = "WarningMessage";
    }
}