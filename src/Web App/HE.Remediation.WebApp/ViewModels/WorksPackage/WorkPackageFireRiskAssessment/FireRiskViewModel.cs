using HE.Remediation.Core.Enums;

namespace HE.Remediation.WebApp.ViewModels.WorksPackage.WorkPackageFireRiskAssessment;

public class FireRiskViewModel
{
    public string ApplicationReferenceNumber { get; set; }
    public string BuildingName { get; set; }

    public EFraRiskRating? FireRiskRating { get; set; }
    public bool? HasInternalFireSafetyRisks { get; set; }

    public bool VisitedCheckYourAnswers { get; set; }
    public bool HasOffPanelAssessor { get; set; }

    public ESubmitAction SubmitAction { get; set; }
}