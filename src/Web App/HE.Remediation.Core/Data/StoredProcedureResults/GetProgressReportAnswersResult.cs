using HE.Remediation.Core.Enums;

namespace HE.Remediation.Core.Data.StoredProcedureResults;

public class GetProgressReportAnswersResult
{
    public EYesNoNonBoolean RequirePlanningPermission { get; set; }
    public bool? AppliedForPlanningPermission { get; set; }
    public DateTime? PlanningPermissionSubmittedDate { get; set; }
    public DateTime? PlanningPermissionApprovedDate { get; set; }
    public DateTime? PlanningPermissionPlannedSubmitDate { get; set; }
    public string ReasonPlanningPermissionNotApplied { get; set; }
    public bool? PlanningPermissionNeedsSupport { get; set; }
    public bool PreviouslyShownBsr { get; set; }
    public bool CurrentlyShowBsr { get; set; }
    public bool? BuildingHasSafetyRegulatorRegistrationCode { get; set; }
    public string BuildingSafetyRegulatorRegistrationCode { get; set; }
    public bool? BuildingControlRequired { get; set; }
    public bool? HasAppliedForBuildingControl { get; set; }
	public DateTime? BuildingControlForecastSubmissionDate { get; set; }
    public string BuildingControlForecastInformation { get; set; }
    public DateTime? BuildingControlActualSubmissionDate { get; set; }
    public string BuildingControlActualSubmissionInformation { get; set; }
    public string BuildingControlApplicationReference { get; set; }
	public DateTime? BuildingControlValidationDate { get; set; }
    public string BuildingControlValidationInformation { get; set; }
	public DateTime? BuildingControlDecisionDate { get; set; }
    public string BuildingControlDecisionInformation { get; set; }
    public bool? BuildingControlDecision { get; set; }
    public bool QuotesSought { get; set; }
    public EWhyYouHaveNotSoughtQuotes? WhyYouHaveNotSoughtQuotes { get; set; }
    public string QuotesNotSoughtReason { get; set; }
    public DateTime ExpectedWorksPackageSubmissionDate { get; set; }
    public DateTime? ExpectedStartDateOnSite { get; set; }
    public EIntentToProceedType? IntentToProceedType { get; set; }
}