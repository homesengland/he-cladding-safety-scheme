using HE.Remediation.Core.Enums;
using HE.Remediation.WebApp.ViewModels.Shared;

namespace HE.Remediation.WebApp.ViewModels.WorksPackage.WorkPackageFireRiskAssessment;

public class ReportViewModel
{
    public string ApplicationReferenceNumber { get; set; }
    public string BuildingName { get; set; }

    public int? AssessorId { get; set; }
    public DateTime? FraDate { get; set; }
    public List<FireRiskAssessorCompanyViewModel> FireRiskAssessorCompanies { get; set; }
    public bool VisitedCheckYourAnswers { get; set; }
    public ESubmitAction SubmitAction { get; set; }
}