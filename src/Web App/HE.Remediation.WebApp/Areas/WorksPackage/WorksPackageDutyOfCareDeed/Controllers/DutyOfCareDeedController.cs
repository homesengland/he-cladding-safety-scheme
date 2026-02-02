using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageDutyOfCareDeed.Progress.Get;
using HE.Remediation.WebApp.ViewModels.WorksPackage.WorkPackageDutyOfCareDeed;
using Mediator;
using Microsoft.AspNetCore.Mvc;

namespace HE.Remediation.WebApp.Areas.WorksPackage.WorksPackageDutyOfCareDeed.Controllers;

[Area("WorksPackageDutyOfCareDeed")]
[Route("WorksPackages/DutyOfCareDeed")]
public class DutyOfCareDeedController : StartController
{
    private readonly ISender _sender;
    private readonly IMapper _mapper;

    public DutyOfCareDeedController(ISender sender, IMapper mapper) : base(sender)
    {
        _sender = sender;
        _mapper = mapper;
    }

    protected override IActionResult DefaultStart =>
        RedirectToAction("Progress", "DutyOfCareDeed", new { Area = "WorksPackageDutyOfCareDeed" });

    #region Duty of Care Deed Progress

    [HttpGet(nameof(Progress))]
    public async Task<IActionResult> Progress()
    {
        var response = await _sender.Send(GetProgressRequest.Request);
        var viewModel = _mapper.Map<ProgressViewModel>(response);

        viewModel.ReturnUrl = string.Empty;
        return View(viewModel);
    }

    #endregion
}