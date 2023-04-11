using HE.Remediation.Core.UseCase.Areas.AreaProgress;
using HE.Remediation.WebApp.Attributes.Authorisation;
using HE.Remediation.WebApp.Attributes.Routing;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HE.Remediation.WebApp.Areas;

[CookieApplicationAuthorise]
[AreaRedirect]
[RecordRoute]
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
        var area = this.RouteData.Values["area"] as string;
        var response = await _sender.Send(new GetAreaProgressRequest
        {
            Area = area
        });

        return response is not null
            ? RedirectToAction(response.Action, response.Controller, new { response.Area })
            : DefaultStart;
    }
}