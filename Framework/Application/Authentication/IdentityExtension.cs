using System;
using System.Linq;
using System.Security.Claims;

namespace Framework.Application.Authentication
{
    public static class IdentityExtension
    {
        public static long GetUserId(this ClaimsPrincipal user)
        {
            var data = user?.Claims.SingleOrDefault(s => s.Type == ClaimTypes.NameIdentifier);
            return data is null ? default(long) : Convert.ToInt64(data.Value);
        }
    }
}