using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;

namespace Framework.Application.Authentication
{
    public class AuthHelper : IAuthHelper
    {
        private readonly IHttpContextAccessor _contextAccessor;

        public AuthHelper(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        public async void Signin(AuthViewModel account)
        {
            //var permissions = JsonConvert.SerializeObject(account.Permissions);
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, account.Id.ToString()),
                new Claim(ClaimTypes.Name, account.Fullname),
                new Claim(ClaimTypes.MobilePhone, account.Mobile)
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var authProperties = new Microsoft.AspNetCore.Authentication.AuthenticationProperties
            {
                IsPersistent = account.KeepMe
            };

            await _contextAccessor.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity), authProperties);

        }

        public long GetUserId() =>  _contextAccessor.HttpContext.User.GetUserId();

        public async void SignOut() => await _contextAccessor.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
    }
}