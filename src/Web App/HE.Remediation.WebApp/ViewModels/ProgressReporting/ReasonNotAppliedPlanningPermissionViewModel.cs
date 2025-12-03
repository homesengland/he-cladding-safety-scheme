using HE.Remediation.Core.Enums;

namespace HE.Remediation.WebApp.ViewModels.ProgressReporting;
public class ReasonNotAppliedPlanningPermissionViewModel
{
    public string ApplicationReferenceNumber { get; set; }
    public string BuildingName { get; set; }
    public bool HasVisitedCheckYourAnswers { get; set; }
    public int? PlannedYearToSubmitApplication { get; set; }
    public int? PlannedMonthToSubmitApplication { get; set; }

    public DateTime? PlanToSubmitDate => PlannedYearToSubmitApplication.HasValue &&
                                         PlannedMonthToSubmitApplication.HasValue
        ? new DateTime(PlannedYearToSubmitApplication.Value, PlannedMonthToSubmitApplication.Value, 1)
        : null;
    public string ReasonNotAppliedPlanningPermission { get; set; }
    public DateTime? PreviousPlanToSubmitDate { get; set; }
    public ESubmitAction SubmitAction { get; set; }
}
