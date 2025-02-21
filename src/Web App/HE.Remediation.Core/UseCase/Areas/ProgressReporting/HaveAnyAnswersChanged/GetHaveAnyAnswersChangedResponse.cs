using HE.Remediation.Core.Enums;

namespace HE.Remediation.Core.UseCase.Areas.ProgressReporting.HaveAnyAnswersChanged;

public class GetHaveAnyAnswersChangedResponse
{
    public bool HasSoughtQuotes { get; set; }
    public EWhyYouHaveNotSoughtQuotes? WhyYouHaveNotSoughtQuotes { get; set; }
    public string QuotesNotSoughtReason { get; set; }
    public EYesNoNonBoolean NeedsPlanningPermission { get; set; }
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
    public DateTime WorksPackageEstimateDate { get; set; }

    public DateTime? ExpectedStartDateOnSite { get; set; }

    public string BuildingName { get; set; }
    public string ApplicationReferenceNumber { get; set; }

    public bool? HasGco { get; set; }
    public string TeamMember { get; set; }
    public string Role { get; set; }
    public string NameNumber { get; set; }
    public string AddressLine1 { get; set; }
    public string AddressLine2 { get; set; }
    public string City { get; set; }
    public string County { get; set; }
    public string Postcode { get; set; }
    public string Signatory { get; set; }
    public string SignatoryEmailAddress { get; set; }
    public DateTime? DateAppointed { get; set; }
    public EIntentToProceedType? IntentToProceed { get; set; }
}