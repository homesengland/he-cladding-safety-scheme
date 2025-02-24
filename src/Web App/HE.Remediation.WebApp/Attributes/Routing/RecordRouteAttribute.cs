using System.Text.Json;
using HE.Remediation.Core.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace HE.Remediation.WebApp.Attributes.Routing;

public class RecordRouteAttribute : ActionFilterAttribute
{
    public RecordRouteAttribute()
    {
        Order = 3;
    }

    private static readonly string[] _ExcludedRouteValues = { "action", "area", "controller" };

    public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var tempData = GetTempData(context);

        if (context.HttpContext.Request.Method != "GET" ||
            !context.RouteData.Values.ContainsKey("action") ||
            context.RouteData.Values["action"] is "Start" ||
            (tempData is not null && tempData.TryGetValue("ExcludeRoute", out var excludeRoute) && (excludeRoute as bool?) == true)
        )
        {
            await base.OnActionExecutionAsync(context, next);
            return;
        }

        var services = context.HttpContext.RequestServices;

        var connection = services.GetRequiredService<IDbConnectionWrapper>();
        var applicationDataProvider = services.GetRequiredService<IApplicationDataProvider>();

        var routeData = context.RouteData.Values.Where(x => _ExcludedRouteValues.All(r => r != x.Key)).ToDictionary(x => x.Key, x => x.Value);
        foreach (var query in context.HttpContext.Request.Query)
        {
            routeData.Add(query.Key, query.Value.ToString());
        }
        string routeDataJson = null;
        if (routeData.Any())
        {
            routeDataJson = JsonSerializer.Serialize(routeData);
        }

        var applicationId = applicationDataProvider.GetApplicationId();

        await connection.ExecuteAsync("UpdateAreaProgress", new
        {
            ApplicationId = applicationId,
            Area = context.RouteData.Values["area"],
            Controller = context.RouteData.Values["controller"],
            Action = context.RouteData.Values["action"],
            RouteDataJson = routeDataJson
        });

        await RecordAreaData(context, tempData, connection, applicationId);
        
        await base.OnActionExecutionAsync(context, next);
    }

    private static ITempDataDictionary GetTempData(ActionExecutingContext context)
    {
        return context.Controller is Controller controller 
            ? controller.TempData
            : null;
    }

    private static async Task RecordAreaData(ActionExecutingContext context, ITempDataDictionary tempData, IDbConnectionWrapper connection, Guid applicationId)
    {
        if (tempData is null)
        {
            return;
        }
        
        if (!tempData.TryGetValue("AreaDataJson", out var value))
        {
            return;
        }

        if (value is not string areaDataJson)
        {
            return;
        }
        
        await connection.ExecuteAsync("UpdateAreaProgressAreaData", new
        {
            ApplicationId = applicationId,
            Area = context.RouteData.Values["area"],
            AreaDataJson = areaDataJson
        });
    }
}