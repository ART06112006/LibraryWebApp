using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication_Library_.Infrastructure;

namespace WebApplication_Library_.Filters
{
    public class LoggingActionFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            filterContext.HttpContext.Items["ActionStartTime"] = DateTime.Now;
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            var controllerName = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;
            var actionName = filterContext.ActionDescriptor.ActionName;
            var ipAddress = filterContext.HttpContext.Request.UserHostAddress;
            var requestParams = filterContext.HttpContext.Request.Params;

            var startTime = (DateTime)filterContext.HttpContext.Items["ActionStartTime"];
            var endTime = DateTime.Now;
            var duration = endTime - startTime;

            Log.Information($"{new { ControllerName = controllerName, ActionName = actionName, IpAddress = ipAddress, StartTime = startTime, EndTime = endTime, Duration = duration, RequestParams = requestParams }}\n\n");
        }
    }
}