using System.Web.Mvc;

namespace WebApplication_Library_.Areas.User
{
    public class UserAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "User";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "User_default",
                "User/{controller}/{action}",
                new { controller = "Shop", action = "Index" },
                namespaces: new[] { "WebApplication_Library_.Areas.User.Controllers" }
            );
        }
    }
}