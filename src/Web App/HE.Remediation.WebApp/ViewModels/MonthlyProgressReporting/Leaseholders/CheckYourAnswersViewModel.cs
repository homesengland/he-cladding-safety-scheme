using HE.Remediation.Core.Enums;

namespace HE.Remediation.WebApp.ViewModels.MonthlyProgressReporting.Leaseholders
{
    public class CheckYourAnswersViewModel
    {
        public string ApplicationReferenceNumber { get; set; }
        public string BuildingName { get; set; }

        public ENoYes? HasContacted { get; set; }
        public DateTime? LastCommunicationDate { get; set; }

        public List<string> AddedFiles { get; set; }
    }
}