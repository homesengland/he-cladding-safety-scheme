using AutoMapper;
using FluentValidation.AspNetCore;
using HE.Remediation.Core.Enums;
using HE.Remediation.Core.Exceptions;
using HE.Remediation.Core.UseCase.Areas.Location.PostCode;
using HE.Remediation.Core.UseCase.Areas.ProgressReporting;
using HE.Remediation.Core.UseCase.Areas.ProgressReporting.AddRole.GetAddRole;
using HE.Remediation.Core.UseCase.Areas.ProgressReporting.AddRole.SetAddRole;
using HE.Remediation.Core.UseCase.Areas.ProgressReporting.AppliedForPlanningPermission.GetAppliedForPlanningPermission;
using HE.Remediation.Core.UseCase.Areas.ProgressReporting.AppliedForPlanningPermission.SetAppliedForPlanningPermission;
using HE.Remediation.Core.UseCase.Areas.ProgressReporting.AppointedOtherMembers.GetAppointedOtherMembers;
using HE.Remediation.Core.UseCase.Areas.ProgressReporting.AppointedOtherMembers.SetAppointedOtherMembers;
using HE.Remediation.Core.UseCase.Areas.ProgressReporting.BuildingControl.GetBuildingControlRequired;
using HE.Remediation.Core.UseCase.Areas.ProgressReporting.BuildingControl.UpdateBuildingControlRequired;
using HE.Remediation.Core.UseCase.Areas.ProgressReporting.ChangeAnswers;
using HE.Remediation.Core.UseCase.Areas.ProgressReporting.Evidence.DeleteEvidence;
using HE.Remediation.Core.UseCase.Areas.ProgressReporting.Evidence.GetEvidence;
using HE.Remediation.Core.UseCase.Areas.ProgressReporting.Evidence.SetEvidence;
using HE.Remediation.Core.UseCase.Areas.ProgressReporting.FinalCheckYourAnswers.GetFinalCheckYourAnswers;
using HE.Remediation.Core.UseCase.Areas.ProgressReporting.FinalCheckYourAnswers.SetFinalCheckYourAnswers;
using HE.Remediation.Core.UseCase.Areas.ProgressReporting.HaveAnyAnswersChanged;
using HE.Remediation.Core.UseCase.Areas.ProgressReporting.InformedLeaseholder.GetInformedLeaseholder;
using HE.Remediation.Core.UseCase.Areas.ProgressReporting.InformedLeaseholder.SetInformedLeaseholder;
using HE.Remediation.Core.UseCase.Areas.ProgressReporting.LeaseholderInformedLast;
using HE.Remediation.Core.UseCase.Areas.ProgressReporting.PlanningPermissionDetails.GetPlanningPermissionDetails;
using HE.Remediation.Core.UseCase.Areas.ProgressReporting.PlanningPermissionDetails.SetPlanningPermissionDetails;
using HE.Remediation.Core.UseCase.Areas.ProgressReporting.PlanningPermissionPlannedSubmitDate.GetPlanningPermissionPlannedSubmitDate;
using HE.Remediation.Core.UseCase.Areas.ProgressReporting.PlanningPermissionPlannedSubmitDate.SetPlanningPermissionPlannedSubmitDate;
using HE.Remediation.Core.UseCase.Areas.ProgressReporting.ProgressReportVersion;
using HE.Remediation.Core.UseCase.Areas.ProgressReporting.ProgressSupport.GetProgressSupport;
using HE.Remediation.Core.UseCase.Areas.ProgressReporting.ProgressSupport.SetProgressSupport;
using HE.Remediation.Core.UseCase.Areas.ProgressReporting.ProjectTeam.GetProjectTeam;
using HE.Remediation.Core.UseCase.Areas.ProgressReporting.ReasonNeedsSupport.GetReasonNeedsSupport;
using HE.Remediation.Core.UseCase.Areas.ProgressReporting.ReasonNeedsSupport.SetReasonNeedsSupport;
using HE.Remediation.Core.UseCase.Areas.ProgressReporting.ReasonNoOtherMembers.GetReasonNoOtherMembers;
using HE.Remediation.Core.UseCase.Areas.ProgressReporting.ReasonNoOtherMembers.SetReasonNoOtherMembers;
using HE.Remediation.Core.UseCase.Areas.ProgressReporting.ReasonPlanningNotApplied.GetReasonPlanningNotApplied;
using HE.Remediation.Core.UseCase.Areas.ProgressReporting.ReasonPlanningNotApplied.SetReasonPlanningNotApplied;
using HE.Remediation.Core.UseCase.Areas.ProgressReporting.ReasonQuotesNotSought.GetReasonQuotesNotSought;
using HE.Remediation.Core.UseCase.Areas.ProgressReporting.ReasonQuotesNotSought.SetReasonQuotesNotSought;
using HE.Remediation.Core.UseCase.Areas.ProgressReporting.SecondaryCheckYourAnswers.GetCheckYourAnswers;
using HE.Remediation.Core.UseCase.Areas.ProgressReporting.SecondaryCheckYourAnswers.SetCheckYourAnswers;
using HE.Remediation.Core.UseCase.Areas.ProgressReporting.SelectedReport.SetSelectReport;
using HE.Remediation.Core.UseCase.Areas.ProgressReporting.SoughtQuotes.GetSoughtQuotes;
using HE.Remediation.Core.UseCase.Areas.ProgressReporting.SoughtQuotes.SetSoughtQuotes;
using HE.Remediation.Core.UseCase.Areas.ProgressReporting.Start;
using HE.Remediation.Core.UseCase.Areas.ProgressReporting.Submitted.GetSubmitted;
using HE.Remediation.Core.UseCase.Areas.ProgressReporting.SummariseProgress;
using HE.Remediation.Core.UseCase.Areas.ProgressReporting.TeamMember.DeleteTeamMember;
using HE.Remediation.Core.UseCase.Areas.ProgressReporting.TeamMember.GetRemoveTeamMember;
using HE.Remediation.Core.UseCase.Areas.ProgressReporting.TeamMember.GetTeamMember;
using HE.Remediation.Core.UseCase.Areas.ProgressReporting.TeamMember.GetTeamMemberCheckYourAnswers;
using HE.Remediation.Core.UseCase.Areas.ProgressReporting.TeamMember.UpdateTeamMember;
using HE.Remediation.Core.UseCase.Areas.ProgressReporting.WhenSubmit.GetWhenSubmit;
using HE.Remediation.Core.UseCase.Areas.ProgressReporting.WhenSubmit.SetWhenSubmit;
using HE.Remediation.Core.UseCase.Areas.ProgressReporting.WorksRequirePermission.GetWorksRequirePermission;
using HE.Remediation.Core.UseCase.Areas.ProgressReporting.WorksRequirePermission.SetWorksRequirePermission;
using HE.Remediation.WebApp.Attributes.Routing;
using HE.Remediation.WebApp.Constants;
using HE.Remediation.WebApp.ViewModels.Location;
using HE.Remediation.WebApp.ViewModels.ProgressReporting;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using HE.Remediation.Core.UseCase.Areas.ProgressReporting.BuildingHasSafetyRegulatorRegistrationCode.GetBuildingHasSafetyRegulatorRegistrationCode;
using HE.Remediation.Core.UseCase.Areas.ProgressReporting.BuildingHasSafetyRegulatorRegistrationCode.SetBuildingHasSafetyRegulatorRegistrationCode;
using HE.Remediation.Core.UseCase.Areas.ProgressReporting.BuildingSafetyRegulatorRegistrationCode.GetBuildingSafetyRegulatorRegistrationCode;
using HE.Remediation.Core.UseCase.Areas.ProgressReporting.BuildingSafetyRegulatorRegistrationCode.SetBuildingSafetyRegulatorRegistrationCode;
using HE.Remediation.Core.UseCase.Areas.ProgressReporting.ExistingTeamMember;
using HE.Remediation.Core.UseCase.Areas.ProgressReporting.WhenStartOnSite.GetWhenStartOnSite;
using HE.Remediation.Core.UseCase.Areas.ProgressReporting.WhenStartOnSite.SetWhenStartOnSite;
using HE.Remediation.Core.UseCase.Areas.ProgressReporting.ProjectPlanMilestones.GetHasProjectPlanMilestones;
using HE.Remediation.Core.UseCase.Areas.ProgressReporting.ProjectPlanMilestones.SetProjectPlanMilestones;

namespace HE.Remediation.WebApp.Areas.ProgressReporting.Controllers;

[Area("ProgressReporting")]
[Route("ProgressReporting")]
public class ProgressReportingController : StartController
{
    private readonly ISender _sender;
    private readonly IMapper _mapper;

    public ProgressReportingController(ISender sender, IMapper mapper) : base(sender)
    {
        _sender = sender;
        _mapper = mapper;
    }

    protected override IActionResult DefaultStart => RedirectToAction("GetProgressReportVersion", "ProgressReporting", new { Area = "ProgressReporting" });

    [HttpGet("Start/{id:guid}")]
    public async Task<IActionResult> Start(Guid id)
    {
        var request = new SetSelectReportRequest
        {
            Id = id
        };

        await _sender.Send(request);

        var area = await GetArea();

        var areaData = new Dictionary<string, Guid>
        {
            { "progressReportId", id }
        };

        var areaDataJson = JsonSerializer.Serialize(areaData);
        TempData["AreaDataJson"] = areaDataJson;

        var isSubmitted = await _sender.Send(new CheckSubmittedStatusRequest
        {
            ProgressReportId = id
        });

        if (isSubmitted)
        {
            return RedirectToAction("ProgressReportDetails", "ProgressReportingDetails", new { Area = "ProgressReportingDetails", ProgressReportId = id });
        }

        if (area is not null)
        {
            var dict = !string.IsNullOrEmpty(area.AreaDataJson)
                ? JsonSerializer.Deserialize<Dictionary<string, Guid>>(area.AreaDataJson) ?? new Dictionary<string, Guid>()
                : new Dictionary<string, Guid>();

            if (dict.TryGetValue("progressReportId", out var value) && value != id)
            {
                return DefaultStart;
            }
        }

        return area is not null
            ? BuildRedirectAction(area)
            : DefaultStart;
    }

    #region Get Progress Report Version

    [HttpGet(nameof(GetProgressReportVersion))]
    [ExcludeRouteRecording]
    public async Task<IActionResult> GetProgressReportVersion()
    {
        var versionNumber = await _sender.Send(GetProgressReportVersionRequest.Request);
        var actionName = versionNumber == 1
            ? nameof(InformedLeaseholder)
            : nameof(ProjectTeamOverview);
        return SafeRedirectToAction(actionName, "ProgressReporting", new { Area = "ProgressReporting" });

    }

