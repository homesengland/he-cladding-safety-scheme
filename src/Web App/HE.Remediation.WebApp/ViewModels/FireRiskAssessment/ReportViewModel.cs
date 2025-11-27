using HE.Remediation.Core.Enums;
using HE.Remediation.WebApp.ViewModels.Shared;

namespace HE.Remediation.WebApp.ViewModels.FireRiskAssessment;

public class ReportViewModel
{
    public int? AssessorId { get; set; }
    public DateTime? FraDate { get; set; }
    public EFraCommissionerType? FraCommissionerType { get; set; }
    public EApplicationScheme? ApplicationScheme { get; set; }
    public List<FireRiskAssessorCompanyViewModel> FireRiskAssessorCompanies { get; set; }
    public bool VisitedCheckYourAnswers { get; set; }
    public ESubmitAction SubmitAction { get; set; }
}