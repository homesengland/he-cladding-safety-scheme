using HE.Remediation.Core.Enums;

namespace HE.Remediation.WebApp.ViewModels.MonthlyProgressReporting.KeyDates
{
    public class PlanningPermissionViewModel
    {
        public string ApplicationReferenceNumber { get; set; }
        public string BuildingName { get; set; }
        public EYesNoNonBoolean? WorksNeedPlanningPermission { get; set; }
        public ESubmitAction SubmitAction { get; set; }
    }
}