    #endregion

    #region Informed Leaseholder

    [HttpGet(nameof(InformedLeaseholder))]
    public async Task<IActionResult> InformedLeaseholder(string returnUrl)
    {
        var response = await _sender.Send(GetInformedLeaseholderRequest.Request);
        var viewModel = _mapper.Map<InformedLeaseholderViewModel>(response);

        viewModel.ReturnUrl = returnUrl;

        return View(viewModel);
    }

    [HttpPost(nameof(InformedLeaseholder))]
    public async Task<IActionResult> InformedLeaseholder(InformedLeaseholderViewModel viewModel, ESubmitAction submitAction)
    {
        var validator = new InformedLeaseholderViewModelValidator();

        var validationResult = await validator.ValidateAsync(viewModel);
        if (validationResult.IsValid)
        {
            var request = _mapper.Map<SetInformedLeaseholderRequest>(viewModel);

            await _sender.Send(request);

            if (viewModel.ReturnUrl is not null)
            {
                return SafeRedirectToAction(viewModel.ReturnUrl, "ProgressReporting", new { Area = "ProgressReporting" });
            }

            if (viewModel.SubmitAction == ESubmitAction.Continue)
            {
                if (request.LeaseholdersInformed == true)
                {
                    return RedirectToAction("UploadEvidence", "ProgressReporting", new { Area = "ProgressReporting" });
                }

                return !viewModel.HasVisitedCheckYourAnswers
                    ? RedirectToAction("AppointedOtherMembers", "ProgressReporting", new { Area = "ProgressReporting" })
                    : RedirectToAction("FinalCheckYourAnswers", "ProgressReporting", new { Area = "ProgressReporting" });
            }

            return RedirectToAction("Index", "StageDiagram", new { area = "Application" });
        }

        validationResult.AddToModelState(ModelState, String.Empty);

        return View("InformedLeaseholder", viewModel);
    }

    #endregion

    #region Upload Evidence

    [HttpGet(nameof(UploadEvidence))]
    public async Task<IActionResult> UploadEvidence()
    {
        var response = await _sender.Send(GetEvidenceRequest.Request);
        var viewModel = _mapper.Map<UploadEvidenceViewModel>(response);

        return View(viewModel);
    }

    [HttpPost(nameof(UploadEvidence))]
    [RequestSizeLimit(FileUploadConstants.MaxRequestSizeBytes)]
    public async Task<IActionResult> UploadEvidence(UploadEvidenceViewModel viewModel, ESubmitAction submitAction)
    {
        var validator = new UploadEvidenceViewModelValidator();
        var validationResult = await validator.ValidateAsync(viewModel);
        if (!validationResult.IsValid)
        {
            validationResult.AddToModelState(ModelState, string.Empty);
            return View(viewModel);
        }

        try
        {
            var request = _mapper.Map<SetEvidenceRequest>(viewModel);
            await _sender.Send(request);
            if (viewModel.SubmitAction == ESubmitAction.Upload)
            {
                return RedirectToAction("UploadEvidence", "ProgressReporting", new { Area = "ProgressReporting" });
            }
        }
        catch (InvalidFileException ex)
        {
            if (ex.Errors is not null)
            {
                foreach (var error in ex.Errors)
                {
                    ModelState.AddModelError(error.Key, error.Value);
                }
            }
            else
            {
                ModelState.AddModelError(nameof(viewModel.File), ex.Message);
            }

            return View(viewModel);

        }

        if (viewModel.SubmitAction == ESubmitAction.Exit)
        {
            return RedirectToAction("Index", "StageDiagram", new { area = "Application" });
        }

        return !viewModel.HasVisitedCheckYourAnswers
            ? RedirectToAction("AppointedOtherMembers", "ProgressReporting", new { area = "ProgressReporting" })
            : RedirectToAction("FinalCheckYourAnswers", "ProgressReporting", new { Area = "ProgressReporting" });
    }

    [HttpGet(nameof(UploadEvidence) + "/Delete")]
    public async Task<IActionResult> UploadEvidenceDelete([FromQuery] Guid fileId)// DeleteEvidenceRequest request)
    {
        await _sender.Send(new DeleteEvidenceRequest
        {
            FileId = fileId
        });
        return RedirectToAction("UploadEvidence", "ProgressReporting", new { Area = "ProgressReporting" });
    }
    #endregion

    #region Have you appointed other team members

    [HttpGet(nameof(AppointedOtherMembers))]
    public async Task<IActionResult> AppointedOtherMembers(string returnUrl)
    {
        var response = await _sender.Send(GetAppointedOtherMembersRequest.Request);
        var viewModel = _mapper.Map<AppointedOtherMembersViewModel>(response);

        viewModel.ReturnUrl = returnUrl;
        return View(viewModel);
    }

    [HttpPost(nameof(AppointedOtherMembers))]
    public async Task<IActionResult> AppointedOtherMembers(AppointedOtherMembersViewModel viewModel, ESubmitAction submitAction)
    {
        var validator = new AppointedOtherMembersViewModelValidator();

        var validationResult = await validator.ValidateAsync(viewModel);
        if (!validationResult.IsValid)
        {
            validationResult.AddToModelState(ModelState, string.Empty);
            return View(viewModel);
        }

        var request = _mapper.Map<SetAppointedOtherMembersRequest>(viewModel);

        await _sender.Send(request);

        if (viewModel.ReturnUrl is not null)
        {
            return SafeRedirectToAction(viewModel.ReturnUrl, "ProgressReporting", new { Area = "ProgressReporting" });
        }

        if (viewModel.SubmitAction == ESubmitAction.Exit)
        {
            return RedirectToAction("Index", "StageDiagram", new { area = "Application" });
        }

        return request.OtherMembersAppointed == true
            ? RedirectToAction("ProjectTeam", "ProgressReporting", new { Area = "ProgressReporting" })
            : RedirectToAction("ReasonNoOtherMembers", "ProgressReporting", new { Area = "ProgressReporting" });
    }

    #endregion

    #region Reason haven't appointed other team members designer

    [HttpGet(nameof(ReasonNoOtherMembers))]
    public async Task<IActionResult> ReasonNoOtherMembers(string returnUrl)
    {
        var response = await _sender.Send(GetReasonNoOtherMembersRequest.Request);
        var viewModel = _mapper.Map<ReasonNoOtherMembersViewModel>(response);

        viewModel.ReturnUrl = returnUrl;
        return View(viewModel);
    }

    [HttpPost(nameof(ReasonNoOtherMembers))]
    public async Task<IActionResult> ReasonNoOtherMembers(ReasonNoOtherMembersViewModel viewModel, ESubmitAction submitAction)
    {
        var validator = new ReasonNoOtherMembersViewModelValidator();

        var validationResult = await validator.ValidateAsync(viewModel);
        if (validationResult.IsValid)
        {
            var request = _mapper.Map<SetReasonNoOtherMembersRequest>(viewModel);

            await _sender.Send(request);

            if (viewModel.ReturnUrl is not null)
            {
                return SafeRedirectToAction(viewModel.ReturnUrl, "ProgressReporting", new { Area = "ProgressReporting" });
            }

            if (viewModel.SubmitAction == ESubmitAction.Continue)
            {
	            return !viewModel.HasVisitedCheckYourAnswers
		            ? RedirectToAction("IntentToProceed", "ProgressReporting", new { Area = "ProgressReporting" })
		            : RedirectToAction("FinalCheckYourAnswers", "ProgressReporting", new { Area = "ProgressReporting" });
            }

            return RedirectToAction("Index", "StageDiagram", new { area = "Application" });
        }

        validationResult.AddToModelState(ModelState, String.Empty);

        return View(viewModel);
    }

    #endregion

    #region Project Team

    [HttpGet(nameof(ProjectTeam))]
    public async Task<IActionResult> ProjectTeam()
    {
        var response = await _sender.Send(GetProjectTeamRequest.Request);
        var viewModel = _mapper.Map<ProjectTeamViewModel>(response);

        viewModel.ReturnUrl = string.Empty;
        return View(viewModel);
    }

    [HttpGet(nameof(ProjectTeamContinue))]
    public async Task<IActionResult> ProjectTeamContinue(CancellationToken cancellationToken)
    {
        var response = await _sender.Send(ProjectTeamContinueRequest.Request, cancellationToken);

        if (response.HasCertifyingOfficerRoles && !response.IsGcoComplete)
        {
            return RedirectToAction("HasGrantCertifyingOfficer", "ProgressReporting", new { Area = "ProgressReporting" });
        }

        if (response.Version > 1)
        {
            return RedirectToAction("HaveAnyAnswersChanged", "ProgressReporting", new { Area = "ProgressReporting" });
        }

        return response.HasVisitedCheckYourAnswers
            ? RedirectToAction("FinalCheckYourAnswers", "ProgressReporting", new { Area = "ProgressReporting" })
            : RedirectToAction("IntentToProceed", "ProgressReporting", new { Area = "ProgressReporting" });
    }

    #endregion

    #region Add role

    [ExcludeRouteRecording]
    [HttpGet(nameof(AddRole))]
    public async Task<IActionResult> AddRole()
    {
        var response = await _sender.Send(GetAddRoleRequest.Request);
        var viewModel = _mapper.Map<AddRoleViewModel>(response);

        viewModel.ReturnUrl = string.Empty;
        return View(viewModel);
    }

    [HttpPost(nameof(AddRole))]
    public async Task<IActionResult> AddRole(AddRoleViewModel viewModel, ESubmitAction submitAction)
    {
        var validator = new AddRoleViewModelValidator();

        var validationResult = await validator.ValidateAsync(viewModel);
        if (validationResult.IsValid)
        {
            var request = _mapper.Map<SetAddRoleRequest>(viewModel);
            var response = await _sender.Send(request);

            if (submitAction == ESubmitAction.Continue)
            {
                return response.CanChooseExistingMembers ? RedirectToAction("ExistingTeamMember", "ProgressReporting", new { Area = "ProgressReporting", TeamRole = (int)viewModel.TeamRole })
                      : RedirectToAction("TeamMember", new { TeamRole = (int)viewModel.TeamRole.Value });
            }

            return RedirectToAction("Index", "StageDiagram", new { area = "Application" });
        }

        validationResult.AddToModelState(ModelState, String.Empty);

        return View(viewModel);
    }

