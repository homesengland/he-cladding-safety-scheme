using AutoMapper;
using HE.Remediation.Core.Enums;
using HE.Remediation.Core.Exceptions;
using HE.Remediation.Core.UseCase.Areas.Application.LeaseholderEvidence.SetLeaseholderEvidence;
using HE.Remediation.WebApp.Authorisation;
using HE.Remediation.Core.UseCase.Areas.Application.LeaseHolderEvidence.DeleteLeaseHolderEvidence;
using HE.Remediation.Core.UseCase.Areas.Application.LeaseHolderEvidence.GetLeaseHolderEvidence;
using HE.Remediation.WebApp.ViewModels.Application;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using File = HE.Remediation.WebApp.ViewModels.Shared.File;
using HE.Remediation.WebApp.Constants;

namespace HE.Remediation.WebApp.Areas.Application.Controllers
{
    [Area("Application")]
    [CookieApplicationAuthorise]
    public class LeaseholderEvidenceController : Controller
    {
        private readonly ISender _sender;
        private readonly IMapper _mapper;

        public LeaseholderEvidenceController(ISender sender, IMapper mapper)
        {
            _sender = sender;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var leaseHolderEvidence = await _sender.Send(GetLeaseHolderEvidenceRequest.Request);

            var files = _mapper.Map<List<File>>(leaseHolderEvidence);

            var model = new LeaseHolderEvidenceViewModel
            {
                AddedFiles = files,
            };

            return View(model);
        }

        [HttpPost]
        [RequestSizeLimit(FileUploadConstants.MaxRequestSizeBytes)]
        public async Task<IActionResult> Index(LeaseHolderEvidenceViewModel request)
        {
            var useCaseRequest = _mapper.Map<SetLeaseHolderEvidenceRequest>(request);
            
            try
            {
                await _sender.Send(useCaseRequest);
            }
            catch (InvalidFileException ex)
            {
                ModelState.AddModelError(nameof(request.File), ex.Message);
                return View(request);
            }

            return useCaseRequest.Completed ? RedirectToAction("Index", "TaskList", new { Area = "Application" }) :
                RedirectToAction("Index", "LeaseHolderEvidence", new { Area = "Application"});
        }

        public async Task<IActionResult> Delete([FromQuery] DeleteLeaseHolderEvidenceRequest request)
        {
            await _sender.Send(request);

            return RedirectToAction("Index", "LeaseHolderEvidence", new { Area = "Application" });
        }
    }
}