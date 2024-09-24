using System.Web.Mvc;

namespace WebApplication_Library_.Areas.Admin
{
    public class AdminAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Admin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Admin_default",
                "Admin/{controller}/{action}",
                new { controller = "Home", action = "Index" },
                namespaces: new[] { "WebApplication_Library_.Areas.Admin.Controllers" }
            );
        }
    }
}