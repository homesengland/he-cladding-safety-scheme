using HE.Remediation.Core.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace HE.Remediation.WebApp.Areas.ScheduleOfWorks.Controllers;

[Area("ScheduleOfWorks")]
[Route("ScheduleOfWorks/api")]
[ApiController]
public class ScheduleOfWorksApiController : Controller
{
    [HttpPost("calculateCosts")]
    public IActionResult CalculateCosts([FromBody] MonthlyCostsCalculationRequest request)
    {
        return Json(CostsCalculationHelper.CalculateMonthlyCosts(request));
    }
}
