using System.Text.Json;
using HE.Remediation.Core.Attributes;
using HE.Remediation.Core.UseCase.Areas.AreaProgress;
using HE.Remediation.WebApp.Attributes.Authorisation;
using HE.Remediation.WebApp.Attributes.Routing;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HE.Remediation.WebApp.Areas;

[CookieApplicationAuthorise]
[AreaRedirect]
[RecordRoute]
[UserIdentityMustBeTheApplicationUser]
public abstract class StartController : Controller
{
    private readonly ISender _sender;

    protected StartController(ISender sender)
    {
        _sender = sender;
    }

    protected abstract IActionResult DefaultStart { get; }

    public async Task<IActionResult> Start()
    {
        var response = await GetArea();

        return response is not null
            ? BuildRedirectAction(response)
            : DefaultStart;
    }

    protected async Task<GetAreaProgressResponse> GetArea()
    {
        var area = this.RouteData.Values["area"] as string;
        var response = await _sender.Send(new GetAreaProgressRequest
        {
            Area = area
        });

        return response;
    }

    protected IActionResult BuildRedirectAction(GetAreaProgressResponse area)
    {
        var routeDictionary = !string.IsNullOrEmpty(area.RouteDataJson)
            ? JsonSerializer.Deserialize<Dictionary<string, object>>(area.RouteDataJson)
            : new Dictionary<string, object>();

        routeDictionary!.Add("Area", area.Area);

        return RedirectToAction(area.Action, area.Controller, routeDictionary);
    }
}