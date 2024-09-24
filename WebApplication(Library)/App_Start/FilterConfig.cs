using System.Web;
using System.Web.Mvc;
using WebApplication_Library_.Filters;

namespace WebApplication_Library_
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new GlobalExceptionFilter());
            filters.Add(new LoggingActionFilter());
        }
    }
}
