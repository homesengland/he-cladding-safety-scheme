using HE.Remediation.Core.Enums;

namespace HE.Remediation.WebApp.ViewModels.MonthlyProgressReporting.KeyDates;
public class TellUsAboutPlanningPermissionViewModel
{
    public string ApplicationReferenceNumber { get; set; }
    public string BuildingName { get; set; }

    // DateSubmitted

    public int? PlanningPermissionDateSubmittedMonth { get; set; }
    public int? PlanningPermissionDateSubmittedYear { get; set; }
    public DateTime? PlanningPermissionDateSubmitted => PlanningPermissionDateSubmittedMonth is >= 1 and <= 12
                                                            && PlanningPermissionDateSubmittedYear is >= 2000 and <= 3000
        ? new DateTime(PlanningPermissionDateSubmittedYear.Value, PlanningPermissionDateSubmittedMonth.Value, 1)
        : null;

    public int? PlanningPermissionPreviousDateSubmittedMonth { get; set; }
    public int? PlanningPermissionPreviousDateSubmittedYear { get; set; }
    public DateTime? PreviousPlanningPermissionDateSubmitted { get; set; }

    // DateApproved

    public int? PlanningPermissionDateApprovedMonth { get; set; }
    public int? PlanningPermissionDateApprovedYear { get; set; }
    public DateTime? PlanningPermissionDateApproved => PlanningPermissionDateApprovedMonth is >= 1 and <= 12
                                                        && PlanningPermissionDateApprovedYear is >= 2000 and <= 3000
    ? new DateTime(PlanningPermissionDateApprovedYear.Value, PlanningPermissionDateApprovedMonth.Value, 1)
    : null;

    public int? PlanningPermissionPreviousDateApprovedMonth { get; set; }
    public int? PlanningPermissionPreviousDateApprovedYear { get; set; }
    public DateTime? PreviousPlanningPermissionDateApproved { get; set; }

    public bool HasVisitedCheckYourAnswers { get; set; }
    public ESubmitAction SubmitAction { get; set; }
}
