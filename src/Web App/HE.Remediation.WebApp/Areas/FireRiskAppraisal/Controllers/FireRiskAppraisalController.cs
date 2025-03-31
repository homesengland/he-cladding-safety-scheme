using AutoMapper;
using FluentValidation.AspNetCore;
using HE.Remediation.Core.Attributes;
using HE.Remediation.Core.Enums;
using HE.Remediation.Core.Exceptions;
using HE.Remediation.Core.UseCase.Areas.FireRiskAppraisal.AddExternalWallWorks;
using HE.Remediation.Core.UseCase.Areas.FireRiskAppraisal.AddInternalWallWorks;
using HE.Remediation.Core.UseCase.Areas.FireRiskAppraisal.AppraisalSurveyDetails.GetAppraisalSurveyDetails;
using HE.Remediation.Core.UseCase.Areas.FireRiskAppraisal.AppraisalSurveyDetails.SetAppraisalSurveyDetails;
using HE.Remediation.Core.UseCase.Areas.FireRiskAppraisal.AssessorDetails.GetAssessorDetails;
using HE.Remediation.Core.UseCase.Areas.FireRiskAppraisal.AssessorDetails.SetAssessorDetails;
using HE.Remediation.Core.UseCase.Areas.FireRiskAppraisal.AssessorList.GetAssessorList;
using HE.Remediation.Core.UseCase.Areas.FireRiskAppraisal.CheckYourAnswers.GetCheckYourAnswers;
using HE.Remediation.Core.UseCase.Areas.FireRiskAppraisal.CheckYourAnswers.SetCheckYourAnswers;
using HE.Remediation.Core.UseCase.Areas.FireRiskAppraisal.CompletedAppraisal.GetCompletedAppraisal;
using HE.Remediation.Core.UseCase.Areas.FireRiskAppraisal.CompletedAppraisal.SetCompletedAppraisal;
using HE.Remediation.Core.UseCase.Areas.FireRiskAppraisal.ExternalWallWorks;
using HE.Remediation.Core.UseCase.Areas.FireRiskAppraisal.ExternalWorksRequired;
using HE.Remediation.Core.UseCase.Areas.FireRiskAppraisal.FireRiskAssessorListPdf;
using HE.Remediation.Core.UseCase.Areas.FireRiskAppraisal.InterimMeasures.GetInterimMeasures;
using HE.Remediation.Core.UseCase.Areas.FireRiskAppraisal.InterimMeasures.SetInterimMeasures;
using HE.Remediation.Core.UseCase.Areas.FireRiskAppraisal.InternalWallWorks;
using HE.Remediation.Core.UseCase.Areas.FireRiskAppraisal.InternalWorksRequired;
using HE.Remediation.Core.UseCase.Areas.FireRiskAppraisal.RecommendedWorks.GetRecommendedWorks;
using HE.Remediation.Core.UseCase.Areas.FireRiskAppraisal.RecommendedWorks.SetRecommendedWorks;
using HE.Remediation.Core.UseCase.Areas.FireRiskAppraisal.RecommendedWorksStart.GetRecommendedWorksStart;
using HE.Remediation.Core.UseCase.Areas.FireRiskAppraisal.ReportDetails.GetReportDetails;
using HE.Remediation.Core.UseCase.Areas.FireRiskAppraisal.ReportDetails.SetReportDetails;
using HE.Remediation.Core.UseCase.Areas.FireRiskAppraisal.SurveyInstructionDetails.GetSurveyInstructionDetails;
using HE.Remediation.Core.UseCase.Areas.FireRiskAppraisal.SurveyInstructionDetails.SetSurveyInstructionDetails;
using HE.Remediation.Core.UseCase.Areas.FireRiskAppraisal.UploadFireRiskAppraisalReport.DeleteFireRiskAppraisalReport;
using HE.Remediation.Core.UseCase.Areas.FireRiskAppraisal.UploadFireRiskAppraisalReport.GetFireRiskAppraisalReport;
using HE.Remediation.Core.UseCase.Areas.FireRiskAppraisal.UploadFireRiskAppraisalReport.UploadFireRiskAppraisalReport;
using HE.Remediation.Core.UseCase.Areas.FireRiskAppraisal.WorksToCladdingSystems.CladdingArea;
using HE.Remediation.Core.UseCase.Areas.FireRiskAppraisal.WorksToCladdingSystems.DeleteCladdingSystem;
using HE.Remediation.Core.UseCase.Areas.FireRiskAppraisal.WorksToCladdingSystems.GetCladdingSystem;
using HE.Remediation.Core.UseCase.Areas.FireRiskAppraisal.WorksToCladdingSystems.GetWorksToCladdingSystems;
using HE.Remediation.Core.UseCase.Areas.FireRiskAppraisal.WorksToCladdingSystems.SetCladdingArea;
using HE.Remediation.Core.UseCase.Areas.FireRiskAppraisal.WorksToCladdingSystems.SetCladdingSystem;
using HE.Remediation.WebApp.Attributes.Routing;
using HE.Remediation.WebApp.Constants;
using HE.Remediation.WebApp.ViewModels.FireRiskAppraisal;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HE.Remediation.WebApp.Areas.FireRiskAppraisal.Controllers
{
    [Area("FireRiskAppraisal")]
    [Route("FireRiskAppraisal")]
    public class FireRiskAppraisalController : StartController
    {
        private readonly ISender _sender;
        private readonly IMapper _mapper;

        public FireRiskAppraisalController(ISender sender, IMapper mapper)
            : base(sender)
        {
            _sender = sender;
            _mapper = mapper;
        }

        protected override IActionResult DefaultStart => RedirectToAction("WhatYouWillNeed", "FireRiskAppraisal",
            new { Area = "FireRiskAppraisal" });

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

            return View("SurveyInstructionDetails", viewModel);
        }

        #endregion

        #region Appraisal Survey Details

        [HttpGet(nameof(AppraisalSurveyDetails))]
        public async Task<IActionResult> AppraisalSurveyDetails(string returnUrl)
        {
            var appraisalSurveyDetailsResponse = await _sender.Send(GetAppraisalSurveyDetailsRequest.Request);

            var viewModel = _mapper.Map<AppraisalSurveyDetailsViewModel>(appraisalSurveyDetailsResponse);
            viewModel.ReturnUrl = returnUrl;

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

                var action = nameof(Guidance);
                if (viewModel.FireAccessorNotOnPanel)
                {
                    action = nameof(GuidanceNotOnPanel);
                }
                action = viewModel.ReturnUrl is null ? action : viewModel.ReturnUrl;

                return viewModel.SubmitAction == ESubmitAction.Continue 
                                    ? SafeRedirectToAction(action, "FireRiskAppraisal", new { area = "FireRiskAppraisal" })
                                    : RedirectToAction("Index", "TaskList", new { Area = "Application" });                
            }

            validationResult.AddToModelState(ModelState, String.Empty);

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
        public async Task<IActionResult> AssessorDetails()
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
        public async Task<IActionResult> UploadFireRiskAppraisalReport(string returnUrl)
        {
            var file = await _sender.Send(new GetFireRiskReportAppraisalReportRequest());

            var fraewFile = _mapper.Map<ViewModels.Shared.File>(file.FraewFile);
            var summaryFile = _mapper.Map<ViewModels.Shared.File>(file.SummaryFile);

            var model = new UploadFireRiskAppraisalReportViewModel
            {
                AddedFraew = fraewFile,
                AddedSummary = summaryFile,
                ReturnUrl = returnUrl
            };

            return View(model);
        }

        [HttpPost(nameof(UploadFireRiskAppraisalReport))]
        [RequestSizeLimit(FileUploadConstants.MaxRequestSizeBytes)]
        public async Task<IActionResult> UploadFireRiskAppraisalReport(UploadFireRiskAppraisalReportViewModel viewModel, ESubmitAction submitAction)
        {
            if (!ModelState.IsValid)
            {
                // this will happen when the request size limit is exceeded, the model is null so manually add the error message
                var file = await _sender.Send(new GetFireRiskReportAppraisalReportRequest());

                var fraewFile = _mapper.Map<ViewModels.Shared.File>(file.FraewFile);
                var summaryFile = _mapper.Map<ViewModels.Shared.File>(file.SummaryFile);

                viewModel = new UploadFireRiskAppraisalReportViewModel
                {
                    AddedFraew = fraewFile,
                    AddedSummary = summaryFile
                };

                ModelState.AddModelError(nameof(viewModel.Fraew), "Your files collectively are greater than 100mb");
                ModelState.AddModelError(nameof(viewModel.FraewSummary), "Your files collectively are greater than 100mb");

                return View(viewModel);
            }

            var validator = new UploadFireRiskAppraisalReportViewModelValidator();
            var validationResult = await validator.ValidateAsync(viewModel);

            if (!validationResult.IsValid)
            {
                validationResult.AddToModelState(ModelState, string.Empty);
                return View(viewModel);
            }

            try
            {
                await _sender.Send(new UploadFireRiskAppraisalReportRequest
                {
                    FraewFile = viewModel.Fraew,
                    SummaryFile = viewModel.FraewSummary
                });
            }
            catch (InvalidFileException ex)
            {
                foreach (var error in ex.Errors)
                {
                    ModelState.AddModelError(error.Key, error.Value);
                }

                return View(viewModel);
            }

            var action = nameof(ReportDetails);
            action = viewModel.ReturnUrl is null ? action : viewModel.ReturnUrl;

            return submitAction == ESubmitAction.Continue 
                ? SafeRedirectToAction(action, "FireRiskAppraisal", new { area = "FireRiskAppraisal" })
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

        #region External Works
        [HttpGet(nameof(ExternalWorksRequired))]
        public async Task<IActionResult> ExternalWorksRequired(string returnUrl)
        {
            var response = await _sender.Send(new GetExternalWorksRequiredRequest());

            var model = new ExternalWorksRequiredViewModel
            {
                WorksRequired = response.WorksRequired,
                ReturnUrl = returnUrl   
            };

            return View(model);
        }

        [HttpPost(nameof(ExternalWorksRequired))]
        public async Task<IActionResult> ExternalWorksRequired(ExternalWorksRequiredViewModel viewModel)
        {
            var validator = new ExternalWorksRequiredViewModelValidator();
            var validationResult = await validator.ValidateAsync(viewModel);

            if (!validationResult.IsValid)
            {
                validationResult.AddToModelState(ModelState, string.Empty);
                return View(viewModel);
            }

            await _sender.Send(new SetExternalWorksRequiredRequest { WorkRequired = viewModel.WorksRequired!.Value });

            if (viewModel.SubmitAction == ESubmitAction.Continue)
            {
                var action = viewModel.WorksRequired!.Value == ENoYes.Yes
                    ? nameof(ExternalWallWorks)
                    : nameof(InternalWorksRequired);

                if (viewModel.ReturnUrl is not null)
                {
                    if (viewModel.WorksRequired!.Value == ENoYes.Yes)
                    {
                        return SafeRedirectToAction(action, "FireRiskAppraisal", new { Area = "FireRiskAppraisal", returnUrl = viewModel.ReturnUrl });
                    }

                    return SafeRedirectToAction(viewModel.ReturnUrl, "FireRiskAppraisal", new { Area = "FireRiskAppraisal" });
                }
                
                return SafeRedirectToAction(action, "FireRiskAppraisal", new { Area = "FireRiskAppraisal" });
            }

            return RedirectToAction("Index", "TaskList", new { Area = "Application" });
        }

        #endregion

        #region External Walls Works

        [HttpGet(nameof(ExternalWallWorks))]
        public async Task<IActionResult> ExternalWallWorks(string returnUrl)
        {
            var response = await _sender.Send(GetExternalWallWorksRequest.Request);
            var wallWorks = _mapper.Map<List<GetExternalWorksViewModel.ExternalWallWorks>>(response);

            var outputModel = new GetExternalWorksViewModel()
            {
                WallWorks = wallWorks,
                ReturnUrl = returnUrl
            };
            return View(outputModel);
        }

        [HttpPost(nameof(SetExternalWallWorks))]
        public IActionResult SetExternalWallWorks(GetExternalWorksViewModel viewModel)
        {
            if (viewModel.WallWorks?.Count > 0)
            {
                if (viewModel.ReturnUrl is not null)
                {
                    return SafeRedirectToAction(viewModel.ReturnUrl, "FireRiskAppraisal", new { area = "FireRiskAppraisal", returnUrl = viewModel.ReturnUrl });    
                }

                return RedirectToAction(nameof(InternalWorksRequired), "FireRiskAppraisal", new { area = "FireRiskAppraisal" });    
            }

            ModelState.AddModelError("WallWorks", "Enter at least one external wall element");

            return View("ExternalWallWorks", viewModel);
        }


        [HttpGet("DeleteExternalWallWorks/{Id?}")]
        public async Task<IActionResult> DeleteExternalWallWorks(Guid? Id, string returnUrl)
        {
            var request = new SetDeleteExternalWallWorksRequest
            {
                Id = Id.HasValue ? Id.Value : null
            };
            await _sender.Send(request);

            if (returnUrl is not null)
            {
                return RedirectToAction("ExternalWallWorks", "FireRiskAppraisal", new { Area = "FireRiskAppraisal", returnUrl = returnUrl });
            }

            return RedirectToAction("ExternalWallWorks", "FireRiskAppraisal", new { Area = "FireRiskAppraisal" });
        }

        #endregion

        #region "Add External Wall Works"

        [HttpGet("AddExternalWallWorks/{Id?}")]
        public async Task<IActionResult> AddExternalWallWorks(Guid? Id, int elementNo, string returnUrl)
        {
            var request = new GetAddExternalWallWorksRequest
            {
                Id = Id
            };

            var response = await _sender.Send(request);
            var model = _mapper.Map<ExternalWorksDetailViewModel>(response);

            if (model == null)
            {
                model = new ExternalWorksDetailViewModel();
            }

            model.ElementNo = elementNo;
            model.ReturnUrl = returnUrl;

            return View(model);
        }


        [HttpPost("AddExternalWallWorks/{Id?}")]
        public async Task<IActionResult> AddExternalWallWorks(ExternalWorksDetailViewModel viewModel, ESubmitAction submitAction)
        {
            var validator = new ExternalWorksDetailViewModelValidator();
            var validationResult = await validator.ValidateAsync(viewModel);

            if (!validationResult.IsValid)
            {
                validationResult.AddToModelState(ModelState, String.Empty);
                return View("AddExternalWallWorks", viewModel);
            }

            var request = _mapper.Map<SetAddExternalWallWorksRequest>(viewModel);
            await _sender.Send(request);

            if (viewModel.ReturnUrl is not null)
            {
                return RedirectToAction("ExternalWallWorks", "FireRiskAppraisal", new { Area = "FireRiskAppraisal", returnUrl = viewModel.ReturnUrl });
            }            

            return RedirectToAction("ExternalWallWorks", "FireRiskAppraisal", new { Area = "FireRiskAppraisal" });
        }

        #endregion

        #region Internal Works
        [HttpGet(nameof(InternalWorksRequired))]
        public async Task<IActionResult> InternalWorksRequired(string returnUrl)
        {
            var response = await _sender.Send(new GetInternalWorksRequiredRequest());

            var model = new InternalWorksRequiredViewModel
            {
                WorksRequired = response.WorksRequired,
                ExternalWorksRequired = response.ExternalWorksRequired,
                ReturnUrl = returnUrl
            };

            return View(model);
        }

        [HttpPost(nameof(InternalWorksRequired))]
        public async Task<IActionResult> InternalWorksRequired(InternalWorksRequiredViewModel viewModel)
        {
            var validator = new InternalWorksRequiredViewModelValidator();
            var validationResult = await validator.ValidateAsync(viewModel);

            if (!validationResult.IsValid)
            {
                validationResult.AddToModelState(ModelState, string.Empty);
                return View(viewModel);
            }

            await _sender.Send(new SetInternalWorksRequiredRequest { WorkRequired = viewModel.WorksRequired!.Value });

            if (viewModel.SubmitAction == ESubmitAction.Continue)
            {
                var action = viewModel.WorksRequired!.Value == ENoYes.Yes
                    ? nameof(InternalWallWorks)
                    : nameof(RecommendedWorksStart);

                if (viewModel.ReturnUrl is not null)
                {
                    if (viewModel.WorksRequired!.Value == ENoYes.Yes)
                    {
                        return SafeRedirectToAction(action, "FireRiskAppraisal", new { Area = "FireRiskAppraisal", returnUrl = viewModel.ReturnUrl });
                    }

                    return SafeRedirectToAction(viewModel.ReturnUrl, "FireRiskAppraisal", new { Area = "FireRiskAppraisal" });
                }

                return SafeRedirectToAction(action, "FireRiskAppraisal", new { Area = "FireRiskAppraisal" });
            }

            return RedirectToAction("Index", "TaskList", new { Area = "Application" });
        }

        #endregion

        #region "Add Internal Wall Works"

        [HttpGet("AddInternalWallWorks/{Id?}")]
        public async Task<IActionResult> AddInternalWallWorks(Guid? Id, int elementNo, string returnUrl)
        {
            var request = new GetAddInternalWallWorksRequest
            {
                Id = Id
            };

            var response = await _sender.Send(request);
            var model = _mapper.Map<InternalWorksDetailViewModel>(response);

            if (model == null)
            {
                model = new InternalWorksDetailViewModel();
            }

            model.ElementNo = elementNo;
            model.ReturnUrl = returnUrl;

            return View(model);
        }


        [HttpPost("AddInternalWallWorks/{Id?}")]
        public async Task<IActionResult> AddInternalWallWorks(InternalWorksDetailViewModel viewModel, ESubmitAction submitAction)
        {
            var validator = new InternalWorksDetailViewModelValidator();
            var validationResult = await validator.ValidateAsync(viewModel);

            if (!validationResult.IsValid)
            {
                validationResult.AddToModelState(ModelState, String.Empty);
                return View("AddInternalWallWorks", viewModel);
            }

            var request = _mapper.Map<SetAddInternalWallWorksRequest>(viewModel);
            await _sender.Send(request);

            if (viewModel.ReturnUrl is not null)
            {
                return RedirectToAction("InternalWallWorks", "FireRiskAppraisal", new { Area = "FireRiskAppraisal", returnUrl = viewModel.ReturnUrl });
            }            

            return RedirectToAction("InternalWallWorks", "FireRiskAppraisal", new { Area = "FireRiskAppraisal" });
        }

        #endregion

        #region Internal Walls Works

        [HttpGet(nameof(InternalWallWorks))]
        public async Task<IActionResult> InternalWallWorks(string returnUrl)
        {
            var response = await _sender.Send(GetInternalWallWorksRequest.Request);
            var wallWorks = _mapper.Map<List<GetInternalWorksViewModel.InternalWallWorks>>(response);

            var outputModel = new GetInternalWorksViewModel()
            {
                WallWorks = wallWorks,
                ReturnUrl = returnUrl
            };
            return View(outputModel);
        }

        [HttpPost(nameof(SetInternalWallWorks))]
        public IActionResult SetInternalWallWorks(GetInternalWorksViewModel viewModel)
        {
            if (viewModel.WallWorks?.Count > 0)
            {
                if (viewModel.ReturnUrl is not null)
                {
                    return SafeRedirectToAction(viewModel.ReturnUrl, "FireRiskAppraisal", new { area = "FireRiskAppraisal", returnUrl = viewModel.ReturnUrl });    
                }

                return RedirectToAction(nameof(RecommendedWorksStart), "FireRiskAppraisal", new { area = "FireRiskAppraisal" });   
            }

            ModelState.AddModelError("WallWorks", "Enter at least one internal wall element");

            return View("InternalWallWorks", viewModel);
        }

        [HttpGet("DeleteInternalWallWorks/{Id}")]
        public async Task<IActionResult> DeleteInternalWallWorks(Guid Id, string returnUrl)
        {
            var request = new SetDeleteInternalWallWorksRequest
            {
                Id = Id
            };
            await _sender.Send(request);

            if (returnUrl is not null)
            {
                return RedirectToAction("InternalWallWorks", "FireRiskAppraisal", new { Area = "FireRiskAppraisal", returnUrl = returnUrl });
            }

            return RedirectToAction("InternalWallWorks", "FireRiskAppraisal", new { Area = "FireRiskAppraisal" });
        }

        #endregion

        #region Check Your Answers
        [HttpGet(nameof(CheckYourAnswers))]
        public async Task<IActionResult> CheckYourAnswers()
        {
            var response = await _sender.Send(GetCheckYourAnswersRequest.Request);

            return View(_mapper.Map<CheckYourAnswersViewModel>(response));
        }

        [HttpPost(nameof(CheckYourAnswers))]
        public async Task<IActionResult> CheckYourAnswers(CheckYourAnswersViewModel viewModel)
        {
            await _sender.Send(SetCheckYourAnswersRequest.Request);
            return RedirectToAction("Index", "TaskList", new { Area = "Application" });
        }

        #endregion

        #region "Recommended Works"

        [HttpGet("RecommendedWorks")]
        public async Task<IActionResult> RecommendedWorks(string returnUrl)
        {
            var response = await _sender.Send(GetRecommendedWorksRequest.Request);
            var model = _mapper.Map<GetRecommendedWorksViewModel>(response);

            model.ReturnUrl = returnUrl;

            return View(model);
        }

        [HttpPost(nameof(RecommendedWorks))]
        public async Task<IActionResult> RecommendedWorks(GetRecommendedWorksViewModel viewModel, ESubmitAction submitAction)
        {
            var validator = new GetRecommendedWorksViewModelValidator();

            var validationResult = await validator.ValidateAsync(viewModel);

            if (!validationResult.IsValid)
            {
                ModelState.Clear();
                validationResult.AddToModelState(ModelState, String.Empty);
                return View("RecommendedWorks", viewModel);
            }
            var request = _mapper.Map<SetRecommendedWorksRequest>(viewModel);
            await _sender.Send(request);

            var action = nameof(CheckYourAnswers);
            action = viewModel.ReturnUrl is null ? action : viewModel.ReturnUrl;

            return submitAction == ESubmitAction.Continue 
                ? SafeRedirectToAction(action, "FireRiskAppraisal", new { area = "FireRiskAppraisal" })
                : RedirectToAction("Index", "TaskList", new { Area = "Application" });            
        }

        #endregion

        #region "Report Details"

        [HttpGet("ReportDetails")]
        public async Task<IActionResult> ReportDetails(string returnUrl)
        {
            var response = await _sender.Send(GetReportDetailsRequest.Request);
            var model = _mapper.Map<GetReportDetailsViewModel>(response);
            model.ReturnUrl = returnUrl;

            return View(model);
        }

        [HttpPost(nameof(ReportDetails))]
        public async Task<IActionResult> ReportDetails(GetReportDetailsViewModel viewModel, ESubmitAction submitAction)
        {
            var validator = new GetReportDetailsViewModelValidator();

            var validationResult = await validator.ValidateAsync(viewModel);

            if (!validationResult.IsValid)
            {
                validationResult.AddToModelState(ModelState, String.Empty);
                return View("ReportDetails", viewModel);
            }
            var request = _mapper.Map<SetReportDetailsRequest>(viewModel);
            await _sender.Send(request);

            var action = nameof(InterimMeasures);
            action = viewModel.ReturnUrl is null ? action : viewModel.ReturnUrl;

            return submitAction == ESubmitAction.Continue 
                                ? SafeRedirectToAction(action, "FireRiskAppraisal", new { area = "FireRiskAppraisal" })
                                : RedirectToAction("Index", "TaskList", new { Area = "Application" });
        }

        #endregion

        #region "Recommended Works Start"

        [HttpGet(nameof(RecommendedWorksStart))]
        public async Task<IActionResult> RecommendedWorksStart()
        {
            var response = await _sender.Send(new GetRecommendedWorksStartRequest());

            var model = new RecommendedWorksStartViewModel
            {
                InternalWorksRequired = response.InternalWorksRequired
            };

            return View(model);
        }

        #endregion

        #region Works To Cladding Systems

        [HttpGet(nameof(WorksToCladdingSystems))]
        public async Task<IActionResult> WorksToCladdingSystems(string returnUrl)
        {
            var response = await _sender.Send(GetWorksToCladdingSystemsRequest.Request);
            var list = _mapper.Map<IEnumerable<SelectedWorksToCladdingSystemsViewModel>>(response);

            var vm = new WorksToCladdingSystemsViewModel
            {
                SelectedWorksToCladdingSystem = list,
                ReturnUrl = returnUrl
            };
            return View(vm);
        }

        [HttpPost(nameof(SetWorksToCladdingSystems))]
        public IActionResult SetWorksToCladdingSystems(WorksToCladdingSystemsViewModel viewModel)
        {
            if (viewModel.SelectedWorksToCladdingSystem != null && viewModel.SelectedWorksToCladdingSystem.ToList().Count > 0)
            {
                var action = nameof(WorksToCladdingCladdingArea);
                action = viewModel.ReturnUrl is null ? action : viewModel.ReturnUrl;
             
                return SafeRedirectToAction(action, "FireRiskAppraisal", new { area = "FireRiskAppraisal" });
            }

            if (viewModel.ReturnUrl is not null)
            {
                return SafeRedirectToAction(viewModel.ReturnUrl, "FireRiskAppraisal", new { area = "FireRiskAppraisal" });
            }

            ModelState.AddModelError("SelectedWorksToCladdingSystem", "Enter at least one cladding systems element");
            
            return View("WorksToCladdingSystems", viewModel);
        }

        [HttpGet(nameof(CladdingSystem))]
        public async Task<IActionResult> CladdingSystem(Guid? fireRiskCladdingSystemsId)
        {
            var request = new GetCladdingSystemRequest
            {
                FireRiskCladdingSystemsId = fireRiskCladdingSystemsId
            };

            var response = await _sender.Send(request);
            var claddingSystem = _mapper.Map<CladdingSystemViewModel>(response);

            return View(claddingSystem);
        }

        [HttpPost(nameof(CladdingSystem))]
        public async Task<IActionResult> CladdingSystem(CladdingSystemViewModel viewModel)
        {
            var validator = new CladdingSystemViewModelValidator();
            var validationResult = await validator.ValidateAsync(viewModel);

            if (!validationResult.IsValid)
            {
                var getCladdingRequest = new GetCladdingSystemRequest();
                var getCladdingResponse = await _sender.Send(getCladdingRequest);
                var claddingSystem = _mapper.Map<CladdingSystemViewModel>(getCladdingResponse);

                viewModel.CladdingTypes = claddingSystem.CladdingTypes;
                viewModel.InsulationTypes = claddingSystem.InsulationTypes;
                viewModel.CladdingManufacturers = claddingSystem.CladdingManufacturers;
                viewModel.InsulationManufacturers = claddingSystem.InsulationManufacturers;

                validationResult.AddToModelState(ModelState, string.Empty);
                return View(viewModel);
            }

            var request = _mapper.Map<SetCladdingSystemRequest>(viewModel);
            await _sender.Send(request);

            return RedirectToAction("WorksToCladdingSystems", "FireRiskAppraisal", new { Area = "FireRiskAppraisal" });
        }

        [HttpGet(nameof(RemoveCladdingSystem))]
        public async Task<IActionResult> RemoveCladdingSystem(Guid fireRiskCladdingSystemsId)
        {
            var request = new DeleteCladdingSystemRequest
            {
                FireRiskCladdingSystemsId = fireRiskCladdingSystemsId
            };
            await _sender.Send(request);

            return RedirectToAction("WorksToCladdingSystems", "FireRiskAppraisal", new { Area = "FireRiskAppraisal" });
        }

        [HttpGet(nameof(WorksToCladdingCladdingArea))]
        public async Task<IActionResult> WorksToCladdingCladdingArea(string returnUrl)
        {
            var request = new GetTotalCladdingAreaRequest();

            var response = await _sender.Send(request);

            var claddingArea = _mapper.Map<WorksToCladdingCladdingAreaViewModel>(response);
            if (claddingArea is null) { claddingArea = new WorksToCladdingCladdingAreaViewModel(); }                             
            
            claddingArea.ReturnUrl = returnUrl;

            return View(claddingArea);
        }

        [HttpPost(nameof(WorksToCladdingCladdingArea))]
        public async Task<IActionResult> WorksToCladdingCladdingArea(WorksToCladdingCladdingAreaViewModel viewModel)
        {
            var validator = new WorksToCladdingCladdingAreaViewModelValidator();
            var validationResult = await validator.ValidateAsync(viewModel);

            if (!validationResult.IsValid)
            {
                var request = new GetTotalCladdingAreaRequest();

                var response = await _sender.Send(request);
                
                var claddingArea = _mapper.Map<WorksToCladdingCladdingAreaViewModel>(response);
                if (claddingArea is null) { claddingArea = new WorksToCladdingCladdingAreaViewModel(); }                 

                validationResult.AddToModelState(ModelState, string.Empty);
                return View(claddingArea);
            }

            var req = new SetCladdingAreaRequest { RecommendedCladdingArea = viewModel.RecommendedTotalAreaCladding };
            await _sender.Send(req);

            var action = nameof(ExternalWorksRequired);
            action = viewModel.ReturnUrl is null ? action : viewModel.ReturnUrl;
            
            return SafeRedirectToAction(action, "FireRiskAppraisal", new { area = "FireRiskAppraisal" });
        }
        #endregion

        #region Summary Guidance

        [HttpGet(nameof(Guidance))]
        public IActionResult Guidance()
        {
            return View();
        }

        #endregion

        #region Summary Guidance

        [HttpGet(nameof(GuidanceNotOnPanel))]
        public IActionResult GuidanceNotOnPanel()
        {
            return View();
        }

        #endregion

        #region Interim Measures

        [HttpGet("InterimMeasures")]
        public async Task<IActionResult> InterimMeasures(string returnUrl)
        {
            var response = await _sender.Send(GetInterimMeasuresRequest.Request);
            var model = _mapper.Map<GetInterimMeasuresViewModel>(response);

            model.ReturnUrl = returnUrl;

            return View(model);
        }

        [HttpPost(nameof(InterimMeasures))]
        public async Task<IActionResult> InterimMeasures(GetInterimMeasuresViewModel viewModel, ESubmitAction submitAction)
        {
            var validator = new GetInterimMeasuresViewModelValidator();

            var validationResult = await validator.ValidateAsync(viewModel);

            if (!validationResult.IsValid)
            {
                validationResult.AddToModelState(ModelState, String.Empty);
                return View("InterimMeasures", viewModel);
            }

            var request = _mapper.Map<SetInterimMeasuresRequest>(viewModel);
            await _sender.Send(request);

            var action = nameof(WorksToCladdingSystems);
            action = viewModel.ReturnUrl is null ? action : viewModel.ReturnUrl;

            return submitAction == ESubmitAction.Continue 
                ? SafeRedirectToAction(action, "FireRiskAppraisal", new { area = "FireRiskAppraisal" })
                : RedirectToAction("Index", "TaskList", new { Area = "Application" });            
        }

        #endregion

        #region Fire Risk Assessor List PDF

        [AllowAnonymous]
        [HttpGet(nameof(FireRiskAssessorListPdf))]
        [ExcludeRouteRecording]
        public async Task<IActionResult> FireRiskAssessorListPdf()
        {
            var file = await _sender.Send(FireRiskAssessorListPdfRequest.Request);
            return File(file, "application/pdf", "Fire Risk Assessor List.pdf");
        }
        #endregion
    }
}
