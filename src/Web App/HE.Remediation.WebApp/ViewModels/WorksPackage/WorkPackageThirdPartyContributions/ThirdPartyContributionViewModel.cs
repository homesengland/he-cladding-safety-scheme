using HE.Remediation.Core.Enums;
using HE.Remediation.WebApp.ViewModels.WorksPackage.Shared;

namespace HE.Remediation.WebApp.ViewModels.WorksPackage.WorkPackageThirdPartyContributions;

public class ThirdPartyContributionViewModel : WorkPackageBaseViewModel
{
    public IEnumerable<EFundingStillPursuing> ContributionPursuingTypes { get; set; } = new List<EFundingStillPursuing>();
    public decimal? ContributionAmount { get; set; }
    public string ContributionNotes { get; set; }
}