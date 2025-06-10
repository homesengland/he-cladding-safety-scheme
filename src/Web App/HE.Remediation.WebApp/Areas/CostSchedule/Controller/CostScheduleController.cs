using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.CostSchedule;
using HE.Remediation.WebApp.ViewModels.CostSchedule;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HE.Remediation.WebApp.Areas.CostSchedule.Controller;

[Area("CostSchedule")]
[Route("CostSchedule")]
public class CostScheduleController : StartController
{
    private readonly ISender _sender;
    private readonly IMapper _mapper;

    public CostScheduleController(ISender sender, IMapper mapper) : base(sender)
    {
        _sender = sender;
        _mapper = mapper;
    }

    protected override IActionResult DefaultStart => RedirectToAction("CostSchedule", "CostSchedule", new { Area = "CostSchedule" });

    [HttpGet(nameof(CostSchedule))]
    public async Task<IActionResult> CostSchedule(CancellationToken cancellationToken)
    {
        var response = await _sender.Send(GetCostScheduleRequest.Request, cancellationToken);
        var model = _mapper.Map<CostScheduleViewModel>(response);
        return View(model);
    }
}
