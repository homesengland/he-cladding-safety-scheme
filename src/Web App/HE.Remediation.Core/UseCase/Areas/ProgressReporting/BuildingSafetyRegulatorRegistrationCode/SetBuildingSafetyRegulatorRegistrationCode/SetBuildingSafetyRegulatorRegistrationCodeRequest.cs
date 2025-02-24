using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.ProgressReporting.BuildingSafetyRegulatorRegistrationCode.SetBuildingSafetyRegulatorRegistrationCode
{
    public class SetBuildingSafetyRegulatorRegistrationCodeRequest : IRequest<Unit>
    {
        public string BuildingSafetyRegulatorRegistrationCode { get; set; }
    }
}
