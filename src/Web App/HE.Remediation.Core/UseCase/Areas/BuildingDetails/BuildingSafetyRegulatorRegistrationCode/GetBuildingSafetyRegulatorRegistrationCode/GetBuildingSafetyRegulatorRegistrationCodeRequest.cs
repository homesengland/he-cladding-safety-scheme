using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.BuildingDetails.BuildingSafetyRegulatorRegistrationCode.GetBuildingSafetyRegulatorRegistrationCode
{
    public class GetBuildingSafetyRegulatorRegistrationCodeRequest : IRequest<GetBuildingSafetyRegulatorRegistrationCodeResponse>
    {
        private GetBuildingSafetyRegulatorRegistrationCodeRequest() { }

        public static readonly GetBuildingSafetyRegulatorRegistrationCodeRequest Request = new();
    }
}
