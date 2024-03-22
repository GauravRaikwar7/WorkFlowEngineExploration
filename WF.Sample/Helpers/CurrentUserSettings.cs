using Microsoft.AspNetCore.Http;
using System;
using System.Linq;

namespace WF.Sample.Helpers
{
    public class CurrentUserSettings
    {
        public static Guid GetCurrentUser(HttpContext context)
        {
            Guid res = Guid.Empty;
            string resRole = string.Empty;
            if (context.Request.Query["CurrentEmployee"].FirstOrDefault() != null)
            {
                Guid.TryParse(context.Request.Query["CurrentEmployee"].FirstOrDefault(), out res);
                resRole =  context.Request.Query["UserRole"].FirstOrDefault();
                SetUserInCookies(context, res, resRole);
            }
            else if (context.Request.Cookies["CurrentEmployee"] != null)
            {
                Guid.TryParse(context.Request.Cookies["CurrentEmployee"], out res);
                resRole = context.Request.Query["UserRole"].FirstOrDefault();
            }
            return res;
        }
        public static string GetCurrentUserRole(HttpContext context)
        {
            Guid res = Guid.Empty;
            string resRole = string.Empty;
            if (context.Request.Query["CurrentEmployee"].FirstOrDefault() != null)
            {
                Guid.TryParse(context.Request.Query["CurrentEmployee"].FirstOrDefault(), out res);
                resRole = context.Request.Query["UserRole"].FirstOrDefault();
                SetUserInCookies(context, res, resRole);
            }
            else if (context.Request.Cookies["UserRole"] != null)
            {
                Guid.TryParse(context.Request.Cookies["CurrentEmployee"], out res);
                resRole = context.Request.Query["UserRole"].FirstOrDefault();
            }
            return resRole;
        }

        public static void SetUserInCookies(HttpContext context, Guid userId, string UserRole)
        {
            context.Response.Cookies.Append("CurrentEmployee", userId.ToString());
            SetUserRoleInCookies(context, UserRole);
        }

        public static void SetUserRoleInCookies(HttpContext context, string userRole)
        {
            if(userRole == null) { return; }
            context.Response.Cookies.Append("UserRole", userRole);
        }
    }
}