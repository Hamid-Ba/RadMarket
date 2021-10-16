using AccountManagement.Application.Contract.AdminUserAgg;
using Framework.Application.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ServiceHost.Tools
{
    public class AdminPermissionCheckerAttribute : AuthorizeAttribute, IAuthorizationFilter
    {
        private IAdminUserApplication _userApplication;
        private int permissionId;

        public AdminPermissionCheckerAttribute(int permissionId) => this.permissionId = permissionId;

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            _userApplication = (IAdminUserApplication)context.HttpContext.RequestServices.GetService(typeof(IAdminUserApplication));

            if (context.HttpContext.User.Identity.IsAuthenticated)
            {
                long userId = context.HttpContext.User.GetUserId();

                if (!_userApplication.IsUserHasPermissions(permissionId, userId)) { context.Result = new RedirectResult("/NotFound"); }
            }

            else context.Result = new RedirectResult("/NotFound");
        }
    }
}
