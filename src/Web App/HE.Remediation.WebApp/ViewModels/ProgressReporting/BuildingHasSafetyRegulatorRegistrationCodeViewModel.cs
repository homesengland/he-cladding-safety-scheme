using HE.Remediation.Core.Enums;

namespace HE.Remediation.WebApp.ViewModels.ProgressReporting;

public class BuildingHasSafetyRegulatorRegistrationCodeViewModel
{
    public string ApplicationReferenceNumber { get; set; }
    public string BuildingName { get; set; }
    public bool? WorksPermissionApplied { get; set; }
    public EYesNoNonBoolean? WorksPermissionRequired { get; set; }
    public bool? BuildingHasSafetyRegulatorRegistrationCode { get; set; }
    public int Version { get; set; }
    public bool HasVisitedCheckYourAnswers { get; set; }
    public ESubmitAction SubmitAction { get; set; }
    public string ReturnUrl { get; set; }
}