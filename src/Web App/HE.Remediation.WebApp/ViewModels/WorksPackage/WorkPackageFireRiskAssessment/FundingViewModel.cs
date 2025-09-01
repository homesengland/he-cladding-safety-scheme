using HE.Remediation.Core.Enums;

namespace HE.Remediation.WebApp.ViewModels.WorksPackage.WorkPackageFireRiskAssessment;

public class FundingViewModel
{
    public string ApplicationReferenceNumber { get; set; }
    public string BuildingName { get; set; }

    public bool? HasFunding { get; set; }
    public EFraFundingType? HasFundingType { get; set; }
    public EFraFundingType? HasNoFundingType { get; set; }

    public bool HasDefects { get; set; }
    public bool VisitedCheckYourAnswers { get; set; }
    public ESubmitAction SubmitAction { get; set; }
}