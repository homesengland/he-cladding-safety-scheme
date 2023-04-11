using HE.Remediation.Core.Enums;

namespace HE.Remediation.Core.UseCase.Areas.AlternativeFundingRoutes.PursuedSourcesFunding.GetFundingStillPursuing;

public class GetFundingStillPursuingResponse
{
    public IEnumerable<EFundingStillPursuing> FundingStillPursuing { get; set; }
}