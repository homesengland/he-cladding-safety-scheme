using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace HE.Remediation.WebApp.Attributes.Routing;

public class AreaRedirectAttribute : ActionFilterAttribute
{
    public AreaRedirectAttribute()
    {
        Order = 2;
    }
    
    private const string Redirect = nameof(Redirect);
    
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        var headers = context.HttpContext.Request.Headers;
        var ctrl = context.Controller as Controller;

        if (headers.Referer.Any(x => x.Contains(context.HttpContext.Request.Host.Value))
            || ctrl is null 
            || (ctrl.TempData.TryGetValue(Redirect, out var value) && (value as bool?) == true))
        {
            base.OnActionExecuting(context);
            return;
        }

        
        var area = context.RouteData.Values["area"] as string;
        var controller = context.RouteData.Values["controller"] as string;
        ctrl.TempData[Redirect] = true;

        context.Result = new RedirectToActionResult("Start", controller, new { Area = area, headerRedirect = true });
    }
}