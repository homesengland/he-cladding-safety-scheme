using AutoMapper;
using HE.Remediation.Core.Interface;
using HE.Remediation.Core.UseCase.Areas.Application.StageDiagram.GetStageDiagram;
using HE.Remediation.WebApp.Attributes.Authorisation;
using HE.Remediation.WebApp.ViewModels.Application;
using Mediator;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace HE.Remediation.WebApp.Areas.Application.Controllers
{
    [Area("Application")]
    [CookieApplicationAuthorise]
    public class StageDiagramController : Controller
    {
        private readonly ISender _sender;
        private readonly IMapper _mapper;
        private readonly IApplicationDataProvider _applicationDataProvider;
        private readonly ILogger<StageDiagramController> _logger;

        public StageDiagramController(ISender sender, IMapper mapper, IApplicationDataProvider applicationDataProvider, ILogger<StageDiagramController> logger)
        {
            _sender = sender;
            _mapper = mapper;
            _applicationDataProvider = applicationDataProvider;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var taskListResponse = await _sender.Send(GetStageDiagramRequest.Request);
                var viewModel = _mapper.Map<StageDiagramViewModel>(taskListResponse);
                viewModel.ApplicationScheme = _applicationDataProvider.GetApplicationScheme();
                viewModel.IsApplicationActive = taskListResponse.IsApplicationActive;
                return View(viewModel);
            }
            catch (Exception ex)
            {
                var userId = _applicationDataProvider.GetUserId();
                _logger.LogError(ex, "Error in StageDiagramController.Index for user {UserId}", userId);
                throw;
            }
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