    #endregion

    #region Existing Member

    [ExcludeRouteRecording]
    [HttpGet("ExistingTeamMember/{teamRole:int}")]
    public async Task<IActionResult> ExistingTeamMember([FromRoute] GetExistingTeamMemberRequest request)
    {
        var response = await _sender.Send(request);
        var viewModel = _mapper.Map<ExistingTeamMemberViewModel>(response);

        viewModel.ReturnUrl = string.Empty;
        return View(viewModel);
    }

    [HttpPost("ExistingTeamMember/{teamRole:int}")]
    public async Task<IActionResult> ExistingTeamMember(ExistingTeamMemberViewModel viewModel, ESubmitAction submitAction)
    {
        var validator = new ExistingTeamMemberViewModelValidator();
        var validationResult = await validator.ValidateAsync(viewModel);

        if (!validationResult.IsValid)
        {
            validationResult.AddToModelState(ModelState, string.Empty);
            return View(viewModel);
        }

        return submitAction == ESubmitAction.Continue && viewModel.TeamRole.HasValue
            ? RedirectToAction("TeamMember",
                new
                {
                    TeamRole = (int)viewModel.TeamRole.Value,
                    Selected = viewModel.SameAsPrevious.HasValue && viewModel.SameAsPrevious.Value ? viewModel.SelectedPreviousTeamMember : null
                })
            : RedirectToAction("Index", "StageDiagram", new { area = "Application" });
    }

    #endregion

