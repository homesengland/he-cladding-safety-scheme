using HE.Remediation.Core.Enums;

namespace HE.Remediation.WebApp.ViewModels.ClosingReport
{
    public class ThirdPartyYesNoDeclarationViewModel : ClosingReportInformationViewModel
    {
        public ENoYes? Declaration { get; set; }
        public bool IsSubmitted { get; set; }
    }
}
 