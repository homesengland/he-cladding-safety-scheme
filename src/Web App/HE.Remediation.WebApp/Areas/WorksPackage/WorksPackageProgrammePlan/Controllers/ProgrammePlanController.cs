using AutoMapper;
using FluentValidation.AspNetCore;
using HE.Remediation.Core.Enums;
using HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageProgrammePlan;
using HE.Remediation.WebApp.Constants;
using HE.Remediation.WebApp.ViewModels.WorksPackage.WorkPackageProgrammePlan;
using Mediator;
using Microsoft.AspNetCore.Mvc;

namespace HE.Remediation.WebApp.Areas.WorksPackage.WorksPackageProgrammePlan.Controllers;

[Area("WorksPackageProgrammePlan")]
[Route("WorksPackage/ProgrammePlan")]
public class ProgrammePlanController : StartController
{
	private readonly ISender _sender;
	private readonly IMapper _mapper;

	public ProgrammePlanController(ISender sender, IMapper mapper) : base(sender)
	{
		_sender = sender;
		_mapper = mapper;
	}


	protected override IActionResult DefaultStart => RedirectToAction("StartInformation", "ProgrammePlan", new { Area = "WorksPackageProgrammePlan" });

    #region Start Information

    [HttpGet(nameof(StartInformation))]
	public async Task<IActionResult> StartInformation(CancellationToken cancellationToken)
    {
        var response = await _sender.Send(GetStartInformationRequest.Request, cancellationToken);
        var model = _mapper.Map<StartInformationViewModel>(response);
		return View(model);
	}

	[HttpPost(nameof(StartInformation))]
	public async Task<IActionResult> StartInformation(StartInformationViewModel model, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        await _sender.Send(StartProgrammePlanRequest.Request, cancellationToken);

        return RedirectToAction("HasProjectPlan", "ProgrammePlan", new { Area = "WorksPackageProgrammePlan" });
    }

    #endregion

    #region Has Project Plan
    [HttpGet(nameof(HasProjectPlan))]
    public async Task<IActionResult> HasProjectPlan(CancellationToken cancellationToken)
    {
        var response = await _sender.Send(GetHasProjectPlanRequest.Request, cancellationToken);
        var model = _mapper.Map<HasProjectPlanViewModel>(response);
        return View(model);
    }

	[HttpPost(nameof(HasProjectPlan))]
	public async Task<IActionResult> HasProjectPlan(HasProjectPlanViewModel model, CancellationToken cancellationToken)
    {
        var validator = new HasProjectPlanViewModelValidator();
        var validationResult = await validator.ValidateAsync(model, cancellationToken);

        if (!validationResult.IsValid)
        {
            validationResult.AddToModelState(ModelState, string.Empty);
            return View(model);
        }

        var request = _mapper.Map<SetHasProjectPlanRequest>(model);
        await _sender.Send(request, cancellationToken);

        if (model.SubmitAction == ESubmitAction.Exit)
        {
            return RedirectToAction("TaskList", "WorkPackage", new { Area = "WorksPackage" });
        }

        return model.HasProjectPlan == true
            ? RedirectToAction("UploadProjectPlan", "ProgrammePlan", new { Area = "WorksPackageProgrammePlan" })
            : RedirectToAction("CheckYourAnswers", "ProgrammePlan", new { Area = "WorksPackageProgrammePlan" });
    }

    #endregion

    #region Upload Project Plan
    
    [HttpGet(nameof(UploadProjectPlan))]
    public async Task<IActionResult> UploadProjectPlan(CancellationToken cancellationToken)
    {
        var response = await _sender.Send(GetUploadProjectPlanRequest.Request, cancellationToken);
        var model = _mapper.Map<UploadProjectPlanViewModel>(response);
        return View(model);
    }

    [HttpPost(nameof(UploadProjectPlan))]
    [RequestSizeLimit(FileUploadConstants.MaxRequestSizeBytes)]
    public async Task<IActionResult> UploadProjectPlan(UploadProjectPlanViewModel model, CancellationToken cancellationToken)
    {
        var validator = new UploadProjectPlanViewModelValidator();
        var validationResult = await validator.ValidateAsync(model, cancellationToken);

        if (!validationResult.IsValid)
        {
            validationResult.AddToModelState(ModelState, string.Empty);
            return View(model);
        }

        if (model.SubmitAction == ESubmitAction.Upload)
        {
            var request = _mapper.Map<SetUploadProjectPlanRequest>(model);
            await _sender.Send(request, cancellationToken);

            return RedirectToAction("UploadProjectPlan", "ProgrammePlan", new { Area = "WorksPackageProgrammePlan" });
        }

        return model.SubmitAction == ESubmitAction.Continue
            ? RedirectToAction("CheckYourAnswers", "ProgrammePlan", new { Area = "WorksPackageProgrammePlan" })
            : RedirectToAction("TaskList", "WorkPackage", new { Area = "WorksPackage" });
    }

    [HttpGet(nameof(UploadProjectPlan) + "/Delete")]
    public async Task<IActionResult> DeleteProjectPlan(DeleteProjectPlanRequest request, CancellationToken cancellationToken)
    {
        await _sender.Send(request, cancellationToken);
        return RedirectToAction("UploadProjectPlan", "ProgrammePlan", new { Area = "WorksPackageProgrammePlan" });
    }

    #endregion

    #region Check Your Answers
    
    [HttpGet(nameof(CheckYourAnswers))]
    public async Task<IActionResult> CheckYourAnswers(CancellationToken cancellationToken)
    {
        var response = await _sender.Send(GetProjectPlanCheckYourAnswersRequest.Request, cancellationToken);
        var model = _mapper.Map<CheckYourAnswersViewModel>(response);
        return View(model);
    }

    [HttpPost(nameof(CheckYourAnswers))]
    public async Task<IActionResult> CheckYourAnswers(CheckYourAnswersViewModel model, CancellationToken cancellationToken)
    {
        if (!model.IsSubmitted)
        {
            await _sender.Send(SetProjectPlanCheckYourAnswersRequest.Request, cancellationToken);
        }

        return RedirectToAction("TaskList", "WorkPackage", new { Area = "WorksPackage" });
    }

    #endregion
}
