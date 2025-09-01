using HE.Remediation.Core.Enums;

namespace HE.Remediation.WebApp.ViewModels.FireRiskAssessment;

public class FireRiskViewModel
{
    public EFraRiskRating? FireRiskRating { get; set; }
    public bool? HasInternalFireSafetyRisks { get; set; }
    public bool HasOffPanelAssessor { get; set; }
    public bool VisitedCheckYourAnswers { get; set; }


    public ESubmitAction SubmitAction { get; set; }
}