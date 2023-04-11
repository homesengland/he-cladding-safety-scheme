using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.Application.StageDiagram.GetStageDiagram;
using HE.Remediation.WebApp.ViewModels.Application;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HE.Remediation.WebApp.Areas.Application.Controllers
{
    [Area("Application")]
    public class StageDiagramController : Controller
    {

        private readonly ISender _sender;
        private readonly IMapper _mapper;

        public StageDiagramController(ISender sender, IMapper mapper)
        {
            _sender = sender;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var taskListResponse = await _sender.Send(GetStageDiagramRequest.Request);

            var viewModel = _mapper.Map<StageDiagramViewModel>(taskListResponse);

            return View(viewModel);
        }
    }
}