using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.BuildingDetails.BuildingHasSafetyRegulatorRegistrationCode.SetBuildingHasSafetyRegulatorRegistrationCode;

public class SetBuildingHasSafetyRegulatorRegistrationCodeRequest : IRequest
{
    public bool? BuildingHasSafetyRegulatorRegistrationCode { get; set; }
}