using AutoMapper;
using FluentValidation.AspNetCore;
using HE.Remediation.Core.Enums;
using HE.Remediation.Core.Exceptions;
using HE.Remediation.Core.Interface;
using HE.Remediation.Core.UseCase.Areas.MonthlyProgressReporting.ProjectTeam;
using HE.Remediation.Core.UseCase.Areas.MonthlyProgressReporting.ProjectTeam.AddTeamRole;
using HE.Remediation.Core.UseCase.Areas.MonthlyProgressReporting.ProjectTeam.ExistingTeamMember;
using HE.Remediation.Core.UseCase.Areas.MonthlyProgressReporting.ProjectTeam.NoTeam;
using HE.Remediation.Core.UseCase.Areas.MonthlyProgressReporting.ProjectTeam.TeamMember;
using HE.Remediation.WebApp.Attributes.Authorisation;
using HE.Remediation.WebApp.Attributes.Routing;
using HE.Remediation.WebApp.ViewModels.MonthlyProgressReporting.ProjectTeam;
using HE.Remediation.WebApp.ViewModels.MonthlyProgressReporting.ProjectTeam.AddTeamRole;
using HE.Remediation.WebApp.ViewModels.MonthlyProgressReporting.ProjectTeam.ExistingTeamMember;
using HE.Remediation.WebApp.ViewModels.MonthlyProgressReporting.ProjectTeam.NoTeam;
using HE.Remediation.WebApp.ViewModels.MonthlyProgressReporting.ProjectTeam.TeamMember;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HE.Remediation.WebApp.Areas.MonthlyProgressReporting.MonthlyProgressReportingProjectTeam.Controllers;
[Area("MonthlyProgressReportingProjectTeam")]
[Route("MonthlyProgressReporting/ProjectTeam")]
[CookieApplicationAuthorise]
public class MonthlyProgressReportingProjectTeamController : Controller
{
    private readonly ISender _sender;
    private readonly IMapper _mapper;

    public MonthlyProgressReportingProjectTeamController(ISender sender, IMapper mapper, IApplicationDataProvider applicationDataProvider)
    {
        _sender = sender;
        _mapper = mapper;
    }

    [HttpGet("ProjectTeam")]
    public async Task<IActionResult> ProjectTeam()
    {
        var response = await _sender.Send(GetProjectTeamRequest.Request);
        var viewModel = _mapper.Map<ProjectTeamViewModel>(response);
        return View(viewModel);
    }

    #region Existing Team Member
    [ExcludeRouteRecording]
    [HttpGet("ExistingTeamMember/{teamRole:int}/{sameAsPrevious:bool?}")]
    public async Task<IActionResult> ExistingTeamMember([FromRoute] GetProjectTeamExistingTeamMemberRequest request)
    {
        var response = await _sender.Send(request);
        var viewModel = _mapper.Map<ExistingTeamMemberViewModel>(response);

        viewModel.SameAsPrevious = request.SameAsPrevious;
        viewModel.ReturnUrl = string.Empty;

        return View(viewModel);
    }

