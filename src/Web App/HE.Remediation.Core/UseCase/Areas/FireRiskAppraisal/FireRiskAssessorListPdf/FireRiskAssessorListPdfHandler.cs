using HE.Remediation.Core.Data.Repositories.FireRiskAppraisal;
using HE.Remediation.Core.Data.StoredProcedureResults;
using HE.Remediation.Core.Enums;
using HE.Remediation.Core.Services.PdfRendererService;
using HE.Remediation.Core.Services.RazorRenderer.Models;
using MediatR;
using Syncfusion.Pdf.Graphics;

namespace HE.Remediation.Core.UseCase.Areas.FireRiskAppraisal.FireRiskAssessorListPdf;

public class FireRiskAssessorListPdfHandler : IRequestHandler<FireRiskAssessorListPdfRequest, byte[]>
{
    private readonly IFireRiskAppraisalRepository _fireRiskAppraisalRepository;
    private readonly IPdfRenderer _pdfRenderer;

    public FireRiskAssessorListPdfHandler(IFireRiskAppraisalRepository fireRiskAppraisalRepository, IPdfRenderer pdfRenderer)
    {
        _fireRiskAppraisalRepository = fireRiskAppraisalRepository;
        _pdfRenderer = pdfRenderer;
    }

    public async Task<byte[]> Handle(FireRiskAssessorListPdfRequest request, CancellationToken cancellationToken)
    {
        var assessors = await _fireRiskAppraisalRepository.GetFireRiskAssessorPdfList();

        var viewModels = assessors.Select(x => new FireRiskAssessorViewModel
        {
            Name = x.CompanyName,
            EmailAddress = x.EmailAddress,
            Telephone = x.Telephone,
            Regions = FormatRegions(x.Regions)
        });

        var pdf = await _pdfRenderer.RenderPdf(
            Path.Combine("Views", "Pdf", "FireRiskAssessorList.cshtml"), 
            viewModels,
            new PdfRenderOptions
            {
                Margins = new PdfMargins
                {
                    All = 40f
                }
            });

        return pdf;
    }

    private static string FormatRegions(IList<GetFireRiskAssessorPdfListResult.RegionResult> regions)
    {
        var allRegions = Enum.GetValues<ERegion>();

        return allRegions.All(x => regions.Any(r => r.Id == (int)x))
            ? "All Regions"
            : string.Join(", ", regions.OrderBy(x => x.Name).Select(x => x.Name));
    }

}