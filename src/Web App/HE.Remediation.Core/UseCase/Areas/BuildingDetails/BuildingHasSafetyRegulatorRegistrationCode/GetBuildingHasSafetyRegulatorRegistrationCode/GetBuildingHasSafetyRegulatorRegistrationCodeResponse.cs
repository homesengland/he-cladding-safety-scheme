using HE.Remediation.Core.Enums;

namespace HE.Remediation.Core.UseCase.Areas.BuildingDetails.BuildingHasSafetyRegulatorRegistrationCode.GetBuildingHasSafetyRegulatorRegistrationCode;

public class GetBuildingHasSafetyRegulatorRegistrationCodeResponse
{
    public bool? BuildingHasSafetyRegulatorRegistrationCode { get; set; }
    public EApplicationScheme ApplicationScheme { get; set; }
}