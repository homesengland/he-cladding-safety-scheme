using HE.Remediation.Core.Interface;
using Microsoft.AspNetCore.Mvc.Filters;

namespace HE.Remediation.WebApp.Attributes.Routing;

public class RecordRouteAttribute : ActionFilterAttribute
{
    public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        if (context.HttpContext.Request.Method != "GET" ||
            !context.RouteData.Values.ContainsKey("action") ||
            context.RouteData.Values["action"] is "Start")
        {
            await base.OnActionExecutionAsync(context, next);
            return;
        }

        var services = context.HttpContext.RequestServices;

        var connection = services.GetRequiredService<IDbConnectionWrapper>();
        var applicationDataProvider = services.GetRequiredService<IApplicationDataProvider>();
        await connection.ExecuteAsync("UpdateAreaProgress", new
        {
            ApplicationId = applicationDataProvider.GetApplicationId(),
            Area = context.RouteData.Values["area"],
            Controller = context.RouteData.Values["controller"],
            Action = context.RouteData.Values["action"]
        });

        await base.OnActionExecutionAsync(context, next);
    }
}