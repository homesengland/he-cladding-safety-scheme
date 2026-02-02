using AutoMapper;
using Mediator;
using Microsoft.AspNetCore.Mvc;
using HE.Remediation.WebApp.Attributes.Authorisation;
using HE.Remediation.WebApp.ViewModels.MonthlyProgressReporting;
using HE.Remediation.Core.UseCase.Areas.MonthlyProgressReporting;

namespace HE.Remediation.WebApp.Areas.MonthlyProgressReporting.Controllers;

[Area("MonthlyProgressReporting")]
[Route("MonthlyProgressReporting")]
[CookieApplicationAuthorise]
public class MonthlyProgressReportingController : Controller
{
    private readonly ISender _sender;
    private readonly IMapper _mapper;

    public MonthlyProgressReportingController(ISender sender, IMapper mapper)
    {
        _sender = sender;
        _mapper = mapper;
    }

    //TODO: Delete once all sections have been implemented
    [HttpGet("Placeholder")]
    public IActionResult Placeholder()
    {
        return View(new TaskListViewModel());
    }

    #region Task List

    [HttpGet]
    public async Task<IActionResult> TaskList()
    {
        var response = await _sender.Send(new GetTaskListRequest());
        var viewModel = _mapper.Map<TaskListViewModel>(response);
        return View(viewModel);
    }

    #endregion

}