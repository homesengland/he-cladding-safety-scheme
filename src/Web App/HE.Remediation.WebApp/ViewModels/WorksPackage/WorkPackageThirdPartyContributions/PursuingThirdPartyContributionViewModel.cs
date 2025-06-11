using HE.Remediation.Core.Enums;
using HE.Remediation.WebApp.ViewModels.WorksPackage.Shared;

namespace HE.Remediation.WebApp.ViewModels.WorksPackage.WorkPackageThirdPartyContributions;

public class PursuingThirdPartyContributionViewModel : WorkPackageBaseViewModel
{
    public EThirdPartyContributionPursuitStatus? ThirdPartyContributionPursuitStatusTypeId { get; set; }
}