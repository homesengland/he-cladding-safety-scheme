using HE.Remediation.Core.Enums;

namespace HE.Remediation.WebApp.ViewModels.MonthlyProgressReporting.KeyDates
{
    public class HaveYouAppliedPlanningPermissionViewModel
    {
        public string ApplicationReferenceNumber { get; set; }
        public string BuildingName { get; set; }
        public bool? HaveAppliedPlanningPermission { get; set; }
        public ESubmitAction SubmitAction { get; set; }
    }
}