using AccountManagement.Application.Contract.AdminUserAgg;
using Framework.Application.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace ServiceHost.Tools
{
    [HtmlTargetElement(Attributes = "Permission")]
    public class AdminPermissionTagHelper : TagHelper
    {
        public int Permission { get; set; }

        private readonly IAdminUserApplication _userApplication;
        private readonly IHttpContextAccessor _contextAccessor;

        public AdminPermissionTagHelper(IAdminUserApplication userApplication, IHttpContextAccessor httpContextAccessor)
        {
            _userApplication = userApplication;
            _contextAccessor = httpContextAccessor;
        }
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (!_contextAccessor.HttpContext.User.Identity.IsAuthenticated)
            {
                output.SuppressOutput();
                return;
            }

            if (!_userApplication.IsUserHasPermissions(Permission, _contextAccessor.HttpContext.User.GetUserId()))
            {
                output.SuppressOutput();
                return;
            }

            base.Process(context, output);
        }
    }
}
