using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.ProgressReporting.BuildingSafetyRegulatorRegistrationCode.GetBuildingSafetyRegulatorRegistrationCode
{
    public class GetBuildingSafetyRegulatorRegistrationCodeRequest : IRequest<GetBuildingSafetyRegulatorRegistrationCodeResponse>
    {
        private GetBuildingSafetyRegulatorRegistrationCodeRequest() { }

        public static readonly GetBuildingSafetyRegulatorRegistrationCodeRequest Request = new();
    }
}
