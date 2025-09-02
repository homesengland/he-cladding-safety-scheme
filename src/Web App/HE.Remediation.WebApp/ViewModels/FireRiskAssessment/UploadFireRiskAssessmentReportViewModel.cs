using HE.Remediation.Core.Enums;

namespace HE.Remediation.WebApp.ViewModels.FireRiskAssessment;

public class UploadFireRiskAssessmentReportViewModel
{
    public EFireRiskAssessmentType? FireRiskAssessmentType { get; set; }

    public string DeleteEndpoint => "/FireRiskAssessment/DeleteAssessmentReport";
    public string[] FraReportAcceptedFileTypes => new[] { ".pdf", ".docx", ".xlsx" };
    public Shared.File AddedFra { get; set; }
    public IFormFile FraReport { get; set; }
    public bool VisitedCheckYourAnswers { get; set; }

    public ESubmitAction SubmitAction { get; set; }
}