    [HttpPost("ExistingTeamMember/{teamRole:int}/{sameAsPrevious:bool?}")]
    public async Task<IActionResult> ExistingTeamMember(
        ExistingTeamMemberViewModel model,
        ESubmitAction submitAction,
        CancellationToken cancellationToken)
    {
        var validator = new ExistingTeamMemberViewModelValidator();
        var validationResult = await validator.ValidateAsync(model, cancellationToken);

        if (!validationResult.IsValid)
        {
            validationResult.AddToModelState(ModelState, string.Empty);
            return View(model);
        }

        if (model.TeamRole is not ETeamRole teamRole)
        {
            return RedirectToAction(
                nameof(ProjectTeam),
                "MonthlyProgressReportingProjectTeam",
                new { Area = "MonthlyProgressReportingProjectTeam" });
        }

        if (model.SameAsPrevious == true && model.SelectedPreviousTeamMember.HasValue)
        {
            return RedirectToAction(
                nameof(TeamMember),
                new
                {
                    teamRole = (int)teamRole,
                    teamMemberId = model.SelectedPreviousTeamMember,
                    selected = model.SelectedPreviousTeamMember,
                    sameAsPrevious = model.SameAsPrevious
                });
        }

        return RedirectToAction(
            nameof(TeamMember),
            new
            {
                teamRole = (int)teamRole,
                sameAsPrevious = model.SameAsPrevious
            });
    }
    #endregion
    #region Add Team Role
    [ExcludeRouteRecording]
    [HttpGet(nameof(AddTeamRole))]
    public async Task<IActionResult> AddTeamRole(ETeamRole? teamRole)
    {
        var response = await _sender.Send(GetAddTeamRoleRequest.Request);
        var viewModel = _mapper.Map<AddTeamRoleViewModel>(response);

        if (teamRole.HasValue)
        {
            viewModel.TeamRole = teamRole.Value;
        }

        viewModel.ReturnUrl = string.Empty;
        return View(viewModel);
    }

    [HttpPost(nameof(AddTeamRole))]
    public async Task<IActionResult> AddTeamRole(AddTeamRoleViewModel model, ESubmitAction submitAction, CancellationToken cancellationToken)
    {
        var validator = new AddTeamRoleViewModelValidator();
        var validationResult = await validator.ValidateAsync(model);
        if (!validationResult.IsValid)
        {
            validationResult.AddToModelState(ModelState, string.Empty);
            return View(model);
        }
        var request = _mapper.Map<SetAddTeamRoleRequest>(model);
        var response = await _sender.Send(request);

        if (response.CanChooseExistingMembers)
        {
            return RedirectToAction(
                nameof(ExistingTeamMember),
                "MonthlyProgressReportingProjectTeam",
                new { Area = "MonthlyProgressReportingProjectTeam", teamRole = (int)model.TeamRole.Value }
            );
        }
        else
        {
            return RedirectToAction("TeamMember", new { TeamRole = (int)model.TeamRole.Value, returnUrl = nameof(AddTeamRole) });
        }
    }
    #endregion

