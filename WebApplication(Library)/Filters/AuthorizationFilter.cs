using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication_Library_.Models;

namespace WebApplication_Library_.Filters
{
    public class AuthorizationFilter : AuthorizeAttribute
    {
        private Role _role;

        public AuthorizationFilter(Role role)
        {
            _role = role;
        }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            var user = httpContext.Session["CurrentUser"] as User;

            if (user == null) return false;

            if (user.Role != _role) return false;

            return true;
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            filterContext.Result = new ViewResult()
            {
                ViewName = "~/Views/Shared/Error.cshtml",
                ViewData = new ViewDataDictionary(new Exception("User is unauthorized!"))
            };
        }
    }
}