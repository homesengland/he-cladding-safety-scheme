using HE.Remediation.Core.Enums;

namespace HE.Remediation.WebApp.ViewModels.AlternativeFundingRoutes;

public class CostRecoveryViewModel
{
    public ECostRecoveryType? CostRecoveryType { get; set; }
    public bool VisitedCheckYourAnswers { get; set; }
    public ESubmitAction SubmitAction { get; set; }
}