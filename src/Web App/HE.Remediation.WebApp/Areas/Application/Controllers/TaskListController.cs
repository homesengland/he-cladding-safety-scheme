using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.Application.TaskList.GetTaskList;
using HE.Remediation.WebApp.Authorisation;
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

        public async Task<IActionResult> Index()
        {
            var taskListResponse = await _sender.Send(GetTaskListRequest.Request);

            var viewModel = _mapper.Map<TaskListViewModel>(taskListResponse);

            return View(viewModel);
        }
    }
}