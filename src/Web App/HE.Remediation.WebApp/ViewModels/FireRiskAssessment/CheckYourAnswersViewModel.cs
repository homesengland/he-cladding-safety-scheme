using HE.Remediation.Core.Enums;

namespace HE.Remediation.WebApp.ViewModels.FireRiskAssessment;

public class CheckYourAnswersViewModel
{
    public bool? HasFra { get; set; }
    public string FraFile { get; set; }
    public EFireRiskAssessmentType? FireRiskAssessmentType { get; set; }
    public string FireRiskAssessor { get; set; }
    public string OtherAssessorFirstName { get; set; }
    public string OtherAssessorLastName { get; set; }
    public string OtherAssessorCompanyName { get; set; }
    public string OtherAssessorCompanyNumber { get; set; }
    public string OtherAssessorEmailAddress { get; set; }
    public string OtherAssessorTelephone { get; set; }
    public DateTime? FireRiskAssessmentDate { get; set; }
    public EFraRiskRating? FireRiskRating { get; set; }
    public bool? HasInternalFireSafetyRisks { get; set; }
    public bool? HasFunding { get; set; }
    public EFraFundingType? FraFundingType { get; set; }
    public string OtherInternalFireSafetyRisk { get; set; }
    public bool IsSubmitted { get; set; }
    public IList<string> Defects { get; set; } = new List<string>();
}