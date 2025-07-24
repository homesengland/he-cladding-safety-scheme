
namespace HE.Remediation.Core.UseCase.Areas.ProgressReporting.ReasonPlanningNotApplied.GetReasonPlanningNotApplied;

public class GetReasonPlanningNotAppliedResponse
{
    public string ApplicationReferenceNumber { get; set; }

    public string BuildingName { get; set; }

    public string ReasonPlanningPermissionNotApplied { get; set; }

    public bool? PlanningPermissionNeedsSupport { get; set; }
    public bool HasVisitedCheckYourAnswers { get; set; }
}
