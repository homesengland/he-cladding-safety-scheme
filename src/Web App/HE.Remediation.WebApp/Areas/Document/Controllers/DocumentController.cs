using AutoMapper;
using HE.Remediation.Core.Attributes;
using HE.Remediation.Core.Exceptions;
using HE.Remediation.Core.UseCase.Areas.Document;
using HE.Remediation.WebApp.ViewModels.Document;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HE.Remediation.WebApp.Areas.Document.Controllers;

[Area("Document")]
[Route("Document")]
[UserIdentityMustBeTheApplicationUser]
public class DocumentController : Controller
{
    private readonly ISender _sender;
    private readonly IMapper _mapper;

    public DocumentController(ISender sender, IMapper mapper)
    {
        _sender = sender;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> Index(GetApplicantDocumentsRequest request, CancellationToken cancellationToken)
    {
        var response = await _sender.Send(request, cancellationToken);
        var model = _mapper.Map<ApplicantDocumentsViewModel>(response);
        if (TempData.TryGetValue("Error", out var value))
        {
            ModelState.AddModelError(nameof(ApplicantDocumentsViewModel.Files), value.ToString());
        }
        return View(model);
    }

    [HttpGet(nameof(Download))]
    public async Task<IActionResult> Download(DownloadDocumentRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var response = await _sender.Send(request, cancellationToken);
            return File(response.File, response.ContentType, response.FileName);
        }
        catch (EntityNotFoundException ex)
        {
            TempData["Error"] = ex.Message;
            return RedirectToAction("Index", "Document", new { Area = "Document" });
        }
    }
}
