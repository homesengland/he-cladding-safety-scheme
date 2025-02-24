using HE.Remediation.Core.Enums;

namespace HE.Remediation.WebApp.ViewModels.AlternativeFundingRoutes;

public class CheckYourAnswersViewModel
{
    public EPursuedSourcesFundingType OtherSourcesPursuedType { get; set; }
    public string PursuedSourcesFundingAnswer { get; set; }
    public IEnumerable<string> FundingStillPursuingAnswer { get; set; }
    public bool ReadOnly { get; set; }
}