using HE.Remediation.Core.Enums;

namespace HE.Remediation.WebApp.ViewModels.FireRiskAssessment;

public class IdentifiedDefectsViewModel
{
    public EApplicationScheme ApplicationScheme { get; set; }
    
    public IList<EInternalFireSafetyDefect> InternalFireSafetyDefects { get; set; } = new List<EInternalFireSafetyDefect>();
    public string OtherInternalDefect { get; set; }
    public bool VisitedCheckYourAnswers { get; set; }

    public ESubmitAction SubmitAction { get; set; }
}