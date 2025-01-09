using AutoMapper;
using FluentValidation.AspNetCore;
using HE.Remediation.Core.Enums;
using HE.Remediation.Core.Exceptions;
using HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageProjectTeamMember.AddRole.Get;
using HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageProjectTeamMember.AddRole.Set;
using HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageProjectTeamMember.CheckYourAnswers.Get;
using HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageProjectTeamMember.ExistingMember.Get;
using HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageProjectTeamMember.Remove.Get;
using HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageProjectTeamMember.Remove.Set;
using HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageProjectTeamMember.TeamMember.Get;
using HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageProjectTeamMember.TeamMember.Set;
using HE.Remediation.WebApp.Attributes.Routing;
using HE.Remediation.WebApp.ViewModels.WorksPackage.WorkPackageProjectTeamMember;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HE.Remediation.WebApp.Areas.WorksPackage.WorksPackageProjectTeam.Controllers;

[Area("WorksPackageProjectTeam")]
[Route("WorksPackage/ProjectTeamMember")]
public class ProjectTeamMemberController : StartController
{
    private readonly ISender _sender;
    private readonly IMapper _mapper;

    public ProjectTeamMemberController(ISender sender, IMapper mapper) : base(sender)
    {
        _sender = sender;
        _mapper = mapper;
    }

    protected override IActionResult DefaultStart =>
        RedirectToAction("StartInformation", "ProjectTeam", new { Area = "WorksPackageProjectTeam" });

    #region Add

    [ExcludeRouteRecording]
    [HttpGet(nameof(Add))]
    public async Task<IActionResult> Add()
    {
        var response = await _sender.Send(GetAddRoleRequest.Request);
        var viewModel = _mapper.Map<AddViewModel>(response);

        viewModel.ReturnUrl = string.Empty;
        return View(viewModel);
    }

    [HttpPost(nameof(Add))]
    public async Task<IActionResult> Add(AddViewModel viewModel, ESubmitAction submitAction)
    {
        var validator = new AddViewModelValidator();
        var validationResult = await validator.ValidateAsync(viewModel);

        if (!validationResult.IsValid || !viewModel.TeamRole.HasValue)
        {
            validationResult.AddToModelState(ModelState, string.Empty);
            return View(viewModel);
        }

        var request = _mapper.Map<SetAddRoleRequest>(viewModel);
        var response = await _sender.Send(request);

        return submitAction == ESubmitAction.Continue
            ? response.CanChooseExistingMembers
                ? RedirectToAction("ExistingMember", new { TeamRole = (int)viewModel.TeamRole.Value })
                : RedirectToAction("TeamMember", new { TeamRole = (int)viewModel.TeamRole.Value })
            : RedirectToAction("TaskList", "WorkPackage", new { area = "WorksPackage" });
    }

    #endregion

    #region Existing Member

    [ExcludeRouteRecording]
    [HttpGet("ExistingMember/{teamRole:int}")]
    public async Task<IActionResult> ExistingMember([FromRoute] GetExistingMemberRequest request)
    {
        var response = await _sender.Send(request);
        var viewModel = _mapper.Map<ExistingMemberViewModel>(response);

        viewModel.ReturnUrl = string.Empty;
        return View(viewModel);
    }

    [HttpPost("ExistingMember/{teamRole:int}")]
    public async Task<IActionResult> ExistingMember(ExistingMemberViewModel viewModel, ESubmitAction submitAction)
    {
        var validator = new ExistingMemberViewModelValidator();
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
            : RedirectToAction("TaskList", "WorkPackage", new { area = "WorksPackage" });
    }

    #endregion

    #region Team Member

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
            return RedirectToAction("ProjectTeam", "ProjectTeam");
        }

        return !string.IsNullOrEmpty(model.ReturnUrl)
            ? RedirectToAction(model.ReturnUrl)
            : RedirectToAction("CheckYourAnswers", new { TeamMemberId = teamMemberId });
    }

    #endregion

    #region Check Your Answers

    [ExcludeRouteRecording]
    [HttpGet("CheckYourAnswers/{teamMemberId:guid}")]
    public async Task<IActionResult> CheckYourAnswers([FromRoute] Guid teamMemberId)
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
        if (!validationResult.IsValid)
        {
            validationResult.AddToModelState(ModelState, String.Empty);

            return View(viewModel);
        }

        if (viewModel.Confirm == true)
        {
            var request = _mapper.Map<DeleteTeamMemberRequest>(viewModel);
            await _sender.Send(request);
        }

        if (viewModel.ReturnUrl is not null)
        {
            return RedirectToAction(viewModel.ReturnUrl);
        }

        return viewModel.SubmitAction == ESubmitAction.Continue
            ? RedirectToAction("ProjectTeam", "ProjectTeam", new { Area = "WorksPackageProjectTeam" })
            : RedirectToAction("Index", "TaskList", new { area = "WorksPackage" });
    }

    #endregion
}