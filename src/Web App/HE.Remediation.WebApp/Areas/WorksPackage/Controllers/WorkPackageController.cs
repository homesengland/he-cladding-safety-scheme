using AutoMapper;
using HE.Remediation.Core.Attributes;
using HE.Remediation.Core.Enums;
using HE.Remediation.Core.UseCase.Areas.WorkPackage.TaskList.GetTaskList;
using HE.Remediation.WebApp.Attributes.Authorisation;
using HE.Remediation.WebApp.ViewModels.WorksPackage;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HE.Remediation.WebApp.Areas.WorksPackage.Controllers;

[Area("WorksPackage")]
[Route("WorksPackage")]
[CookieApplicationAuthorise]
[UserIdentityMustBeTheApplicationUser]
public class WorkPackageController  : Controller
{
    private readonly ISender _sender;
    private readonly IMapper _mapper;
    public WorkPackageController(ISender sender, IMapper mapper)
    {
        _sender = sender;
        _mapper = mapper;
    }

    #region Task List

    [HttpGet(nameof(TaskList))]
    public async Task<IActionResult> TaskList()
    {
        var response = await _sender.Send(GetTaskListRequest.Request);
        var viewModel = _mapper.Map<TaskListViewModel>(response);

        viewModel.ReturnUrl = string.Empty;
        return View(viewModel);
    }
    
    [HttpPost(nameof(TaskList))]
    public IActionResult TaskList(TaskListViewModel viewModel, ESubmitAction submitAction)
    {
        return View(viewModel);
    }

    #endregion
}
