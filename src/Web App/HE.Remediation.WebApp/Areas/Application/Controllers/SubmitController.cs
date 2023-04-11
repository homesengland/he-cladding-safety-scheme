using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.Application.Submit.GetSubmit;
using HE.Remediation.Core.UseCase.Areas.Application.Submit.SetSubmit;
using HE.Remediation.WebApp.Authorisation;
using HE.Remediation.WebApp.ViewModels.Application;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HE.Remediation.WebApp.Areas.Application.Controllers
{
    [Area("Application")]
    [CookieApplicationAuthorise]
    public class SubmitController : Controller
    {
        private readonly ISender _sender;
        private readonly IMapper _mapper;

        public SubmitController(ISender sender, IMapper mapper)
        {
            _sender = sender;
            _mapper = mapper;
        }

        #region "Submit"

        [HttpGet(nameof(Submit))]
        public IActionResult Submit()
        {
            return View();
        }

        [HttpPost(nameof(SubmitApplication))]
        public async Task<IActionResult> SubmitApplication()
        {
            await _sender.Send(SetSubmitRequest.Request); 
            
            return RedirectToAction("Submitted", "Submit", new { area = "Application" });
        }

        [HttpGet(nameof(Submitted))]
        public async Task<IActionResult> Submitted()
        {
            var confirmDetailsResponse = await _sender.Send(GetSubmitRequest.Request);

            var viewModel = _mapper.Map<SubmittedViewModel>(confirmDetailsResponse);

            return View(viewModel);
        }

        #endregion
    }
}