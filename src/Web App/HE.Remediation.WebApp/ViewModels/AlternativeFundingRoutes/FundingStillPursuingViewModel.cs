using HE.Remediation.Core.Enums;

namespace HE.Remediation.WebApp.ViewModels.AlternativeFundingRoutes
{
    public class FundingStillPursuingViewModel
    {
        public IEnumerable<EFundingStillPursuing> FundingStillPursuing { get; set; } =
            new List<EFundingStillPursuing>();
    }
}
