using HE.Remediation.Core.Enums;
using HE.Remediation.WebApp.ViewModels.WorksPackage.Shared;

namespace HE.Remediation.WebApp.ViewModels.WorksPackage.WorkPackageCostsScheduling
{
    public class PreferredContractorLinksViewModel : WorkPackageBaseViewModel
    {
        public EYesNoNonBoolean? PreferredContractorLinks { get; set; }
        public string PreferredContractorLinkAdditionalNotes { get; set; }
        public ENoYes? SoughtQuotes { get; set; }
    }
}
