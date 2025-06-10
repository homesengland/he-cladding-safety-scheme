using HE.Remediation.Core.Enums;

namespace HE.Remediation.WebApp.ViewModels.FireRiskAppraisal;

public class UploadFireRiskAssessmentReportViewModel
{
    public EFireRiskAssessmentType? FireRiskAssessmentType { get; set; }

    public string DeleteEndpoint => "/FireRiskAppraisal/DeleteAssessmentReport";
    public string[] FraReportAcceptedFileTypes => new[] { ".pdf", ".docx",".xlsx" };

    public Shared.File AddedFra { get; set; }

    public IFormFile FraReport { get; set; }

    public string ReturnUrl { get; set; }

    public ESubmitAction SubmitAction { get; set; }
}