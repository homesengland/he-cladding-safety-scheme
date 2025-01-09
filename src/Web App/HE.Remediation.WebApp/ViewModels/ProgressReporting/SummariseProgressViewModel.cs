using HE.Remediation.Core.Enums;

namespace HE.Remediation.WebApp.ViewModels.ProgressReporting
{
    public class SummariseProgressViewModel
    {
        public string ApplicationReferenceNumber { get; set; }
        public string BuildingName { get; set; }
        public ESubmitAction SubmitAction { get; set; }
        public string ReturnUrl { get; set; }
        public string ProgressSummary { get; set; }
        public string GoalSummary { get; set; }
        public bool? IsSupportNeeded { get; set; }
    }
}
