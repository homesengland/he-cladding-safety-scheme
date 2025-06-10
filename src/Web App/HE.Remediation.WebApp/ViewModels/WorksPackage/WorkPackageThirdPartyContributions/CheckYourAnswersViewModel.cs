using HE.Remediation.Core.Enums;
using HE.Remediation.WebApp.ViewModels.WorksPackage.Shared;

namespace HE.Remediation.WebApp.ViewModels.WorksPackage.WorkPackageThirdPartyContributions
{
    public class CheckYourAnswersViewModel : WorkPackageBaseViewModel
    {
        public EThirdPartyContributionPursuitStatus PursuingThirdPartyContribution { get; set; }
        public List<string> ContributionTypes { get; set; }
        public decimal ContributionAmount { get; set; }
        public string ContributionNotes { get; set; }
    }
}
