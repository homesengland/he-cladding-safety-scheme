using HE.Remediation.Core.Enums;

namespace HE.Remediation.WebApp.ViewModels.MonthlyProgressReporting.Leaseholders
{
    public class HaveYouContactedViewModel
    {
        public string ApplicationReferenceNumber { get; set; }
        public string BuildingName { get; set; }
        public DateTime? PreviousLastCommunicationDate { get; set; }
        public ENoYes? HasContacted { get; set; }
    }
}