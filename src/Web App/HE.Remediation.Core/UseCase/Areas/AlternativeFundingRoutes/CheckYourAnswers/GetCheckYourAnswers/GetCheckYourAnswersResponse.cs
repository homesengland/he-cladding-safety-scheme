using HE.Remediation.Core.Enums;

namespace HE.Remediation.Core.UseCase.Areas.AlternativeFundingRoutes.CheckYourAnswers.GetCheckYourAnswers;

public class GetCheckYourAnswersResponse
{
    public EPursuedSourcesFundingType OtherSourcesPursuedType { get; set; }
    public string PursuedSourcesFundingAnswer { get; set; }
    public string FundingStillPursuingAnswer { get; set; }
    public bool ReadOnly { get; set; }
}