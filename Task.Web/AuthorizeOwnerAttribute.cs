using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Task.Infrastructure.Logging;

namespace Task.Web
{
    /// <summary>
    /// Authorize attribute which uses RoleProvider.IsUserInRole function for checking roles for current user
    /// </summary>
    public class AuthorizeOwnerAttribute : AuthorizeAttribute
    {
        private bool _failedRoleValidation;

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (httpContext == null)
            {
                Logger.Log(new ArgumentNullException("httpContext"));
                return false;
            }
            if (!httpContext.User.Identity.IsAuthenticated)
                return false;
            var roles = Roles.Split(',');
            if (!roles.Any())
                return false;
            if (roles.Any(role => IsUserInRoleMethod.IsUserInRole(httpContext.User.Identity.Name, role)))
            {
                return true;
            }
            _failedRoleValidation = true;
            return false;
        }

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            base.OnAuthorization(filterContext);
            if (filterContext.Result is HttpUnauthorizedResult || _failedRoleValidation)
            {
                filterContext.Result = new RedirectToRouteResult(
                  new RouteValueDictionary {
                    { "controller", "Account" },
                    { "action", "LogOn" },
                    { "ReturnUrl", filterContext.HttpContext.Request.RawUrl }
                });
            }
        }
    }
}