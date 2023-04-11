using HE.Remediation.Core.Enums;

namespace HE.Remediation.WebApp.ViewModels.FireRiskAppraisal
{
    public class ExternalWorksRequiredViewModel
    {
        public ENoYes? WorksRequired { get; set; }
        public ESubmitAction? SubmitAction { get; set; }
    }
}
