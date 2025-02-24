using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace HE.Remediation.WebApp.Attributes.Routing;

public class ExcludeRouteRecordingAttribute : ActionFilterAttribute
{
    public ExcludeRouteRecordingAttribute()
    {
        Order = 1;
    }

    public override Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        if (context.Controller is not Controller controller)
        {
            return base.OnActionExecutionAsync(context, next);
        }

        controller.TempData["ExcludeRoute"] = true;

        return base.OnActionExecutionAsync(context, next);
    }
}