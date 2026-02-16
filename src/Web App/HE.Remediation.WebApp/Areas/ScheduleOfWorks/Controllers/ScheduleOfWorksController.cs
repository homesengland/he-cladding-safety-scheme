using AutoMapper;
using HE.Remediation.Core.Enums;
using Mediator;
using Microsoft.AspNetCore.Mvc;
using HE.Remediation.Core.UseCase.Areas.ScheduleOfWorks.BaseInformation.Get;
using HE.Remediation.Core.UseCase.Areas.ScheduleOfWorks.CheckYourAnswers.Get;
using HE.Remediation.Core.UseCase.Areas.ScheduleOfWorks.ConfirmChangeProjectDates.Get;
using HE.Remediation.Core.UseCase.Areas.ScheduleOfWorks.Costs.Create;
using HE.Remediation.Core.UseCase.Areas.ScheduleOfWorks.Costs.Delete;
using HE.Remediation.Core.UseCase.Areas.ScheduleOfWorks.Declaration.Get;
using HE.Remediation.Core.UseCase.Areas.ScheduleOfWorks.Declaration.Set;
using HE.Remediation.Core.UseCase.Areas.ScheduleOfWorks.ProjectDates.Get;
using HE.Remediation.Core.UseCase.Areas.ScheduleOfWorks.ProjectDates.Set;
using HE.Remediation.Core.UseCase.Areas.ScheduleOfWorks.Submit.Set;
using HE.Remediation.Core.UseCase.Areas.ScheduleOfWorks.Submitted.Get;
using HE.Remediation.Core.UseCase.Areas.ScheduleOfWorks.WorksContract.Add;
using HE.Remediation.Core.UseCase.Areas.ScheduleOfWorks.WorksContract.Get;
using HE.Remediation.Core.UseCase.Areas.ScheduleOfWorks.WorksContract.Delete;
using HE.Remediation.Core.UseCase.Areas.ScheduleOfWorks.WorksContract.Set;
using HE.Remediation.WebApp.ViewModels.ScheduleOfWorks;
using HE.Remediation.Core.Exceptions;
using HE.Remediation.WebApp.Constants;
using FluentValidation.AspNetCore;
using FluentValidation.Results;
using HE.Remediation.Core.Helpers;
using System.Text.Json;
using HE.Remediation.Core.UseCase.Areas.ScheduleOfWorks;
using HE.Remediation.WebApp.Attributes.Routing;
using HE.Remediation.WebApp.ViewModels.Shared;
using HE.Remediation.Core.UseCase.Shared.Costs.Get;
using HE.Remediation.Core.UseCase.Shared.Costs.Set;
using HE.Remediation.Core.UseCase.Areas.ScheduleOfWorks.TaskList.GetTaskList;
using HE.Remediation.WebApp.Attributes.Authorisation;

namespace HE.Remediation.WebApp.Areas.ScheduleOfWorks.Controllers;

[Area("ScheduleOfWorks")]
[Route("ScheduleOfWorks")]
[CookieApplicationAuthorise]
public class ScheduleOfWorksController : Controller
{
    private readonly ISender _sender;
    private readonly IMapper _mapper;

    public ScheduleOfWorksController(ISender sender, IMapper mapper) 
    {
        _sender = sender;
        _mapper = mapper;
    }

    #region "Task List"

    [HttpGet(nameof(TaskList))]
    public async Task<IActionResult> TaskList()
    {
        var response = await _sender.Send(GetTaskListRequest.Request);
        var viewModel = _mapper.Map<TaskListViewModel>(response);

        viewModel.ReturnUrl = string.Empty;

        return View(viewModel);
    }

    #endregion

}
