using HE.Remediation.Core.Enums;

namespace HE.Remediation.WebApp.ViewModels.ProgressReporting
{
    public class SecondaryCheckYourAnswersViewModel
    {
        public string ApplicationReferenceNumber { get; set; }
        public string BuildingName { get; set; }
        public DateTime? LastUpdate { get; set; }
        public string ThisMonthProgressSummary { get; set; }
        public string NextMonthProgressSummary { get; set; }
        public bool? HelpNeeded { get; set; }
        public string SupportNeededReason { get; set; }
        public List<EProgressReportSupportType> SupportTypes { get; set; }
        public ESubmitAction SubmitAction { get; set; }
        public string ReturnUrl { get; set; }
    }
}