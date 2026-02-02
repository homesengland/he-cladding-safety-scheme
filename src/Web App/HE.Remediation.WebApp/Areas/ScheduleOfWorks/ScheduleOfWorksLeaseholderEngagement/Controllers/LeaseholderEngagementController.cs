using AutoMapper;
using FluentValidation.AspNetCore;
using HE.Remediation.Core.Enums;
using HE.Remediation.Core.Exceptions;
using HE.Remediation.Core.UseCase.Areas.ScheduleOfWorks;
using HE.Remediation.Core.UseCase.Areas.ScheduleOfWorks.BaseInformation.Get;
using HE.Remediation.WebApp.Constants;
using HE.Remediation.WebApp.ViewModels.ScheduleOfWorks;
using Mediator;
using Microsoft.AspNetCore.Mvc;


namespace HE.Remediation.WebApp.Areas.ScheduleOfWorks.ScheduleOfWorksLeaseholderEngagement.Controllers;


[Area("ScheduleOfWorksLeaseholderEngagement")]
[Route("ScheduleOfWorks/LeaseholderEngagement")]
public class LeaseholderEngagementController : StartController
{
    private readonly ISender _sender;
    private readonly IMapper _mapper;


    public LeaseholderEngagementController(ISender sender, IMapper mapper) : base(sender)
    {
        _sender = sender;
        _mapper = mapper;
    }
    protected override IActionResult DefaultStart =>
     RedirectToAction("StartInformation", "LeaseholderEngagement", new { Area = "ScheduleOfWorksLeaseholderEngagement" });


    #region Start Information


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
        return RedirectToAction("UploadLeaseholderEngagement", "LeaseholderEngagement", new { Area = "ScheduleOfWorksLeaseholderEngagement" });
    }


    #endregion


    #region Upload Leaseholder Engagement
    [HttpGet(nameof(UploadLeaseholderEngagement))]
    public async Task<IActionResult> UploadLeaseholderEngagement(string returnUrl, CancellationToken cancellationToken)
    {
        var response = await _sender.Send(GetLeaseholderEngagementRequest.Request, cancellationToken);
        var model = _mapper.Map<UploadLeaseholderEngagementViewModel>(response);
        model.ReturnUrl = returnUrl;
        return View(model);
    }


    [HttpPost(nameof(UploadLeaseholderEngagement))]
    [RequestSizeLimit(FileUploadConstants.MaxRequestSizeBytes)]
    public async Task<IActionResult> UploadLeaseholderEngagement(UploadLeaseholderEngagementViewModel model, CancellationToken cancellationToken)
    {
        var validator = new UploadLeaseholderEngagementViewModelValidator();
        var validationResult = await validator.ValidateAsync(model, cancellationToken);


        if (!validationResult.IsValid)
        {
            validationResult.AddToModelState(ModelState, string.Empty);
            return View(model);
        }


        if (model.SubmitAction == ESubmitAction.Upload)
        {
            try
            {
                var request = _mapper.Map<AddLeaseholderEngagementFileRequest>(model);
                await _sender.Send(request, cancellationToken);
                return RedirectToAction("UploadLeaseholderEngagement", "LeaseholderEngagement",
                    new { Area = "ScheduleOfWorksLeaseholderEngagement", model.ReturnUrl });
            }
            catch (InvalidFileException ex)
            {
                ModelState.AddModelError(nameof(model.File), ex.Message);
                return View(model);
            }
        }


        if (model.SubmitAction == ESubmitAction.Continue)
        {
            var request = SetLeaseholderEngagementRequest.Request;
            await _sender.Send(request, cancellationToken);
            return RedirectToAction("TaskList", "ScheduleOfWorks", new { Area = "ScheduleOfWorks" });
        }


        return RedirectToAction("TaskList", "ScheduleOfWorks", new { area = "ScheduleOfWorks" });
    }


    [HttpGet(nameof(UploadLeaseholderEngagement) + "/Delete")]
    public async Task<IActionResult> DeletedLeaseHolderEngagement(
        DeleteLeaseholderEngagementFileRequest request,
        CancellationToken cancellationToken)
    {
        await _sender.Send(request, cancellationToken);
        return RedirectToAction("UploadLeaseholderEngagement", "LeaseholderEngagement", new { Area = "ScheduleOfWorksLeaseholderEngagement" });
    }


    #endregion
}