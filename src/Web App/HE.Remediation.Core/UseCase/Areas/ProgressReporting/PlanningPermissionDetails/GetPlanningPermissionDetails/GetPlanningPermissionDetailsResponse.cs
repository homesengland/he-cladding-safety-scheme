namespace HE.Remediation.Core.UseCase.Areas.ProgressReporting.PlanningPermissionDetails.GetPlanningPermissionDetails
{
    public class GetPlanningPermissionDetailsResponse
    {
        public string ApplicationReferenceNumber { get; set; }
        public string BuildingName { get; set; }
        public int? PlanningPermissionSubmittedMonth { get; set; }
        public int? PlanningPermissionSubmittedYear { get; set; }
        public int? PlanningPermissionApprovedMonth { get; set; }
        public int? PlanningPermissionApprovedYear { get; set; }
        public int Version { get; set; }
        public bool ShowBuildingSafetyRegulatorRegistrationCode { get; set; }
    }
}
