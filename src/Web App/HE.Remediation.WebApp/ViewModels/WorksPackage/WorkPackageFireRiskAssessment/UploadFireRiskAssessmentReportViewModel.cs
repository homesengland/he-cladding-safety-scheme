using HE.Remediation.Core.Enums;
using HE.Remediation.WebApp.ViewModels.Shared;

namespace HE.Remediation.WebApp.ViewModels.WorksPackage.WorkPackageFireRiskAssessment;

public class UploadFireRiskAssessmentReportViewModel : FileUploadViewModel
{
    public string ApplicationReferenceNumber { get; set; }
    public string BuildingName { get; set; }

    public override string DeleteEndpoint => "/WorksPackage/FireRiskAssessment/UploadFireRiskAssessmentReport/Delete";
    public override string[] AcceptedFileTypes => new[] { ".pdf", ".docx", ".xlsx" };
    public override int NumberOfFilesAllowed => 1;

    public EFireRiskAssessmentType? FireRiskAssessmentType { get; set; }
    public bool VisitedCheckYourAnswers { get; set; }
}