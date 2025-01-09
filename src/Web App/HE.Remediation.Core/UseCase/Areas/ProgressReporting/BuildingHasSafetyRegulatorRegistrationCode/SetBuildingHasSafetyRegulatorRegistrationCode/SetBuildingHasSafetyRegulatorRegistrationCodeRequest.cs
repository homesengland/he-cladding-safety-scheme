using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.ProgressReporting.BuildingHasSafetyRegulatorRegistrationCode.SetBuildingHasSafetyRegulatorRegistrationCode;

public class SetBuildingHasSafetyRegulatorRegistrationCodeRequest : IRequest
{
    public bool? BuildingHasSafetyRegulatorRegistrationCode { get; set; }
}