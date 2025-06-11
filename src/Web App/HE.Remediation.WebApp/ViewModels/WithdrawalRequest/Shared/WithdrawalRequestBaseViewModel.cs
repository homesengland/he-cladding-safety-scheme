using HE.Remediation.Core.Enums;

namespace HE.Remediation.WebApp.ViewModels.WithdrawalRequest.Shared
{
    public class WithdrawalRequestBaseViewModel
    {
        public string ApplicationReferenceNumber { get; set; }

        public string BuildingName { get; set; }

        public bool IsSubmitted { get; set; }

        public bool IsExpired { get; set; }

        public ESubmitAction SubmitAction { get; set; }

        public string ReturnUrl { get; set; }
    }
}
