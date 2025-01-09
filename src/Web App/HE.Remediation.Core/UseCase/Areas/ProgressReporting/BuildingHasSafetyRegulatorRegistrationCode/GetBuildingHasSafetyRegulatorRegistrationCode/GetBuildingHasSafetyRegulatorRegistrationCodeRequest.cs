using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.ProgressReporting.BuildingHasSafetyRegulatorRegistrationCode.GetBuildingHasSafetyRegulatorRegistrationCode;

public class GetBuildingHasSafetyRegulatorRegistrationCodeRequest : IRequest<GetBuildingHasSafetyRegulatorRegistrationCodeResponse>
{
    private GetBuildingHasSafetyRegulatorRegistrationCodeRequest()
    {
    }

    public static readonly GetBuildingHasSafetyRegulatorRegistrationCodeRequest Request = new();
}