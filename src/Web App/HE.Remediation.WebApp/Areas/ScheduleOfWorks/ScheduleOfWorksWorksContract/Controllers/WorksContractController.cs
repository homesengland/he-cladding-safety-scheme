using AutoMapper;

using FluentValidation.AspNetCore;

using HE.Remediation.Core.Enums;
using HE.Remediation.Core.Exceptions;
using HE.Remediation.Core.UseCase.Areas.ScheduleOfWorks;
using HE.Remediation.Core.UseCase.Areas.ScheduleOfWorks.BaseInformation.Get;
using HE.Remediation.Core.UseCase.Areas.ScheduleOfWorks.WorksContract.Add;
using HE.Remediation.Core.UseCase.Areas.ScheduleOfWorks.WorksContract.Delete;
using HE.Remediation.Core.UseCase.Areas.ScheduleOfWorks.WorksContract.Get;
using HE.Remediation.Core.UseCase.Areas.ScheduleOfWorks.WorksContract.Set;
using HE.Remediation.WebApp.Constants;
using HE.Remediation.WebApp.ViewModels.ScheduleOfWorks;

using Mediator;

using Microsoft.AspNetCore.Mvc;

namespace HE.Remediation.WebApp.Areas.ScheduleOfWorks.ScheduleOfWorksWorksContract.Controllers;

[Area("ScheduleOfWorksWorksContract")]
[Route("ScheduleOfWorks/WorksContract")]
public class WorksContractController : StartController
{
    private readonly ISender _sender;
    private readonly IMapper _mapper;

    public WorksContractController(ISender sender, IMapper mapper) : base(sender)
    {
        _sender = sender;
        _mapper = mapper;
    }
    protected override IActionResult DefaultStart =>
     RedirectToAction("StartInformation", "WorksContract", new { Area = "ScheduleOfWorksWorksContract" });

    #region Start Information

    [HttpGet(nameof(StartInformation))]
    public async Task<IActionResult> StartInformation()
    {
        var response = await _sender.Send(GetBaseInformationRequest.Request);
        var viewModel = _mapper.Map<WorksContractStartInformationViewModel>(response);

        viewModel.ReturnUrl = string.Empty;
        return View(viewModel);
    }

    [HttpPost(nameof(StartInformation))]
    public IActionResult StartInformation(StartInformationViewModel viewModel)
    {
        return RedirectToAction("UploadWorksContract", "WorksContract", new { Area = "ScheduleOfWorksWorksContract" });
    }

    #endregion

    #region "Upload works contract"

    [HttpGet(nameof(UploadWorksContract))]
    public async Task<IActionResult> UploadWorksContract(string returnUrl)
    {
        var response = await _sender.Send(GetWorksContractRequest.Request);
        var viewModel = _mapper.Map<UploadWorksContractViewModel>(response);

        viewModel.ReturnUrl = returnUrl;
        return View(viewModel);
    }

    [HttpPost(nameof(UploadWorksContract))]
    [RequestSizeLimit(FileUploadConstants.MaxRequestSizeBytes)]
    public async Task<IActionResult> UploadWorksContract(UploadWorksContractViewModel viewModel)
    {
        var validator = new UploadWorksContractViewModelValidator();
        var validationResult = await validator.ValidateAsync(viewModel);

        if (!validationResult.IsValid)
        {
            validationResult.AddToModelState(ModelState, string.Empty);
            return View(viewModel);
        }

        if (viewModel.SubmitAction == ESubmitAction.Continue)
        {
            var contractsUploadedRequest = _mapper.Map<SetWorksContractRequest>(viewModel);
            await _sender.Send(contractsUploadedRequest);
            return RedirectToAction("TaskList", "ScheduleOfWorks", new { Area = "ScheduleOfWorks" });
        }

        if (viewModel.SubmitAction == ESubmitAction.Upload)
        {

            var request = _mapper.Map<AddWorksContractRequest>(viewModel);
            try
            {
                await _sender.Send(request);
            }
            catch (InvalidFileException ex)
            {
                ModelState.AddModelError(nameof(request.File), ex.Message);
                return View(viewModel);
            }
        }

        if (viewModel.SubmitAction == ESubmitAction.Exit)
        {
            return RedirectToAction("TaskList", "ScheduleOfWorks", new { Area = "ScheduleOfWorks" });
        }

        return viewModel.ReturnUrl is not null
            ? RedirectToAction("UploadWorksContract", "WorksContract", new { Area = "ScheduleOfWorksWorksContract", returnUrl = viewModel.ReturnUrl })
            : RedirectToAction("UploadWorksContract", "WorksContract", new { Area = "ScheduleOfWorksWorksContract" });
    }

    [HttpGet(nameof(UploadWorksContract) + "/Delete")]
    public async Task<IActionResult> DeleteWorksContract([FromQuery] DeleteWorksContractRequest request, [FromQuery] string returnUrl)
    {
        await _sender.Send(request);

        return returnUrl is not null
            ? RedirectToAction("UploadWorksContract", "WorksContract", new { Area = "ScheduleOfWorksWorksContract", returnUrl })
            : RedirectToAction("UploadWorksContract", "WorksContract", new { Area = "ScheduleOfWorksWorksContract" });
    }

    #endregion
}