    #region Team Member
    [ExcludeRouteRecording]
    [HttpGet("TeamMember/{teamRole:int}/{teamMemberId:guid?}")]
    public async Task<IActionResult> TeamMember(
    [FromRoute] int teamRole,
    [FromRoute] Guid? teamMemberId,
    [FromQuery] Guid? selected,
    [FromQuery] bool? isChangingExistingMember,
    [FromQuery] bool? sameAsPrevious,
    [FromQuery] string returnUrl = null)
    {
        try
        {
            var request = new GetProjectTeamTeamMemberRequest
            {
                TeamRole = (ETeamRole)teamRole,
                TeamMemberId = teamMemberId,
                Selected = selected
            };

            var response = await _sender.Send(request);
            var viewModel = _mapper.Map<TeamMemberViewModel>(response);
            viewModel.IsChangingExistingMember = isChangingExistingMember ?? false;
            viewModel.ReturnUrl = returnUrl;
            viewModel.SameAsPrevious = sameAsPrevious;
            return View(viewModel);
        }
        catch (EntityNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpPost("TeamMember/{teamRole:int}/{teamMemberId:guid?}")]
    public async Task<IActionResult> TeamMember(
        TeamMemberViewModel model,
        ESubmitAction submitAction,
        CancellationToken cancellationToken)
    {
        var validator = new TeamMemberViewModelValidator();
        var validationResult = await validator.ValidateAsync(model);
        if (!validationResult.IsValid)
        {
            validationResult.AddToModelState(ModelState, string.Empty);
            return View(model);
        }
        var request = _mapper.Map<UpdateProjectTeamTeamMemberRequest>(model);
        request.IsChangingExistingMember = model.IsChangingExistingMember;
        var teamMemberId = await _sender.Send(request);

        return RedirectToAction("TeamMemberCheckYourAnswers", "MonthlyProgressReportingProjectTeam", new { Area = "MonthlyProgressReportingProjectTeam", TeamMemberId = teamMemberId });
    }
    #endregion

    #region Project Team No Team
    [HttpGet("NoTeam")]
    public async Task<IActionResult> NoTeam()
    {
        var response = await _sender.Send(GetProjectTeamNoTeamRequest.Request);
        var viewModel = new ProjectTeamNoTeamViewModel
        {
            BuildingName = response?.BuildingName,
            ApplicationReferenceNumber = response?.ApplicationReferenceNumber,
            ReasonNoTeam = response.ReasonNoTeam
        };
        return View(viewModel);
    }

    [HttpPost("NoTeam")]
    public async Task<IActionResult> NoTeam(ProjectTeamNoTeamViewModel model)
    {
        var validator = new ProjectTeamNoTeamViewModelValidator();
        var validationResult = await validator.ValidateAsync(model);
        if (!validationResult.IsValid)
        {
            validationResult.AddToModelState(ModelState, string.Empty);
            return View(model);
        }
        var request = _mapper.Map<SetProjectTeamNoTeamRequest>(model);
        await _sender.Send(request);

        return RedirectToAction("TaskList", "MonthlyProgressReporting", new { Area = "MonthlyProgressReporting" });
    }
    #endregion

    #region Remove Team Member
    [ExcludeRouteRecording]
    [HttpGet(nameof(RemoveTeamMember))]
    public async Task<IActionResult> RemoveTeamMember(Guid teamMemberId)
    {
        var response = await _sender.Send(new GetRemoveTeamMemberRequest { TeamMemberId = teamMemberId });
        var viewModel = _mapper.Map<RemoveTeamMemberViewModel>(response);

        viewModel.ReturnUrl = string.Empty;
        return View(viewModel);
    }

    [HttpPost(nameof(RemoveTeamMember))]
    public async Task<IActionResult> RemoveTeamMember(RemoveTeamMemberViewModel viewModel, ESubmitAction submitAction)
    {
        var validator = new RemoveTeamMemberViewModelValidator();

        var validationResult = await validator.ValidateAsync(viewModel);
        if (validationResult.IsValid)
        {
            if (viewModel.Confirm == true)
            {
                var request = _mapper.Map<DeleteTeamMemberRequest>(viewModel);
                await _sender.Send(request);
            }

            if (viewModel.SubmitAction == ESubmitAction.Continue)
            {
                return RedirectToAction("ProjectTeam", "MonthlyProgressReportingProjectTeam", new { Area = "MonthlyProgressReportingProjectTeam" });
            }

            return RedirectToAction("TaskList", "MonthlyProgressReporting", new { area = "MonthlyProgressReporting" });
        }

        validationResult.AddToModelState(ModelState, String.Empty);

        return View(viewModel);
    }
    #endregion

    #region Team Member CYA
    [ExcludeRouteRecording]
    [HttpGet("TeamMemberCheckYourAnswers/{teamMemberId:guid}")]
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

    #region Key Roles

    [HttpGet(nameof(KeyRoles))]
    public async Task<IActionResult> KeyRoles(CancellationToken cancellationToken)
    {
        var response = await _sender.Send(GetKeyRolesRequest.Request, cancellationToken);
        var model = _mapper.Map<KeyRolesViewModel>(response);
        return View(model);
    }

    [HttpPost(nameof(KeyRoles))]
    public async Task<IActionResult> KeyRoles(KeyRolesViewModel model, CancellationToken cancellationToken)
    {
        var request = _mapper.Map<SetKeyRolesRequest>(model);
        await _sender.Send(request, cancellationToken);

        return RedirectToAction("ProjectTeam", "MonthlyProgressReportingProjectTeam", new { Area = "MonthlyProgressReportingProjectTeam" });
    }

    #endregion
}