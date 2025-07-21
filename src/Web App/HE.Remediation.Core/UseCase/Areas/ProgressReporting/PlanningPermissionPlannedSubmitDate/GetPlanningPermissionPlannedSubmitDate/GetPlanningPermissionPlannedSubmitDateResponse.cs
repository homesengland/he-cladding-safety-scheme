namespace HE.Remediation.Core.UseCase.Areas.ProgressReporting.PlanningPermissionPlannedSubmitDate.GetPlanningPermissionPlannedSubmitDate
{
    public class GetPlanningPermissionPlannedSubmitDateResponse
    {
        public string ApplicationReferenceNumber { get; set; }
        public string BuildingName { get; set; }
        public int? PlanningPermissionPlannedSubmitMonth { get; set; }
        public int? PlanningPermissionPlannedSubmitYear { get; set; }
        public int Version { get; set; }
        public bool HasVisitedCheckYourAnswers { get; set; }
    }
}