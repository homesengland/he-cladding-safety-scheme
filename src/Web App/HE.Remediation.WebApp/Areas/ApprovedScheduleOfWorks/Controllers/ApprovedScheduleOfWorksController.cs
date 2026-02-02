using AutoMapper;
using Mediator;
using Microsoft.AspNetCore.Mvc;
using HE.Remediation.Core.UseCase.Areas.ApprovedScheduleOfWorks.BaseInformation.Get;
using HE.Remediation.Core.UseCase.Areas.ApprovedScheduleOfWorks.CostProfile.Get;
using HE.Remediation.WebApp.ViewModels.ApprovedScheduleOfWorks;

namespace HE.Remediation.WebApp.Areas.ApprovedScheduleOfWorks.Controllers;

[Area("ApprovedScheduleOfWorks")]
[Route("ApprovedScheduleOfWorks")]
public class ApprovedScheduleOfWorksController : StartController
{
    private readonly ISender _sender;
    private readonly IMapper _mapper;

    public ApprovedScheduleOfWorksController(ISender sender, IMapper mapper) : base(sender)
    {
        _sender = sender;
        _mapper = mapper;
    }

    protected override IActionResult DefaultStart => RedirectToAction("StartInformation", "ApprovedScheduleOfWorks", new { Area = "ApprovedScheduleOfWorks" });

    #region "About this section"

    [HttpGet(nameof(StartInformation))]
    public async Task<IActionResult> StartInformation()
    {
        var response = await _sender.Send(GetBaseInformationRequest.Request);
        var viewModel = _mapper.Map<StartInformationViewModel>(response);

        viewModel.ReturnUrl = string.Empty;
        return View(viewModel);
    }

    [HttpPost(nameof(StartInformation))]
    public IActionResult StartInformation(StartInformationViewModel viewModel)
    {
        return RedirectToAction("CostProfile", "ApprovedScheduleOfWorks", new { Area = "ApprovedScheduleOfWorks" });
    }

    #endregion

    #region "Confirmed schedule of works"

    [HttpGet(nameof(CostProfile))]
    public async Task<IActionResult> CostProfile()
    {
        var response = await _sender.Send(GetCostProfileRequest.Request);
        var viewModel = _mapper.Map<CostProfileViewModel>(response);

        viewModel.ReturnUrl = string.Empty;
        return View(viewModel);
    }

    #endregion
}
