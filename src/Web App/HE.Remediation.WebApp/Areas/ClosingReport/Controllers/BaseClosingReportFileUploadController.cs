using AutoMapper;
using FluentValidation.AspNetCore;
using HE.Remediation.Core.Enums;
using HE.Remediation.Core.Exceptions;
using HE.Remediation.Core.UseCase.Areas.ClosingReport.AddFile;
using HE.Remediation.Core.UseCase.Areas.ClosingReport.DeleteFile;
using HE.Remediation.Core.UseCase.Areas.ClosingReport.GetUpload;
using HE.Remediation.Core.UseCase.Areas.ClosingReport.ProceedFromAbout;
using HE.Remediation.WebApp.Constants;
using HE.Remediation.WebApp.ViewModels.ClosingReport;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HE.Remediation.WebApp.Areas.ClosingReport.Controllers;

public abstract class BaseClosingReportFileUploadController : BaseAboutController
{
    private readonly ISender _sender;
    private readonly IMapper _mapper;

    protected abstract EClosingReportFileType ClosingReportFileType { get; }

    public BaseClosingReportFileUploadController(ISender sender, IMapper mapper) : base(sender)
    {
        _sender = sender;
        _mapper = mapper;
    }

    [HttpGet("upload")]
    public async Task<ActionResult> Upload(string returnUrl)
    {
        var response = await _sender.Send(new GetUploadRequest(ClosingReportFileType));
        var viewModel = _mapper.Map<UploadViewModel>(response);
        viewModel.ReturnUrl = Url.Action("About");

        return View(viewModel);
    }

    [RequestSizeLimit(FileUploadConstants.MaxRequestSizeBytes)]
    [HttpPost("upload")]
    public async Task<ActionResult> Upload(UploadViewModel model)
    {
        var validator = new UploadViewModelValidator();
        var validationResult = await validator.ValidateAsync(model);

        if (!validationResult.IsValid)
        {
            validationResult.AddToModelState(ModelState, string.Empty);
            return View(model);
        }

        if (model.SubmitAction == ESubmitAction.Upload)
        {
            var request = _mapper.Map<AddFileRequest>(model);
            try
            {
                await _sender.Send(request);
            }
            catch (InvalidFileException ex)
            {
                ModelState.AddModelError(nameof(request.File), ex.Message);
                return View(model);
            }
            return RedirectToAction("Upload", 
                ControllerContext.ActionDescriptor.ControllerName, 
                    new { Area = ControllerContext.RouteData.Values["area"]?.ToString() ?? string.Empty, model.UploadType, model.ReturnUrl });
        }

        if (model.SubmitAction == ESubmitAction.Continue)
        {
            await _sender.Send(new UpdateTaskStatusRequest(ClosingReportTask, ETaskStatus.Completed));
        }

        return RedirectToAction("TaskList", "ClosingReport", new { Area = "ClosingReport" });
    }

    [HttpGet("delete/{uploadType}")]
    public async Task<IActionResult> Delete([FromQuery] DeleteFileRequest request, EClosingReportFileType uploadType,
        [FromQuery] string returnUrl)
    {
        request.Task = ClosingReportTask;

        await _sender.Send(request);

        return RedirectToAction("Upload",
                    ControllerContext.ActionDescriptor.ControllerName,
                        new { Area = ControllerContext.RouteData.Values["area"]?.ToString() ?? string.Empty, uploadType, returnUrl });
    }
}