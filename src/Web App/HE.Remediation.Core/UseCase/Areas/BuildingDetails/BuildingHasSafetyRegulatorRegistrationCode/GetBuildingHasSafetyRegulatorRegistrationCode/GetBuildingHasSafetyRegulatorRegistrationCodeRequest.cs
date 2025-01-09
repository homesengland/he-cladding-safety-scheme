using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.BuildingDetails.BuildingHasSafetyRegulatorRegistrationCode.GetBuildingHasSafetyRegulatorRegistrationCode;

public class GetBuildingHasSafetyRegulatorRegistrationCodeRequest : IRequest<GetBuildingHasSafetyRegulatorRegistrationCodeResponse>
{
    private GetBuildingHasSafetyRegulatorRegistrationCodeRequest()
    {
    }

    public static readonly GetBuildingHasSafetyRegulatorRegistrationCodeRequest Request = new();
}