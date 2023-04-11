using HE.Remediation.Core.Enums;

namespace HE.Remediation.WebApp.ViewModels.PreTenderSupport
{
    public class SupportRequiredViewModel
    {
        public Guid ApplicationId { get; set; }
        public bool SupportRequired { get; set; }
        public ESubmitAction SubmitAction { get; set; }
    }
}