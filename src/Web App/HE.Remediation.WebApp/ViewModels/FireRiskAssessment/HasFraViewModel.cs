using HE.Remediation.Core.Enums;

namespace HE.Remediation.WebApp.ViewModels.FireRiskAssessment;

public class HasFraViewModel
{
    public bool? HasFra { get; set; }
    public bool VisitedCheckYourAnswers { get; set; }

    public ESubmitAction SubmitAction { get; set; }
}