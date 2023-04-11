using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.Application.TaskList.GetTaskList;
using HE.Remediation.WebApp.Attributes.Authorisation;
using HE.Remediation.WebApp.ViewModels.Application;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HE.Remediation.WebApp.Areas.Application.Controllers
{
    [Area("Application")]
    [CookieApplicationAuthorise]
    public class TaskListController : Controller
    {

        private readonly ISender _sender;
        private readonly IMapper _mapper;

        public TaskListController(ISender sender, IMapper mapper)
        {
            _sender = sender;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index(string backLink)
        {
            var taskListResponse = await _sender.Send(GetTaskListRequest.Request);

            var viewModel = _mapper.Map<TaskListViewModel>(taskListResponse);

            viewModel.BackLink = backLink;

            return View(viewModel);
        }
    }
}