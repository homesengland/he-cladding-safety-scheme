using HE.Remediation.Core.Enums;

namespace HE.Remediation.WebApp.ViewModels.MonthlyProgressReporting.ProjectPlan
{
    public class ProjectPlanViewModel
    {
        public EApplicationScheme ApplicationScheme { get; set; }
        public string BuildingName { get; set; }
        public string ApplicationReferenceNumber { get; set; }
        public EIntentToProceedType? IntentToProceedType { get; set; }
        public decimal? AmountPaidForPTS { get; set; }
        public decimal? RemainingAmount { get; set; }
        public bool? EnoughFunds { get; set; }
        public bool? InternalAdditionalWork { get; set; }
        public ESubmitAction SubmitAction { get; set; }
    }
}
