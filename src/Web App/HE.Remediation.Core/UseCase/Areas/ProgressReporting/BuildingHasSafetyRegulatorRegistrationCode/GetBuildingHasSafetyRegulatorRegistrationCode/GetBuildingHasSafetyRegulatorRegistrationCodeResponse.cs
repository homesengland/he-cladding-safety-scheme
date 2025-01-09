using HE.Remediation.Core.Enums;

namespace HE.Remediation.Core.UseCase.Areas.ProgressReporting.BuildingHasSafetyRegulatorRegistrationCode.GetBuildingHasSafetyRegulatorRegistrationCode;

public class GetBuildingHasSafetyRegulatorRegistrationCodeResponse
{
    public string ApplicationReferenceNumber { get; set; }
    public string BuildingName { get; set; }
    public bool? WorksPermissionApplied { get; set; }
    public EYesNoNonBoolean? WorksPermissionRequired { get; set; }
    public bool? BuildingHasSafetyRegulatorRegistrationCode { get; set; }
    public int Version { get; set; }
}