using HE.Remediation.Core.Helpers;
using HE.Remediation.WebApp.Attributes.Authorisation;
using Microsoft.AspNetCore.Mvc;

namespace HE.Remediation.WebApp.Areas.ScheduleOfWorks.Controllers;

[Area("ScheduleOfWorks")]
[Route("ScheduleOfWorks/api")]
[ApiController]
[CookieApplicationAuthorise]
public class ScheduleOfWorksApiController : Controller
{
    [HttpPost("calculateCosts")]
    public IActionResult CalculateCosts([FromBody] MonthlyCostsCalculationRequest request)
    {
        return Json(CostsCalculationHelper.CalculateMonthlyCosts(request));
    }
}
