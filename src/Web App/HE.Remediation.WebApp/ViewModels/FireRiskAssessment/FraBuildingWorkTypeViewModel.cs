using HE.Remediation.Core.Enums;

namespace HE.Remediation.WebApp.ViewModels.FireRiskAssessment;

public class FraBuildingWorkTypeViewModel
{
    public EApplicationScheme ApplicationScheme { get; set; }
    public EFraBuildingWorkType? FraBuildingWorkTypeId { get; set; }
    public bool VisitedCheckYourAnswers { get; set; }

    public ESubmitAction SubmitAction { get; set; }
}