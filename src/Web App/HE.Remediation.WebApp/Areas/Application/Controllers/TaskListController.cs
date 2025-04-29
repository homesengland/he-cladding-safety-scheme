using AutoMapper;
using HE.Remediation.Core.Interface;
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
        private readonly IApplicationDataProvider _applicationDataProvider;

        public TaskListController(ISender sender, IMapper mapper, IApplicationDataProvider applicationDataProvider)
        {
            _sender = sender;
            _mapper = mapper;
            _applicationDataProvider = applicationDataProvider;
        }

        public async Task<IActionResult> Index()
        {
            var taskListResponse = await _sender.Send(GetTaskListRequest.Request);

            var viewModel = _mapper.Map<TaskListViewModel>(taskListResponse);
            viewModel.ApplicationScheme = _applicationDataProvider.GetApplicationScheme();
            
            return View(viewModel);
        }
    }
}