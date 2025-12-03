using AutoMapper;
using HE.Remediation.Core.Interface;
using HE.Remediation.Core.UseCase.Areas.Application.StageDiagram.GetStageDiagram;
using HE.Remediation.WebApp.Attributes.Authorisation;
using HE.Remediation.WebApp.ViewModels.Application;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HE.Remediation.WebApp.Areas.Application.Controllers
{
    [Area("Application")]
    [CookieApplicationAuthorise]
    public class StageDiagramController : Controller
    {

        private readonly ISender _sender;
        private readonly IMapper _mapper;
        private readonly IApplicationDataProvider _applicationDataProvider;
        public StageDiagramController(ISender sender, IMapper mapper, IApplicationDataProvider applicationDataProvider)
        {
            _sender = sender;
            _mapper = mapper;
            _applicationDataProvider = applicationDataProvider;
        }

        public async Task<IActionResult> Index()
        {
            var taskListResponse = await _sender.Send(GetStageDiagramRequest.Request);

            var viewModel = _mapper.Map<StageDiagramViewModel>(taskListResponse);
            viewModel.ApplicationScheme = _applicationDataProvider.GetApplicationScheme();
            viewModel.IsApplicationActive = taskListResponse.IsApplicationActive;
            return View(viewModel);
        }

        [HttpGet]
        [Route("Application/StageDiagram/MonthlyProgressReporting/{id:guid}")]
        public IActionResult Create(Guid id)
        {
            _applicationDataProvider.SetProgressReportId(id);
            return RedirectToAction("TaskList", "MonthlyProgressReporting", new { Area = "MonthlyProgressReporting" });
        }
    }
}