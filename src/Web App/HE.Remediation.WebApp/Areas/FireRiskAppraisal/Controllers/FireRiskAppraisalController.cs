using AutoMapper;
using FluentValidation.AspNetCore;
using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Enums;
using HE.Remediation.Core.Exceptions;
using HE.Remediation.Core.Interface;
using HE.Remediation.Core.UseCase.Areas.FireRiskAppraisal.AppraisalSurveyDetails.GetAppraisalSurveyDetails;
using HE.Remediation.Core.UseCase.Areas.FireRiskAppraisal.AppraisalSurveyDetails.SetAppraisalSurveyDetails;
using HE.Remediation.Core.UseCase.Areas.FireRiskAppraisal.AssessorDetails.GetAssessorDetails;
using HE.Remediation.Core.UseCase.Areas.FireRiskAppraisal.AssessorDetails.SetAssessorDetails;
using HE.Remediation.Core.UseCase.Areas.FireRiskAppraisal.AssessorList.GetAssessorList;
using HE.Remediation.Core.UseCase.Areas.FireRiskAppraisal.CompletedAppraisal.GetCompletedAppraisal;
using HE.Remediation.Core.UseCase.Areas.FireRiskAppraisal.CompletedAppraisal.SetCompletedAppraisal;
using HE.Remediation.Core.UseCase.Areas.FireRiskAppraisal.SurveyInstructionDetails.GetSurveyInstructionDetails;
using HE.Remediation.Core.UseCase.Areas.FireRiskAppraisal.SurveyInstructionDetails.SetSurveyInstructionDetails;
using HE.Remediation.Core.UseCase.Areas.FireRiskAppraisal.UploadFireRiskAppraisalReport;
using HE.Remediation.Core.UseCase.Areas.FireRiskAppraisal.UploadFireRiskAppraisalReport.DeleteFireRiskAppraisalReport;
using HE.Remediation.Core.UseCase.Areas.FireRiskAppraisal.UploadFireRiskAppraisalReport.GetFireRiskAppraisalReport;
using HE.Remediation.Core.UseCase.Areas.FireRiskAppraisal.UploadFireRiskAppraisalReport.UploadFireRiskAppraisalReport;
using HE.Remediation.WebApp.Authorisation;
using HE.Remediation.WebApp.Constants;
using HE.Remediation.WebApp.ViewModels.FireRiskAppraisal;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HE.Remediation.WebApp.Areas.FireRiskAppraisal.Controllers
{
    [Area("FireRiskAppraisal")]
    [Route("FireRiskAppraisal")]
    [CookieApplicationAuthorise]
    public class FireRiskAppraisalController : Controller
    {
        private readonly ISender _sender;
        private readonly IMapper _mapper;
        private readonly IFireAssessorListRepository _fireAssessorListService;

        public FireRiskAppraisalController(ISender sender, IMapper mapper, IFireAssessorListRepository fireAssessorListService)
        {
            _sender = sender;
            _mapper = mapper;
            _fireAssessorListService = fireAssessorListService;
        }

        #region WhatYouWillNeed
        [HttpGet(nameof(WhatYouWillNeed))]
        public IActionResult WhatYouWillNeed()
        {
            return View();
        }
        #endregion

        #region Completed Appraisal

        [HttpGet(nameof(CompletedAppraisal))]
        public async Task<IActionResult> CompletedAppraisal()
        {
            var response = await _sender.Send(GetCompletedAppraisalRequest.Request);
            var model = _mapper.Map<CompletedAppraisalViewModel>(response);
            return View(model);
        }

        [HttpPost(nameof(SetCompletedAppraisal))]
        public async Task<IActionResult> SetCompletedAppraisal(CompletedAppraisalViewModel viewModel, ESubmitAction submitAction)
        {
            var validator = new CompletedAppraisalViewModelValidator();

            var validationResult = await validator.ValidateAsync(viewModel);

            if (!validationResult.IsValid)
            {
                validationResult.AddToModelState(ModelState, String.Empty);
                return View("CompletedAppraisal", viewModel);
            }

            var request = _mapper.Map<SetCompletedAppraisalRequest>(viewModel);
            await _sender.Send(request);

            return submitAction == ESubmitAction.Continue ?
                request.IsAppraisalCompleted == true ?
                    RedirectToAction("AppraisalSurveyDetails", "FireRiskAppraisal", new { Area = "FireRiskAppraisal" }) :
                    RedirectToAction("AssessorList", "FireRiskAppraisal", new { Area = "FireRiskAppraisal" }) :
                RedirectToAction("Index", "TaskList", new { Area = "Application" });
        }

        #endregion

        #region Assessor List
        [HttpGet(nameof(AssessorList))]
        public async Task<IActionResult> AssessorList(bool fireAccessorNotOnPanel)
        {
            var response = await _sender.Send(GetAssessorListRequest.Request);
            var viewModel = _mapper.Map<AssessorListViewModel>(response);
            ViewData["FireAccessorNotOnPanel"] = fireAccessorNotOnPanel;
            return View(viewModel);
        }
        #endregion

        #region Survey Instruction Details

        [HttpGet(nameof(SurveyInstructionDetails))]
        public async Task<IActionResult> SurveyInstructionDetails()
        {
            var surveyInstructionDetailsResponse = await _sender.Send(GetSurveyInstructionDetailsRequest.Request);

            var viewModel = _mapper.Map<SurveyInstructionDetailsViewModel>(surveyInstructionDetailsResponse);

            return View(viewModel);
        }

        [HttpPost(nameof(SurveyInstructionDetails))]
        public async Task<IActionResult> SurveyInstructionDetails(SurveyInstructionDetailsViewModel viewModel)
        {
            var validator = new SurveyInstructionDetailsViewModelValidator();

            var validationResult = await validator.ValidateAsync(viewModel);

            if (validationResult.IsValid)
            {
                var request = _mapper.Map<SetSurveyInstructionDetailsRequest>(viewModel);

                await _sender.Send(request);

                if (viewModel.SubmitAction == ESubmitAction.Continue)
                {
                    // Removing file upload until this can be plumbed in properly
                    return RedirectToAction("CompletedAppraisal", "FireRiskAppraisal", new { area = "FireRiskAppraisal" });
                }

                return RedirectToAction("Index", "TaskList", new { area = "Application" });
            }

            validationResult.AddToModelState(ModelState, String.Empty);

            var fireAssessors = await _fireAssessorListService.GetFireAssessorList();
            viewModel.FireRiskAssessorCompanies = _mapper.Map<List<FireRiskAssessorCompanyViewModel>>(fireAssessors);

            return View("SurveyInstructionDetails", viewModel);
        }

        #endregion

        #region Appraisal Survey Details

        [HttpGet(nameof(AppraisalSurveyDetails))]
        public async Task<IActionResult> AppraisalSurveyDetails()
        {
            var appraisalSurveyDetailsResponse = await _sender.Send(GetAppraisalSurveyDetailsRequest.Request);

            var viewModel = _mapper.Map<AppraisalSurveyDetailsViewModel>(appraisalSurveyDetailsResponse);

            return View(viewModel);
        }

        [HttpGet(nameof(AppraisalSurveyDetailsNotOnPanel))]
        public async Task<IActionResult> AppraisalSurveyDetailsNotOnPanel()
        {
            var appraisalSurveyDetailsResponse = await _sender.Send(GetAppraisalSurveyDetailsRequest.Request);

            var viewModel = _mapper.Map<AppraisalSurveyDetailsViewModel>(appraisalSurveyDetailsResponse);

            return View(viewModel);
        }

        [HttpPost(nameof(AppraisalSurveyDetails))]
        public Task<IActionResult> AppraisalSurveyDetails(AppraisalSurveyDetailsViewModel viewModel)
        {
            return SaveAppraisalSurveyDetails(viewModel);
        }

        [HttpPost(nameof(AppraisalSurveyDetailsNotOnPanel))]
        public Task<IActionResult> AppraisalSurveyDetailsNotOnPanel(AppraisalSurveyDetailsViewModel viewModel)
        {
            viewModel.FireAccessorNotOnPanel = true;

            return SaveAppraisalSurveyDetails(viewModel);
        }

        private async Task<IActionResult> SaveAppraisalSurveyDetails(AppraisalSurveyDetailsViewModel viewModel)
        {
            var validator = new AppraisalSurveyViewModelValidator();

            var validationResult = await validator.ValidateAsync(viewModel);

            if (validationResult.IsValid)
            {
                var request = _mapper.Map<SetAppraisalSurveyDetailsRequest>(viewModel);

                await _sender.Send(request);

                if (viewModel.SubmitAction == ESubmitAction.Continue)
                {
                    return RedirectToAction("UploadFireRiskAppraisalReport", "FireRiskAppraisal", new { area = "FireRiskAppraisal" });
                }

                return RedirectToAction("Index", "TaskList", new { area = "Application" });
            }

            validationResult.AddToModelState(ModelState, String.Empty);

            var fireAssessors = await _fireAssessorListService.GetFireAssessorList();
            viewModel.FireRiskAssessorCompanies = _mapper.Map<List<FireRiskAssessorCompanyViewModel>>(fireAssessors);

            if (viewModel.FireAccessorNotOnPanel)
            {
                return View("AppraisalSurveyDetailsNotOnPanel", viewModel);
            }
            else
            {
                return View("AppraisalSurveyDetails", viewModel);
            }
        }

        #endregion

        #region Assessor Details

       

        #endregion

        #region Fire Risk Assessor Details

        [HttpGet(nameof(AssessorDetails))]
        public async Task<IActionResult>  AssessorDetails()
        {
            var response = await _sender.Send(GetAssessorDetailsRequest.Request);
            var model = _mapper.Map<FireRiskAssessorDetailsViewModel>(response);
            return View(model);
        }

        [HttpPost(nameof(AssessorDetails))]
        public async Task<IActionResult> AssessorDetails(FireRiskAssessorDetailsViewModel viewModel)
        {
            var validator = new FireRiskAssessorDetailsViewModelValidator();

            var validationResult = await validator.ValidateAsync(viewModel);

            if (!validationResult.IsValid)
            {
                validationResult.AddToModelState(ModelState, String.Empty);
                return View("AssessorDetails", viewModel);
            }
            var request = _mapper.Map<SetAssessorDetailsRequest>(viewModel);
            await _sender.Send(request);

            if (viewModel.SubmitAction == ESubmitAction.Continue)
            {
                return RedirectToAction("AppraisalSurveyDetailsNotOnPanel", "FireRiskAppraisal", new { Area = "FireRiskAppraisal" });
            }
            return RedirectToAction("Index", "TaskList", new { area = "Application" });
        }

        #endregion

        #region Upload Fire Risk Appraisal Report

        [HttpGet(nameof(UploadFireRiskAppraisalReport))]
        public async Task<IActionResult> UploadFireRiskAppraisalReport()
        {
            var file = await _sender.Send(new GetFireRiskReportAppraisalReportRequest());

            var files = _mapper.Map<List<ViewModels.Shared.File>>(file);

            var model = new UploadFireRiskAppraisalReportViewModel
            {
                AddedFiles = files

            };
            return View(model);
        }

        [HttpPost(nameof(UploadFireRiskAppraisalReport))]
        [RequestSizeLimit(FileUploadConstants.MaxRequestSizeBytes)]
        public async Task<IActionResult> UploadFireRiskAppraisalReport(UploadFireRiskAppraisalReportViewModel viewModel, ESubmitAction submitAction)
        {
            try
            {
                await _sender.Send(new UploadFireRiskAppraisalReportRequest
                {
                    File = viewModel.File,
                });
            }
            catch (InvalidFileException ex)
            {
                ModelState.AddModelError("File", ex.Message);

                return View(viewModel);
            }

            return submitAction == ESubmitAction.Continue ? RedirectToAction("Summary", "FireRiskAppraisal", new { area = "FireRiskAppraisal" })
                : RedirectToAction("Index", "TaskList", new { Area = "Application" });
        }

        [HttpGet("DeleteReport")]
        public async Task<IActionResult> DeleteReport([FromQuery] Guid fileId)
        {
            var request = new DeleteFireRiskAppraisalRequest
            {
                FileId = fileId
            };

            await _sender.Send(request);

            return RedirectToAction("UploadFireRiskAppraisalReport", "FireRiskAppraisal", new { Area = "FireRiskAppraisal" });
        }

        #endregion

        #region Summary

        [HttpGet(nameof(Summary))]
        public IActionResult Summary()
        {
            return View();
        }

        #endregion
    }
}