    #region Add Team Member
    [ExcludeRouteRecording]
    [HttpGet("TeamMember/{teamRole:int}/{teamMemberId:guid?}")]
    public async Task<IActionResult> TeamMember(
        GetTeamMemberRequest request,
        [FromQuery] string returnUrl = null)
    {
        try
        {
            var response = await _sender.Send(request);
            var model = _mapper.Map<TeamMemberViewModel>(response);

            model.ReturnUrl = returnUrl;

            return View(model);
        }
        catch (EntityNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpPost(nameof(TeamMember))]
    public async Task<IActionResult> TeamMember(TeamMemberViewModel model)
    {
        var validator = new TeamMemberViewModelValidator();
        var validationResult = await validator.ValidateAsync(model);

        if (!validationResult.IsValid)
        {
            validationResult.AddToModelState(ModelState, string.Empty);
            return View(model);
        }

        var request = _mapper.Map<UpdateTeamMemberRequest>(model);
        var teamMemberId = await _sender.Send(request);

        if (model.SubmitAction == ESubmitAction.Exit)
        {
            return RedirectToAction("Index", "StageDiagram", new { area = "Application" });
        }

        return !string.IsNullOrEmpty(model.ReturnUrl)
            ? SafeRedirectToAction(model.ReturnUrl, "ProgressReporting", new { Area = "ProgressReporting" })
            : RedirectToAction("TeamMemberCheckYourAnswers", "ProgressReporting", new { Area = "ProgressReporting", TeamMemberId = teamMemberId });
    }

    #endregion

    #region Add Team Member Check Your Answers

    [ExcludeRouteRecording]
    [HttpGet("TeamMember/CheckYourAnswers/{teamMemberId:guid}")]
    public async Task<IActionResult> TeamMemberCheckYourAnswers([FromRoute] Guid teamMemberId)
    {
        var response = await _sender.Send(new GetTeamMemberCheckYourAnswersRequest
        {
            TeamMemberId = teamMemberId
        });

        var model = _mapper.Map<TeamMemberCheckYourAnswersViewModel>(response);

        return View(model);
    }

    #endregion

    #region Remove Member
    [ExcludeRouteRecording]
    [HttpGet(nameof(RemoveMember))]
    public async Task<IActionResult> RemoveMember(Guid teamMemberId)
    {
        var response = await _sender.Send(new GetRemoveTeamMemberRequest { TeamMemberId = teamMemberId });
        var viewModel = _mapper.Map<RemoveMemberViewModel>(response);

        viewModel.ReturnUrl = string.Empty;
        return View(viewModel);
    }

    [HttpPost(nameof(RemoveMember))]
    public async Task<IActionResult> RemoveMember(RemoveMemberViewModel viewModel, ESubmitAction submitAction)
    {
        var validator = new RemoveMemberViewModelValidator();

        var validationResult = await validator.ValidateAsync(viewModel);
        if (validationResult.IsValid)
        {
            if (viewModel.Confirm == true)
            {
                var request = _mapper.Map<DeleteTeamMemberRequest>(viewModel);
                await _sender.Send(request);
            }

            if (viewModel.ReturnUrl is not null)
            {
                return SafeRedirectToAction(viewModel.ReturnUrl, "ProgressReporting", new { Area = "ProgressReporting" });
            }

            if (viewModel.SubmitAction == ESubmitAction.Continue)
            {
                return RedirectToAction("ProjectTeam", "ProgressReporting", new { Area = "ProgressReporting" });
            }

            return RedirectToAction("Index", "StageDiagram", new { area = "Application" });
        }

        validationResult.AddToModelState(ModelState, String.Empty);

        return View(viewModel);
    }

    #endregion

    #region Have You Sought Quotes

    [HttpGet(nameof(SoughtQuotes))]
    public async Task<IActionResult> SoughtQuotes(string returnUrl)
    {
        var response = await _sender.Send(GetSoughtQuotesRequest.Request);
        var viewModel = _mapper.Map<SoughtQuotesViewModel>(response);

        viewModel.ReturnUrl = returnUrl;
        return View(viewModel);
    }

    [HttpPost(nameof(SoughtQuotes))]
    public async Task<IActionResult> SoughtQuotes(SoughtQuotesViewModel viewModel)
    {
        var validator = new SoughtQuotesViewModelValidator();

        var validationResult = await validator.ValidateAsync(viewModel);
        if (validationResult.IsValid)
        {
            var request = _mapper.Map<SetSoughtQuotesRequest>(viewModel);

            await _sender.Send(request);

            if (viewModel.ReturnUrl is not null)
            {
                return SafeRedirectToAction(viewModel.ReturnUrl, "ProgressReporting", new { Area = "ProgressReporting" });
            }

            if (viewModel.SubmitAction == ESubmitAction.Continue)
            {
                if (viewModel.Version > 1)
                {
                    return RedirectToAction("HaveAnyAnswersChanged", "ProgressReporting", new { Area = "ProgressReporting" });
                }

                if (request.QuotesSought == true)
                {
	                return !viewModel.HasVisitedCheckYourAnswers
		                ? RedirectToAction("WorksRequirePermission", "ProgressReporting", new { Area = "ProgressReporting" })
		                : RedirectToAction("FinalCheckYourAnswers", "ProgressReporting", new { Area = "ProgressReporting" });
                }

                return RedirectToAction("ReasonQuotesNotSought", "ProgressReporting", new { Area = "ProgressReporting" });
            }

            return RedirectToAction("Index", "StageDiagram", new { area = "Application" });
        }

        validationResult.AddToModelState(ModelState, String.Empty);

        return View(viewModel);
    }

    #endregion

    #region Reason haven't sought quotes or issued a tender?

    [HttpGet(nameof(ReasonQuotesNotSought))]
    public async Task<IActionResult> ReasonQuotesNotSought(string returnUrl)
    {
        var response = await _sender.Send(GetReasonQuotesNotSoughtRequest.Request);
        var viewModel = _mapper.Map<ReasonQuotesNotSoughtViewModel>(response);

        viewModel.ReturnUrl = returnUrl;
        return View(viewModel);
    }

    [HttpPost(nameof(ReasonQuotesNotSought))]
    public async Task<IActionResult> ReasonQuotesNotSought(ReasonQuotesNotSoughtViewModel viewModel, ESubmitAction submitAction)
    {
        var validator = new ReasonQuotesNotSoughtViewModelValidator();

        var validationResult = await validator.ValidateAsync(viewModel);
        if (validationResult.IsValid)
        {
            var request = _mapper.Map<SetReasonQuotesNotSoughtRequest>(viewModel);

            await _sender.Send(request);

            if (viewModel.SubmitAction == ESubmitAction.Exit)
            {
                return RedirectToAction("Index", "StageDiagram", new { area = "Application" });
            }

            if (viewModel.ReturnUrl is not null)
            {
                return SafeRedirectToAction(viewModel.ReturnUrl, "ProgressReporting", new { Area = "ProgressReporting" });
            }

            if (viewModel.SubmitAction == ESubmitAction.Continue)
            {
                if (viewModel.Version > 1)
                {
                    return RedirectToAction("HaveAnyAnswersChanged", "ProgressReporting", new { Area = "ProgressReporting" });
                }

                return !viewModel.HasVisitedCheckYourAnswers
	                ? RedirectToAction("WorksRequirePermission", "ProgressReporting", new { Area = "ProgressReporting" })
	                : RedirectToAction("FinalCheckYourAnswers", "ProgressReporting", new { Area = "ProgressReporting" });
            }
        }

        validationResult.AddToModelState(ModelState, String.Empty);

        return View(viewModel);
    }

    #endregion

    #region Do Works Require Permission

    [HttpGet(nameof(WorksRequirePermission))]
    public async Task<IActionResult> WorksRequirePermission(string returnUrl)
    {
        var response = await _sender.Send(GetWorksRequirePermissionRequest.Request);
        var viewModel = _mapper.Map<WorksRequirePermissionViewModel>(response);

        viewModel.ReturnUrl = returnUrl;
        return View(viewModel);
    }

    [HttpPost(nameof(WorksRequirePermission))]
    public async Task<IActionResult> WorksRequirePermission(WorksRequirePermissionViewModel viewModel, ESubmitAction submitAction)
    {
        var validator = new WorksRequirePermissionViewModelValidator();

        var validationResult = await validator.ValidateAsync(viewModel);
        if (validationResult.IsValid)
        {
            var request = _mapper.Map<SetWorksRequirePermissionRequest>(viewModel);

            await _sender.Send(request);

            if (viewModel.SubmitAction == ESubmitAction.Exit)
            {
                return RedirectToAction("Index", "StageDiagram", new { area = "Application" });
            }

            if (request.PermissionRequired == EYesNoNonBoolean.Yes)
            {
                return RedirectToAction("AppliedPlanning", "ProgressReporting", new { Area = "ProgressReporting" });
            }

            if (viewModel.Version > 1)
            {
                RedirectToAction("HaveAnyAnswersChanged", "ProgressReporting", new { Area = "ProgressReporting" });
            }

            return !viewModel.HasVisitedCheckYourAnswers
                ? RedirectToAction("RequireBuildingSafetyRegulatoryRegistrationCode", "ProgressReporting", new { Area = "ProgressReporting" })
                : RedirectToAction("FinalCheckYourAnswers", "ProgressReporting", new { Area = "ProgressReporting" });
        }

        validationResult.AddToModelState(ModelState, String.Empty);

        return View(viewModel);
    }

    #endregion    

    #region Have you applied for planning permission

    [HttpGet(nameof(AppliedPlanning))]
    public async Task<IActionResult> AppliedPlanning(string returnUrl)
    {
        var response = await _sender.Send(GetAppliedForPlanningPermissionRequest.Request);
        var viewModel = _mapper.Map<AppliedPlanningViewModel>(response);

        viewModel.ReturnUrl = returnUrl;
        return View(viewModel);
    }

    [HttpPost(nameof(AppliedPlanning))]
    public async Task<IActionResult> AppliedPlanning(AppliedPlanningViewModel viewModel)
    {
        var validator = new AppliedPlanningViewModelValidator();

        var validationResult = await validator.ValidateAsync(viewModel);
        if (validationResult.IsValid)
        {
            var request = _mapper.Map<SetAppliedForPlanningPermissionRequest>(viewModel);

            await _sender.Send(request);

            if (viewModel.SubmitAction == ESubmitAction.Exit)
            {
                return RedirectToAction("Index", "StageDiagram", new { area = "Application" });
            }

            if (viewModel.ReturnUrl is not null)
            {
                return SafeRedirectToAction(viewModel.ReturnUrl, "ProgressReporting", new { Area = "ProgressReporting" });
            }

            if (request.AppliedForPlanningPermission == true)
            {
                return RedirectToAction("PlanningPermissionDetails", "ProgressReporting", new { Area = "ProgressReporting" });
            }

            return RedirectToAction("PlanningPermissionPlannedSubmitDate", "ProgressReporting", new { Area = "ProgressReporting" });
        }

        validationResult.AddToModelState(ModelState, String.Empty);
        return View(viewModel);
    }

    #endregion

    #region Planning Permission Planned Submit Date

    [HttpGet(nameof(PlanningPermissionPlannedSubmitDate))]
    public async Task<IActionResult> PlanningPermissionPlannedSubmitDate()
    {
        var planningPermissionPlannedSubmitDateResponse = await _sender.Send(GetPlanningPermissionPlannedSubmitDateRequest.Request);

        var viewModel = _mapper.Map<PlanningPermissionPlannedSubmitDateViewModel>(planningPermissionPlannedSubmitDateResponse);

        return View(viewModel);
    }

    [HttpPost(nameof(PlanningPermissionPlannedSubmitDate))]
    public async Task<IActionResult> PlanningPermissionPlannedSubmitDate(PlanningPermissionPlannedSubmitDateViewModel viewModel)
    {
        var validator = new PlanningPermissionPlannedSubmitDateViewModelValidator();

        var validationResult = await validator.ValidateAsync(viewModel);

        if (validationResult.IsValid)
        {
            var request = _mapper.Map<SetPlanningPermissionPlannedSubmitDateRequest>(viewModel);

            await _sender.Send(request);

            if (viewModel.SubmitAction == ESubmitAction.Exit)
            {
                return RedirectToAction("Index", "StageDiagram", new { area = "Application" });
            }

            return RedirectToAction("ReasonPlanningNotApplied", "ProgressReporting", new { Area = "ProgressReporting" });
        }

        validationResult.AddToModelState(ModelState, String.Empty);
        return View(viewModel);
    }

    #endregion

    #region Planning Permission Details

    [HttpGet(nameof(PlanningPermissionDetails))]
    public async Task<IActionResult> PlanningPermissionDetails()
    {
        var planningPermissionDetailsResponse = await _sender.Send(GetPlanningPermissionDetailsRequest.Request);

        var viewModel = _mapper.Map<PlanningPermissionDetailsViewModel>(planningPermissionDetailsResponse);

        return View(viewModel);
    }

    [HttpPost(nameof(PlanningPermissionDetails))]
    public async Task<IActionResult> PlanningPermissionDetails(PlanningPermissionDetailsViewModel viewModel)
    {
        var validator = new PlanningPermissionDetailsViewModelValidator();

        var validationResult = await validator.ValidateAsync(viewModel);

        if (validationResult.IsValid)
        {
            var request = _mapper.Map<SetPlanningPermissionDetailsRequest>(viewModel);

            await _sender.Send(request);

            if (viewModel.SubmitAction == ESubmitAction.Exit)
            {
                return RedirectToAction("Index", "StageDiagram", new { area = "Application" });
            }

            if (viewModel.Version > 1)
            {
                return RedirectToAction("HaveAnyAnswersChanged", "ProgressReporting", new { Area = "ProgressReporting" });
            }

            if (viewModel.HasVisitedCheckYourAnswers)
            {
                return RedirectToAction("FinalCheckYourAnswers", "ProgressReporting", new { Area = "ProgressReporting" });
            }

            if (!viewModel.ShowBuildingSafetyRegulatorRegistrationCode)
            {
                return RedirectToAction("BuildingControlRequired", "ProgressReporting", new { Area = "ProgressReporting" });
            }

            return RedirectToAction("BuildingHasSafetyRegulatorRegistrationCode", "ProgressReporting", new { Area = "ProgressReporting" });
        }

        validationResult.AddToModelState(ModelState, String.Empty);
        return View(viewModel);
    }

    #endregion

    #region Need BSR or go to building control

    [HttpGet(nameof(RequireBuildingSafetyRegulatoryRegistrationCode))]
    public async Task<IActionResult> RequireBuildingSafetyRegulatoryRegistrationCode(CancellationToken cancellationToken)
    {
        var response = await _sender.Send(GetRequireBsrCodeRequest.Request, cancellationToken);
        if (response.Version > 1)
        {
            return RedirectToAction("HaveAnyAnswersChanged", "ProgressReporting", new { Area = "ProgressReporting" });
        }

        return response.ShowBsrCode
            ? RedirectToAction("BuildingHasSafetyRegulatorRegistrationCode", "ProgressReporting", new { Area = "ProgressReporting" })
            : RedirectToAction("BuildingControlRequired", "ProgressReporting", new { Area = "ProgressReporting" });
    }

    #endregion

    #region Reason haven't applied for planning permission?

    [HttpGet(nameof(ReasonPlanningNotApplied))]
    public async Task<IActionResult> ReasonPlanningNotApplied(string returnUrl)
    {
        var response = await _sender.Send(GetReasonPlanningNotAppliedRequest.Request);
        var viewModel = _mapper.Map<ReasonPlanningNotAppliedViewModel>(response);

        viewModel.ReturnUrl = returnUrl;
        return View(viewModel);
    }

    [HttpPost(nameof(ReasonPlanningNotApplied))]
    public async Task<IActionResult> ReasonPlanningNotApplied(ReasonPlanningNotAppliedViewModel viewModel, ESubmitAction submitAction)
    {
        var validator = new ReasonPlanningNotAppliedViewModelValidator();

        var validationResult = await validator.ValidateAsync(viewModel);
        if (validationResult.IsValid)
        {
            var request = _mapper.Map<SetReasonPlanningNotAppliedRequest>(viewModel);

            await _sender.Send(request);

            if (viewModel.SubmitAction == ESubmitAction.Exit)
            {
                return RedirectToAction("Index", "StageDiagram", new { area = "Application" });
            }

            return !viewModel.HasVisitedCheckYourAnswers
                ? RedirectToAction("RequireBuildingSafetyRegulatoryRegistrationCode", "ProgressReporting", new { Area = "ProgressReporting" })
                : RedirectToAction("FinalCheckYourAnswers", "ProgressReporting", new { Area = "ProgressReporting" });
        }

        validationResult.AddToModelState(ModelState, String.Empty);

        return View(viewModel);
    }

    #endregion

    #region Building Has Safety Regulator Registration Code
    [HttpGet(nameof(BuildingHasSafetyRegulatorRegistrationCode))]
    public async Task<IActionResult> BuildingHasSafetyRegulatorRegistrationCode(string returnUrl)
    {
        var response = await _sender.Send(GetBuildingHasSafetyRegulatorRegistrationCodeRequest.Request);
        var viewModel = _mapper.Map<BuildingHasSafetyRegulatorRegistrationCodeViewModel>(response);
        viewModel.ReturnUrl = returnUrl;
        return View(viewModel);
    }

    [HttpPost(nameof(BuildingHasSafetyRegulatorRegistrationCode))]
    public async Task<IActionResult> BuildingHasSafetyRegulatorRegistrationCode(BuildingHasSafetyRegulatorRegistrationCodeViewModel model)
    {
        var validator = new BuildingHasSafetyRegulatorRegistrationCodeViewModelValidator();
        var validationResult = await validator.ValidateAsync(model);
        if (!validationResult.IsValid)
        {
            validationResult.AddToModelState(ModelState, string.Empty);
            return View(model);
        }

        var request = _mapper.Map<SetBuildingHasSafetyRegulatorRegistrationCodeRequest>(model);
        await _sender.Send(request);

        if (model.SubmitAction == ESubmitAction.Exit)
        {
            return RedirectToAction("Index", "StageDiagram", new { Area = "Application" });
        }

        if (model.BuildingHasSafetyRegulatorRegistrationCode == true)
        {
            return RedirectToAction("BuildingSafetyRegulatorRegistrationCode", "ProgressReporting", new { Area = "ProgressReporting" });
        }

        if (model.Version > 1)
        {
            return RedirectToAction("HaveAnyAnswersChanged", "ProgressReporting", new { Area = "ProgressReporting" });
        }

        return model.HasVisitedCheckYourAnswers
            ? RedirectToAction("FinalCheckYourAnswers", "ProgressReporting", new { Area = "ProgressReporting" })
            : RedirectToAction("BuildingControlRequired", "ProgressReporting", new { Area = "ProgressReporting" });
    }
    #endregion

    #region Building Safety Regulator Registration Code
    [HttpGet(nameof(BuildingSafetyRegulatorRegistrationCode))]
    public async Task<IActionResult> BuildingSafetyRegulatorRegistrationCode()
    {
        var response = await _sender.Send(GetBuildingSafetyRegulatorRegistrationCodeRequest.Request);

        return View(_mapper.Map<BuildingSafetyRegulatorRegistrationCodeViewModel>(response));
    }

    [HttpPost(nameof(BuildingSafetyRegulatorRegistrationCode))]
    public async Task<IActionResult> BuildingSafetyRegulatorRegistrationCode(BuildingSafetyRegulatorRegistrationCodeViewModel model, ESubmitAction submitAction)
    {
        var validator = new BuildingSafetyRegulatorRegistrationCodeViewModelValidator();
        var validationResult = await validator.ValidateAsync(model);
        if (!validationResult.IsValid)
        {
            validationResult.AddToModelState(ModelState, string.Empty);
            return View(model);
        }

        var request = _mapper.Map<SetBuildingSafetyRegulatorRegistrationCodeRequest>(model);
        await _sender.Send(request);

        if (model.SubmitAction == ESubmitAction.Exit)
        {
            return RedirectToAction("Index", "StageDiagram", new { Area = "Application" });
        }

        if (model.Version > 1)
        {
            return RedirectToAction("HaveAnyAnswersChanged", "ProgressReporting", new { Area = "ProgressReporting" });
        }

        return !model.HasVisitedCheckYourAnswers
            ? RedirectToAction("BuildingControlRequired", "ProgressReporting", new { Area = "ProgressReporting" })
            : RedirectToAction("FinalCheckYourAnswers", "ProgressReporting", new { Area = "ProgressReporting" });
    }
    #endregion

    #region Do you need building control approval?
    [HttpGet(nameof(BuildingControlRequired))]
    public async Task<IActionResult> BuildingControlRequired(string returnUrl)
    {
        var result = await _sender.Send(GetBuildingControlRequiredRequest.Request);

        var viewModel = _mapper.Map<BuildingControlRequiredViewModel>(result);

        viewModel.ReturnUrl = returnUrl;

        return View(viewModel);
    }

    [HttpPost(nameof(BuildingControlRequired))]
    public async Task<IActionResult> BuildingControlRequired(BuildingControlRequiredViewModel model)
    {
        var validator = new BuildingControlRequiredViewModelValidator();
        var validationResult = await validator.ValidateAsync(model);

        if (!validationResult.IsValid)
        {
            validationResult.AddToModelState(ModelState, String.Empty);
            return View(model);
        }

        var request = _mapper.Map<UpdateBuildingControlRequiredRequest>(model);

        await _sender.Send(request);

        return model.SubmitAction == ESubmitAction.Continue
            ? RedirectToAction("HaveYouAppliedForBuildingControl", "ProgressReporting", new { Area = "ProgressReporting" })
            : RedirectToAction("Index", "StageDiagram", new { area = "Application" });
    }
    #endregion

    #region Have you applied for building control
    [HttpGet(nameof(HaveYouAppliedForBuildingControl))]
    public async Task<IActionResult> HaveYouAppliedForBuildingControl(CancellationToken cancellationToken)
    {
        var response = await _sender.Send(GetHasAppliedForBuildingControlRequest.Request, cancellationToken);
        var viewModel = _mapper.Map<HaveYouAppliedForBuildingControlViewModel>(response);
        return View(viewModel);
    }

    [HttpPost(nameof(HaveYouAppliedForBuildingControl))]
    public async Task<IActionResult> HaveYouAppliedForBuildingControl(HaveYouAppliedForBuildingControlViewModel model, CancellationToken cancellationToken)
    {
        var validator = new HaveYouAppliedForBuildingControlViewModelValidator();
        var validationResult = await validator.ValidateAsync(model, cancellationToken);

        if (!validationResult.IsValid)
        {
            validationResult.AddToModelState(ModelState, string.Empty);
            return View(model);
        }

        var request = _mapper.Map<SetHasAppliedForBuildingControlRequest>(model);
        await _sender.Send(request, cancellationToken);

        if (model.SubmitAction == ESubmitAction.Exit)
        {
            return RedirectToAction("Index", "StageDiagram", new { area = "Application" });
        }

        return model.HasAppliedForBuildingControl == false
            ? RedirectToAction("BuildingControlForecast", "ProgressReporting", new { Area = "ProgressReporting" })
            : RedirectToAction("BuildingControlSubmission", "ProgressReporting", new { Area = "ProgressReporting" });
    }
    #endregion

    #region Building control forecast submission date
    [HttpGet(nameof(BuildingControlForecast))]
    public async Task<IActionResult> BuildingControlForecast(CancellationToken cancellationToken)
    {
        var response = await _sender.Send(GetBuildingControlForecastRequest.Request, cancellationToken);
        var viewModel = _mapper.Map<BuildingControlForecastViewModel>(response);
        return View(viewModel);
    }

    [HttpPost(nameof(BuildingControlForecast))]
    public async Task<IActionResult> BuildingControlForecast(BuildingControlForecastViewModel model, CancellationToken cancellationToken)
    {
        var validator = new BuildingControlForecastViewModelValidator();
        var validationResult = await validator.ValidateAsync(model, cancellationToken);

        if (!validationResult.IsValid)
        {
            validationResult.AddToModelState(ModelState, string.Empty);
            return View(model);
        }

        var request = _mapper.Map<SetBuildingControlForecastRequest>(model);
        await _sender.Send(request, cancellationToken);

        if (model.SubmitAction == ESubmitAction.Exit)
        {
            return RedirectToAction("Index", "StageDiagram", new { Area = "Application" });
        }

        if (model.Version > 1)
        {
            return RedirectToAction("HaveAnyAnswersChanged", "ProgressReporting", new { Area = "ProgressReporting" });
        }

        return !model.HasVisitedCheckYourAnswers
            ? RedirectToAction("WhenSubmit", "ProgressReporting", new { Area = "ProgressReporting" })
            : RedirectToAction("FinalCheckYourAnswers", "ProgressReporting", new { Area = "ProgressReporting" });
    }
    #endregion

    #region Building control submission
    [HttpGet(nameof(BuildingControlSubmission))]
    public async Task<IActionResult> BuildingControlSubmission(CancellationToken cancellationToken)
    {
        var response = await _sender.Send(GetBuildingControlSubmissionRequest.Request, cancellationToken);
        var viewModel = _mapper.Map<BuildingControlSubmissionViewModel>(response);
        return View(viewModel);
    }

    [HttpPost(nameof(BuildingControlSubmission))]
    public async Task<IActionResult> BuildingControlSubmission(BuildingControlSubmissionViewModel model, CancellationToken cancellationToken)
    {
        var validator = new BuildingControlSubmissionViewModelValidator();
        var validationResult = await validator.ValidateAsync(model, cancellationToken);

        if (!validationResult.IsValid)
        {
            validationResult.AddToModelState(ModelState, string.Empty);
            return View(model);
        }

        var request = _mapper.Map<SetBuildingControlSubmissionRequest>(model);
        await _sender.Send(request, cancellationToken);

        return model.SubmitAction == ESubmitAction.Exit
            ? RedirectToAction("Index", "StageDiagram", new { Area = "Application" })
            : RedirectToAction("BuildingControlValidation", "ProgressReporting", new { Area = "ProgressReporting" });
    }
    #endregion

    #region Building control validation
    [HttpGet(nameof(BuildingControlValidation))]
    public async Task<IActionResult> BuildingControlValidation(CancellationToken cancellationToken)
    {
        var response = await _sender.Send(GetBuildingControlValidationRequest.Request, cancellationToken);
        var viewModel = _mapper.Map<BuildingControlValidationViewModel>(response);
        return View(viewModel);
    }

    [HttpPost(nameof(BuildingControlValidation))]
    public async Task<IActionResult> BuildingControlValidation(BuildingControlValidationViewModel model, CancellationToken cancellationToken)
    {
        var validator = new BuildingControlValidationViewModelValidator();
        var validationResult = await validator.ValidateAsync(model, cancellationToken);

        if (!validationResult.IsValid)
        {
            validationResult.AddToModelState(ModelState, string.Empty);
            return View(model);
        }

        var request = _mapper.Map<SetBuildingControlValidationRequest>(model);
        await _sender.Send(request, cancellationToken);

        return model.SubmitAction == ESubmitAction.Exit
            ? RedirectToAction("Index", "StageDiagram", new { Area = "Application" })
            : RedirectToAction("BuildingControlDecision", "ProgressReporting", new { Area = "ProgressReporting" });
    }
    #endregion

    #region Building control decision
    [HttpGet(nameof(BuildingControlDecision))]
    public async Task<IActionResult> BuildingControlDecision(CancellationToken cancellationToken)
    {
        var response = await _sender.Send(GetBuildingControlDecisionRequest.Request, cancellationToken);
        var viewModel = _mapper.Map<BuildingControlDecisionViewModel>(response);
        return View(viewModel);
    }

    [HttpPost(nameof(BuildingControlDecision))]
    public async Task<IActionResult> BuildingControlDecision(BuildingControlDecisionViewModel model, CancellationToken cancellationToken)
    {
        var validator = new BuildingControlDecisionViewModelValidator();
        var validationResult = await validator.ValidateAsync(model, cancellationToken);

        if (!validationResult.IsValid)
        {
            validationResult.AddToModelState(ModelState, string.Empty);
            return View(model);
        }

        var request = _mapper.Map<SetBuildingControlDecisionRequest>(model);
        await _sender.Send(request, cancellationToken);

        if (model.SubmitAction == ESubmitAction.Exit)
        {
            return RedirectToAction("Index", "StageDiagram", new { Area = "Application" });
        }

        if (model.Version > 1)
        {
            return RedirectToAction("HaveAnyAnswersChanged", "ProgressReporting", new { Area = "ProgressReporting" });
        }

        return !model.HasVisitedCheckYourAnswers
            ? RedirectToAction("WhenSubmit", "ProgressReporting", new { Area = "ProgressReporting" })
            : RedirectToAction("FinalCheckYourAnswers", "ProgressReporting", new { Area = "ProgressReporting" });

    }
    #endregion

    #region When do you think you'll submit your Works package

    [HttpGet(nameof(WhenSubmit))]
    public async Task<IActionResult> WhenSubmit(string returnUrl)
    {
        var response = await _sender.Send(GetWhenSubmitRequest.Request);
        var viewModel = _mapper.Map<WhenSubmitViewModel>(response);

        viewModel.ReturnUrl = returnUrl;
        return View(viewModel);
    }

    [HttpPost(nameof(WhenSubmit))]
    public async Task<IActionResult> WhenSubmit(WhenSubmitViewModel viewModel)
    {
        var validator = new WhenSubmitViewModelValidator();

        var validationResult = await validator.ValidateAsync(viewModel);
        if (validationResult.IsValid)
        {
            var request = _mapper.Map<SetWhenSubmitRequest>(viewModel);

            await _sender.Send(request);

            if (viewModel.SubmitAction == ESubmitAction.Exit)
            {
                return RedirectToAction("Index", "StageDiagram", new { area = "Application" });
            }

            if (viewModel.ReturnUrl is not null)
            {
                return SafeRedirectToAction(viewModel.ReturnUrl, "ProgressReporting", new { Area = "ProgressReporting" });
            }

            if (viewModel.Version > 1)
            {
	            return RedirectToAction("HaveAnyAnswersChanged", "ProgressReporting", new { Area = "ProgressReporting" });

            }

            return !viewModel.HasVisitedCheckYourAnswers
	            ? RedirectToAction("WhenStartOnSite", "ProgressReporting", new { Area = "ProgressReporting" })
	            : RedirectToAction("FinalCheckYourAnswers", "ProgressReporting", new { Area = "ProgressReporting" });
		}

        validationResult.AddToModelState(ModelState, String.Empty);

        return View(viewModel);
    }

    #endregion

    #region When do you think you'll start on site?

    [HttpGet(nameof(WhenStartOnSite))]
    public async Task<IActionResult> WhenStartOnSite(string returnUrl)
    {
        var response = await _sender.Send(GetWhenStartOnSiteRequest.Request);
        var viewModel = _mapper.Map<WhenStartOnSiteViewModel>(response);

        viewModel.ReturnUrl = returnUrl;
        return View(viewModel);
    }

    [HttpPost(nameof(WhenStartOnSite))]
    public async Task<IActionResult> WhenStartOnSite(WhenStartOnSiteViewModel viewModel)
    {
        var validator = new WhenStartOnSiteViewModelValidator();

        var validationResult = await validator.ValidateAsync(viewModel);
        if (validationResult.IsValid)
        {
            var request = _mapper.Map<SetWhenStartOnSiteRequest>(viewModel);

            var response = await _sender.Send(request);

            if (viewModel.SubmitAction == ESubmitAction.Exit)
            {
                return RedirectToAction("Index", "StageDiagram", new { area = "Application" });
            }

            if (viewModel.Version > 1)
            {
                return RedirectToAction("HaveAnyAnswersChanged", "ProgressReporting", new { Area = "ProgressReporting" });
            }

            if (response.NeedsSupport && !viewModel.HasVisitedCheckYourAnswers)
            {
                return RedirectToAction("ReasonNeedsSupport", "ProgressReporting", new { Area = "ProgressReporting" });
            }

            return RedirectToAction("FinalCheckYourAnswers", "ProgressReporting", new { Area = "ProgressReporting" });
        }

        validationResult.AddToModelState(ModelState, String.Empty);

        return View(viewModel);
    }

    #endregion

    #region What kind of support do you need? 

    [HttpGet(nameof(ReasonNeedsSupport))]
    public async Task<IActionResult> ReasonNeedsSupport(string returnUrl)
    {
        var response = await _sender.Send(GetReasonNeedsSupportRequest.Request);
        var viewModel = _mapper.Map<ReasonNeedsSupportViewModel>(response);

        viewModel.ReturnUrl = returnUrl;
        return View(viewModel);
    }

    [HttpPost(nameof(ReasonNeedsSupport))]
    public async Task<IActionResult> ReasonNeedsSupport(ReasonNeedsSupportViewModel viewModel, ESubmitAction submitAction)
    {
        var validator = new ReasonNeedsSupportViewModelValidator();

        var validationResult = await validator.ValidateAsync(viewModel);
        if (validationResult.IsValid)
        {
            var request = _mapper.Map<SetReasonNeedsSupportRequest>(viewModel);

            await _sender.Send(request);

            if (viewModel.SubmitAction == ESubmitAction.Exit)
            {
                return RedirectToAction("Index", "StageDiagram", new { area = "Application" });
            }

            return RedirectToAction("FinalCheckYourAnswers", "ProgressReporting", new { Area = "ProgressReporting" });
        }

        validationResult.AddToModelState(ModelState, String.Empty);

        return View(viewModel);
    }

    #endregion

    #region Final Check Your Answers

    [HttpGet(nameof(FinalCheckYourAnswers))]
    public async Task<IActionResult> FinalCheckYourAnswers()
    {
        var response = await _sender.Send(GetFinalCheckYourAnswersRequest.Request);
        var viewModel = _mapper.Map<FinalCheckYourAnswersViewModel>(response);

        return View(viewModel);
    }

    [HttpPost(nameof(FinalCheckYourAnswers))]
    public async Task<IActionResult> FinalCheckYourAnswers(FinalCheckYourAnswersViewModel viewModel, CancellationToken cancellationToken)
    {
	    var validator = new FinalCheckYourAnswersViewModelValidator();
	    var validationResult = await validator.ValidateAsync(viewModel, cancellationToken);

	    if (!validationResult.IsValid)
	    {
            validationResult.AddToModelState(ModelState, string.Empty);
            return View(viewModel);
	    }

        await _sender.Send(SetFinalCheckYourAnswersRequest.Request, cancellationToken);

        return RedirectToAction("Submitted", "ProgressReporting", new { Area = "ProgressReporting" });
    }

    #endregion

    #region Change Your Answers

    [HttpGet(nameof(ChangeAnswers))]
    public IActionResult ChangeAnswers()
    {
        return View(new ChangeYourAnswersViewModel());
    }

    [HttpPost(nameof(ChangeAnswers))]
    public async Task<IActionResult> ChangeAnswers(ChangeYourAnswersViewModel model, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        if (model.Proceed == false)
        {
            return RedirectToAction("FinalCheckYourAnswers");
        }

        await _sender.Send(new ChangeYourAnswersRequest(), cancellationToken);

        return RedirectToAction("InformedLeaseholder");
    }
    #endregion

    #region Submitted

    [HttpGet(nameof(Submitted))]
    public async Task<IActionResult> Submitted()
    {
        var submittedResponse = await _sender.Send(GetSubmittedRequest.Request);
        var viewModel = _mapper.Map<SubmittedViewModel>(submittedResponse);

        if (submittedResponse.Version == 1)
        {
            return View(viewModel);

        }

        return View("SubmittedVersion2", viewModel);

    }

    #endregion

    #region Project Team Overview
    [HttpGet(nameof(ProjectTeamOverview))]
    public async Task<IActionResult> ProjectTeamOverview()
    {
        var response = await _sender.Send(GetProjectTeamRequest.Request);
        var viewModel = _mapper.Map<ProjectTeamViewModel>(response);

        return View(viewModel);
    }

    [HttpGet(nameof(ProjectTeamOverviewContinue))]
    public async Task<IActionResult> ProjectTeamOverviewContinue(CancellationToken cancellationToken)
    {
        var isGcoComplete = await _sender.Send(GetIsGcoCompleteRequest.Request, cancellationToken);
        var hasCertifyingRoles = await _sender.Send(ProjectTeamContinueRequest.Request, cancellationToken);

        return !isGcoComplete && hasCertifyingRoles.HasCertifyingOfficerRoles
            ? RedirectToAction("HasGrantCertifyingOfficer", "ProgressReporting", new { Area = "ProgressReporting" })
            : RedirectToAction("HaveAnyAnswersChanged", "ProgressReporting", new { Area = "ProgressReporting" });
    }

    #endregion

    #region Leaseholder Last Update

    [HttpGet(nameof(LeaseholdersInformedLast))]
    public async Task<IActionResult> LeaseholdersInformedLast()
    {
        var response = await _sender.Send(GetLeaseholdersInformedLastRequest.Request);
        var viewModel = _mapper.Map<LeaseholdersInformedLastViewModel>(response);

        return View(viewModel);
    }

    [HttpPost(nameof(LeaseholdersInformedLast))]
    public async Task<IActionResult> LeaseholdersInformedLast(LeaseholdersInformedLastViewModel viewModel)
    {
        var validator = new LeaseholdersInformedLastViewModelValidator();

        var validationResult = await validator.ValidateAsync(viewModel);
        if (validationResult.IsValid)
        {
            var request = _mapper.Map<SetLeaseholdersInformedLastRequest>(viewModel);

            await _sender.Send(request);

            if (viewModel.SubmitAction == ESubmitAction.Continue)
            {
                return !viewModel.HasVisitedCheckYourAnswers
                    ? RedirectToAction("SummariseProgress", "ProgressReporting", new { Area = "ProgressReporting" })
                    : RedirectToAction("SecondaryCheckYourAnswers", "ProgressReporting", new { Area = "ProgressReporting" });
            }

            return RedirectToAction("Index", "StageDiagram", new { Area = "Application" });
        }

        validationResult.AddToModelState(ModelState, String.Empty);

        return View("LeaseholdersInformedLast", viewModel);
    }

    #endregion

    #region Have Any Of Your Answers Changed

    [HttpGet(nameof(HaveAnyAnswersChanged))]
    public async Task<IActionResult> HaveAnyAnswersChanged()
    {
        var response = await _sender.Send(GetHaveAnyAnswersChangedRequest.Request);
        var model = _mapper.Map<HaveAnyAnswersChangedViewModel>(response);
        return View(model);
    }

    #endregion

    #region Summarise Overall Progress

    [HttpGet(nameof(SummariseProgress))]
    public async Task<IActionResult> SummariseProgress()
    {
        var response = await _sender.Send(GetSummariseProgressRequest.Request);
        var viewModel = _mapper.Map<SummariseProgressViewModel>(response);

        viewModel.ReturnUrl = string.Empty;
        return View(viewModel);
    }

    [HttpPost(nameof(SummariseProgress))]
    public async Task<IActionResult> SummariseProgress(SummariseProgressViewModel viewModel)
    {
        var validator = new SummariseProgressViewModelValidator();

        var validationResult = await validator.ValidateAsync(viewModel);

        if (!validationResult.IsValid)
        {
            validationResult.AddToModelState(ModelState, string.Empty);

            return View("SummariseProgress", viewModel);
        }

        var request = _mapper.Map<SetSummariseProgressRequest>(viewModel);

        await _sender.Send(request);

        if (viewModel.SubmitAction == ESubmitAction.Exit)
        {
            return RedirectToAction("Index", "StageDiagram", new { Area = "Application" });
        }
            
        if (viewModel.IsSupportNeeded.HasValue && viewModel.IsSupportNeeded.Value)
        {
            return RedirectToAction("ProgressSupport", "ProgressReporting", new { Area = "ProgressReporting" });
        }

        return RedirectToAction("SecondaryCheckYourAnswers", "ProgressReporting", new { Area = "ProgressReporting" });
    }

    #endregion

    #region Progress Support
    [HttpGet(nameof(ProgressSupport))]
    public async Task<IActionResult> ProgressSupport()
    {
        var response = await _sender.Send(GetProgressSupportRequest.Request);
        var model = _mapper.Map<ProgressSupportViewModel>(response);

        return View(model);
    }

    [HttpPost(nameof(ProgressSupport))]
    public async Task<IActionResult> ProgressSupport(ProgressSupportViewModel viewModel)
    {
        var validator = new ProgressSupportViewModelValidator();

        var validationResult = await validator.ValidateAsync(viewModel);

        if (!validationResult.IsValid)
        {
            validationResult.AddToModelState(ModelState, string.Empty);
            return View(viewModel);
        }

        var request = _mapper.Map<SetProgressSupportRequest>(viewModel);
        await _sender.Send(request);

        return viewModel.SubmitAction == ESubmitAction.Continue
            ? RedirectToAction("SecondaryCheckYourAnswers", "ProgressReporting", new { Area = "ProgressReporting" })
            : RedirectToAction("Index", "StageDiagram", new { Area = "Application" });
    }
    #endregion

    #region Secondary Check Your Answers

    [HttpGet(nameof(SecondaryCheckYourAnswers))]
    public async Task<IActionResult> SecondaryCheckYourAnswers()
    {
        var response = await _sender.Send(GetCheckYourAnswersRequest.Request);
        var model = _mapper.Map<SecondaryCheckYourAnswersViewModel>(response);

        return View(model);
    }

    [HttpPost(nameof(SecondaryCheckYourAnswers))]
    public async Task<IActionResult> SecondaryCheckYourAnswers(SecondaryCheckYourAnswersViewModel viewModel)
    {
        var validator = new SecondaryCheckYourAnswersViewModelValidator();
        var validationResult = await validator.ValidateAsync(viewModel);

        if (!validationResult.IsValid)
        {
            validationResult.AddToModelState(ModelState, string.Empty);
            return View(viewModel);
        }

        await _sender.Send(SetCheckYourAnswersRequest.Request);
        return RedirectToAction("Submitted", "ProgressReporting", new { Area = "ProgressReporting" });
    }

    #endregion

    #region Grant Certifying Officer

    [HttpGet(nameof(HasGrantCertifyingOfficer))]
    public async Task<IActionResult> HasGrantCertifyingOfficer(CancellationToken cancellationToken)
    {
        var response = await _sender.Send(GetHasGrantCertifyingOfficerRequest.Request, cancellationToken);
        var viewModel = _mapper.Map<HasGrantCertifyingOfficerViewModel>(response);
        return View(viewModel);
    }

    [HttpPost(nameof(HasGrantCertifyingOfficer))]
    public async Task<IActionResult> HasGrantCertifyingOfficer(HasGrantCertifyingOfficerViewModel viewModel, CancellationToken cancellationToken)
    {
        var validator = new HasGrantCertifyingOfficerViewModelValidator();

        var validationResult = await validator.ValidateAsync(viewModel, cancellationToken);

        if (!validationResult.IsValid)
        {
            validationResult.AddToModelState(ModelState, string.Empty);
            return View(viewModel);
        }

        var request = _mapper.Map<SetHasGrantCertifyingOfficerRequest>(viewModel);
        await _sender.Send(request, cancellationToken);

        if (viewModel.SubmitAction == ESubmitAction.Exit)
        {
            return RedirectToAction("Index", "StageDiagram", new { Area = "Application" });
        }

        if (viewModel.DoYouHaveAGrantCertifyingOfficer == true)
        {
            return viewModel.Version == 1 || !viewModel.IsGcoComplete
                ? RedirectToAction("WhoIsTheGrantCertifyingOfficer", "ProgressReporting", new { Area = "ProgressReporting" })
                : RedirectToAction("HaveAnyAnswersChanged", "ProgressReporting", new { Area = "ProgressReporting" });
        }

        if (viewModel.Version == 1)
        {
            return viewModel.HasVisitedCheckYourAnswers
                ? RedirectToAction("FinalCheckYourAnswers", "ProgressReporting", new { Area = "ProgressReporting" })
                : RedirectToAction("IntentToProceed", "ProgressReporting", new { Aera = "ProgressReporting" });
        }

        return RedirectToAction("HaveAnyAnswersChanged", "ProgressReporting", new { Area = "ProgressReporting" });
    }

    [HttpGet(nameof(WhoIsTheGrantCertifyingOfficer))]
    public async Task<IActionResult> WhoIsTheGrantCertifyingOfficer(CancellationToken cancellationToken)
    {
        var response = await _sender.Send(GetWhoIsTheGrantCertifyingOfficerRequest.Request, cancellationToken);
        var viewModel = _mapper.Map<WhoIsTheGrantCertifyingOfficerViewModel>(response);
        return View(viewModel);
    }

    [HttpPost(nameof(WhoIsTheGrantCertifyingOfficer))]
    public async Task<IActionResult> WhoIsTheGrantCertifyingOfficer(WhoIsTheGrantCertifyingOfficerViewModel viewModel, CancellationToken cancellationToken)
    {
        var validator = new WhoIsTheGrantCertifyingOfficerViewModelValidator();
        var validationResult = await validator.ValidateAsync(viewModel, cancellationToken);

        if (!validationResult.IsValid)
        {
            validationResult.AddToModelState(ModelState, string.Empty);
            return View(viewModel);
        }

        var request = _mapper.Map<SetWhoIsTheGrantCertifyingOfficerRequest>(viewModel);
        await _sender.Send(request, cancellationToken);

        return viewModel.SubmitAction == ESubmitAction.Continue
            ? RedirectToAction("ConfirmGcoDetails", "ProgressReporting", new { Area = "ProgressReporting" })
            : RedirectToAction("Index", "StageDiagram", new { Area = "Application" });
    }

    [HttpGet(nameof(ConfirmGcoDetails))]
    public async Task<IActionResult> ConfirmGcoDetails(CancellationToken cancellationToken)
    {
        var response = await _sender.Send(GetGcoDetailsRequest.Request, cancellationToken);
        var viewModel = _mapper.Map<ConfirmGcoDetailsViewModel>(response);
        return View(viewModel);
    }

    [HttpPost(nameof(ConfirmGcoDetails))]
    public async Task<IActionResult> ConfirmGcoDetails(ConfirmGcoDetailsViewModel viewModel, CancellationToken cancellationToken)
    {
        var validator = new ConfirmGcoDetailsViewModelValidator();
        var validationResult = await validator.ValidateAsync(viewModel, cancellationToken);

        if (!validationResult.IsValid)
        {
            validationResult.AddToModelState(ModelState, string.Empty);
            return View(viewModel);
        }

        var request = _mapper.Map<SetGcoDetailsRequest>(viewModel);
        await _sender.Send(request, cancellationToken);

        if (viewModel.SubmitAction == ESubmitAction.Exit)
        {
            return RedirectToAction("Index", "StageDiagram", new { Area = "Application" });
        }

        switch (viewModel.CertifyingOfficerResponse)
        {
            case ECertifyingOfficerResponse.Yes:
                return viewModel.Version == 1 || !viewModel.IsGcoComplete
                    ? RedirectToAction("GrantCertifyingOfficerAddress", "ProgressReporting", new { Area = "ProgressReporting" })
                    : RedirectToAction("HaveAnyAnswersChanged", "ProgressReporting", new { Area = "ProgressReporting" });
            case ECertifyingOfficerResponse.Update:
                return RedirectToAction("GrantCertifyingOfficerDetails", "ProgressReporting", new { Area = "ProgressReporting", TeamRole = viewModel.RoleId, TeamMemberId = viewModel.TeamMemberId });
            case ECertifyingOfficerResponse.No:
                return RedirectToAction("WhoIsTheGrantCertifyingOfficer", "ProgressReporting", new { Area = "ProgressReporting" }); ;
            default:
                return RedirectToAction("Index", "StageDiagram", new { Area = "Application" });
        }
    }

    [HttpGet("GrantCertifyingOfficerDetails/{teamRole:int}/{teamMemberId:guid?}")]
    public async Task<IActionResult> GrantCertifyingOfficerDetails(GetGrantCertifyingOfficerDetailsRequest request, CancellationToken cancellationToken)
    {
        var response = await _sender.Send(request, cancellationToken);
        var viewModel = _mapper.Map<GrantCertifyingOfficerDetailsViewModel>(response);
        return View(viewModel);
    }

    [HttpPost(nameof(GrantCertifyingOfficerDetails))]
    public async Task<IActionResult> GrantCertifyingOfficerDetails(GrantCertifyingOfficerDetailsViewModel viewModel, CancellationToken cancellationToken)
    {
        var validator = new GrantCertifyingOfficerDetailsViewModelValidator();
        var validationResult = await validator.ValidateAsync(viewModel, cancellationToken);

        if (!validationResult.IsValid)
        {
            validationResult.AddToModelState(ModelState, string.Empty);
            return View(viewModel);
        }

        var request = _mapper.Map<SetGrantCertifyingOfficerDetailsRequest>(viewModel);
        var teamMemberId = await _sender.Send(request, cancellationToken);

        return viewModel.SubmitAction == ESubmitAction.Exit
            ? RedirectToAction("Index", "StageDiagram", new { area = "Application" })
            : RedirectToAction("GrantCertifyingOfficerCheckYourAnswers", "ProgressReporting", new { Area = "ProgressReporting", TeamMemberId = teamMemberId });
    }

    [HttpGet("GrantCertifyingOfficerDetails/CheckYourAnswers/{teamMemberId:guid}")]
    public async Task<IActionResult> GrantCertifyingOfficerCheckYourAnswers(GetGrantCertifyingOfficerCheckYourAnswersRequest request, CancellationToken cancellationToken)
    {
        var response = await _sender.Send(request, cancellationToken);
        var viewModel = _mapper.Map<GrantCertifyingOfficerCheckYourAnswersViewModel>(response);
        return View(viewModel);
    }

    [HttpGet(nameof(GrantCertifyingOfficerAddress))]
    public async Task<IActionResult> GrantCertifyingOfficerAddress(CancellationToken cancellationToken)
    {
        var response = await _sender.Send(GetGrantCertifyingOfficerAddressRequest.Request, cancellationToken);
        var viewModel = _mapper.Map<PostCodeEntryViewModel>(response);
        return View(viewModel);
    }

    [HttpPost(nameof(GrantCertifyingOfficerAddress))]
    public async Task<IActionResult> GrantCertifyingOfficerAddress(PostCodeManualViewModel viewModel, ESubmitAction submitAction, CancellationToken cancellationToken)
    {
        var validator = new PostCodeManualViewModelValidator(false);
        var validationResult = await validator.ValidateAsync(viewModel, cancellationToken);

        if (!validationResult.IsValid)
        {
            validationResult.AddToModelState(ModelState, string.Empty);
            return View("GrantCertifyingOfficerAddressManual", viewModel);
        }

        var request = _mapper.Map<SetGrantCertifyingOfficerAddressRequest>(viewModel);
        await _sender.Send(request, cancellationToken);

        if (submitAction == ESubmitAction.Exit)
        {
            return RedirectToAction("Index", "StageDiagram", new { Area = "Application" });
        }

        return viewModel.ProgressReportVersion == 1 || !viewModel.IsProgressReportGcoComplete
            ? RedirectToAction("GrantCertifyingOfficerAuthorisedSignatories", "ProgressReporting", new { Area = "ProgressReporting" })
            : RedirectToAction("HaveAnyAnswersChanged", "ProgressReporting", new { Area = "ProgressReporting" });
    }

    [HttpGet(nameof(GrantCertifyingOfficerAddressManual))]
    public async Task<IActionResult> GrantCertifyingOfficerAddressManual(CancellationToken cancellationToken)
    {
        var response = await _sender.Send(GetGrantCertifyingOfficerAddressRequest.Request, cancellationToken);
        var viewModel = _mapper.Map<PostCodeManualViewModel>(response);
        return View(viewModel);
    }

    [ExcludeRouteRecording]
    [HttpGet(nameof(GrantCertifyingOfficerAddressPostCodeItemEntered))]
    public async Task<IActionResult> GrantCertifyingOfficerAddressPostCodeItemEntered(
        PostCodeEntryViewModel viewModel,
        ESubmitAction submitAction,
        CancellationToken cancellationToken)
    {
        var validator = new PostCodeEntryViewModelValidator();
        var validationResult = await validator.ValidateAsync(viewModel, cancellationToken);

        if (!validationResult.IsValid)
        {
            validationResult.AddToModelState(ModelState, String.Empty);

            return View("GrantCertifyingOfficerAddress", viewModel);
        }

        if (submitAction == ESubmitAction.FindAddress)
        {
            var request = new GetPostCodeRequest { PostCode = viewModel.PostCode };
            var response = await _sender.Send(request, cancellationToken);
            var newMappedModel = _mapper.Map<PostCodeSelectionViewModel>(response);
            newMappedModel.ApplicationReferenceNumber = viewModel.ApplicationReferenceNumber;
            newMappedModel.BuildingName = viewModel.BuildingName;
            newMappedModel.ProgressReportVersion = viewModel.ProgressReportVersion;
            newMappedModel.IsProgressReportGcoComplete = viewModel.IsProgressReportGcoComplete;

            if (!newMappedModel.HaveResults)
            {
                var manualViewModel = _mapper.Map<PostCodeManualViewModel>(response);
                manualViewModel.Postcode = viewModel.PostCode;
                manualViewModel.ApplicationReferenceNumber = viewModel.ApplicationReferenceNumber;
                manualViewModel.BuildingName = viewModel.BuildingName;
                manualViewModel.ProgressReportVersion = viewModel.ProgressReportVersion;
                manualViewModel.IsProgressReportGcoComplete = viewModel.IsProgressReportGcoComplete;

                return View("GrantCertifyingOfficerAddressManual", manualViewModel);
            }
            return View("GrantCertifyingOfficerAddressResults", newMappedModel);
        }

        return RedirectToAction("Index", "StageDiagram", new { Area = "Application" });
    }

    [HttpPost(nameof(GrantCertifyingOfficerAddressPostCodeItemSelected))]
    public async Task<IActionResult> GrantCertifyingOfficerAddressPostCodeItemSelected(PostCodeSelectionViewModel viewModel, ESubmitAction submitAction, CancellationToken cancellationToken)
    {
        var validator = new PostCodeSelectionViewModelValidator();
        var validationResult = await validator.ValidateAsync(viewModel, cancellationToken);

        if (!validationResult.IsValid)
        {
            validationResult.AddToModelState(ModelState, string.Empty);
            // need to set these properties on the output model if there is an error
            return View("GrantCertifyingOfficerAddressResults", viewModel);
        }

        var request = _mapper.Map<SetGrantCertifyingOfficerAddressResultRequest>(viewModel);
        await _sender.Send(request, cancellationToken);

        if (submitAction == ESubmitAction.Exit)
        {
            return RedirectToAction("Index", "StageDiagram", new { Area = "Application" });
        }

        return viewModel.ProgressReportVersion == 1 || !viewModel.IsProgressReportGcoComplete
            ? RedirectToAction("GrantCertifyingOfficerAuthorisedSignatories", "ProgressReporting", new { Area = "ProgressReporting" })
            : RedirectToAction("HaveAnyAnswersChanged", "ProgressReporting", new { Area = "ProgressReporting" });
    }

    [HttpGet(nameof(GrantCertifyingOfficerAuthorisedSignatories))]
    public async Task<IActionResult> GrantCertifyingOfficerAuthorisedSignatories(CancellationToken cancellationToken)
    {
        var response = await _sender.Send(GetGrantCertifyingOfficerSignatoryRequest.Request, cancellationToken);
        var viewModel = _mapper.Map<GrantCertifyingOfficerSignatoriesViewModel>(response);
        return View(viewModel);
    }

    [HttpPost(nameof(GrantCertifyingOfficerAuthorisedSignatories))]
    public async Task<IActionResult> GrantCertifyingOfficerAuthorisedSignatories(GrantCertifyingOfficerSignatoriesViewModel viewModel, CancellationToken cancellationToken)
    {
        var validator = new GrantCertifyingOfficerSignatoriesViewModelValidator();
        var validationResult = await validator.ValidateAsync(viewModel, cancellationToken);

        if (!validationResult.IsValid)
        {
            validationResult.AddToModelState(ModelState, string.Empty);
            return View(viewModel);
        }

        var request = _mapper.Map<SetGrantCertifyingOfficerSignatoryRequest>(viewModel);
        await _sender.Send(request, cancellationToken);

        if (viewModel.SubmitAction == ESubmitAction.Exit)
        {
            return RedirectToAction("Index", "StageDiagram", new { Area = "Application" });
        }

        if (viewModel.Version == 1)
        {
            return !viewModel.HasVisitedCheckYourAnswers
                ? RedirectToAction("IntentToProceed", "ProgressReporting", new { Area = "ProgressReporting" })
                : RedirectToAction("FinalCheckYourAnswers", "ProgressReporting", new { Area = "ProgressReporting" });
        }

        return RedirectToAction("HaveAnyAnswersChanged", "ProgressReporting", new { Area = "ProgressReporting" });
    }

    #endregion

    #region Intent To Proceed

    [HttpGet(nameof(IntentToProceed))]
    public async Task<IActionResult> IntentToProceed(CancellationToken cancellationToken)
    {
        var response = await _sender.Send(GetIntentToProceedRequest.Request, cancellationToken);
        var model = _mapper.Map<IntentToProceedViewModel>(response);
        return View(model);
    }

    [HttpPost(nameof(IntentToProceed))]
    public async Task<IActionResult> IntentToProceed(IntentToProceedViewModel model, CancellationToken cancellationToken)
    {
        var validator = new IntentToProceedViewModelValidator();

        var validationResult = await validator.ValidateAsync(model, cancellationToken);

        if (!validationResult.IsValid)
        {
            validationResult.AddToModelState(ModelState, string.Empty);
            return View(model);
        }

        var request = _mapper.Map<SetIntentToProceedRequest>(model);
        await _sender.Send(request, cancellationToken);

        if (model.SubmitAction == ESubmitAction.Exit)
        {
            return RedirectToAction("Index", "StageDiagram", new { Area = "Application" });
        }

        if (model.Version == 1)
        {
	        return !model.HasVisitedCheckYourAnswers
		        ? RedirectToAction("HasProjectPlanMilestones", "ProgressReporting", new { Area = "ProgressReporting" })
		        : RedirectToAction("FinalCheckYourAnswers", "ProgressReporting", new { Area = "ProgressReporting" });
        }

        return RedirectToAction("HaveAnyAnswersChanged", "ProgressReporting", new { Area = "ProgressReporting" });

    }

    #endregion

    #region Do you have an agreed Project Plan setting out the delivery milestones and underlying activities/timescales?

    [HttpGet(nameof(HasProjectPlanMilestones))]
    public async Task<IActionResult> HasProjectPlanMilestones(CancellationToken cancellationToken)
    {
        var response = await _sender.Send(GetHasProjectPlanMilestonesRequest.Request, cancellationToken);
        var model = _mapper.Map<HasProjectPlanMilestonesViewModel>(response);
        return View(model);
    }

    [HttpPost(nameof(HasProjectPlanMilestones))]
    public async Task<IActionResult> HasProjectPlanMilestones(HasProjectPlanMilestonesViewModel model, CancellationToken cancellationToken)
    {
        var validator = new HasProjectPlanMilestonesViewModelValidator();

        var validationResult = await validator.ValidateAsync(model, cancellationToken);

        if (!validationResult.IsValid)
        {
            validationResult.AddToModelState(ModelState, string.Empty);
            return View(model);
        }

        var request = _mapper.Map<SetHasProjectPlanMilestonesRequest>(model);
        await _sender.Send(request, cancellationToken);

        if (model.SubmitAction == ESubmitAction.Exit)
        {
            return RedirectToAction("Index", "StageDiagram", new { Area = "Application" });
        }

        if (model.Version == 1)
        {
	        return !model.HasVisitedCheckYourAnswers
		        ? RedirectToAction("SoughtQuotes", "ProgressReporting", new { Area = "ProgressReporting" })
		        : RedirectToAction("FinalCheckYourAnswers", "ProgressReporting", new { Area = "ProgressReporting" });
        }

        return RedirectToAction("HaveAnyAnswersChanged", "ProgressReporting", new { Area = "ProgressReporting" });
	}

    #endregion
}
