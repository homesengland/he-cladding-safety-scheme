using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.BuildingDetails.BuildingSafetyRegulatorRegistrationCode.SetBuildingSafetyRegulatorRegistrationCode
{
    public class SetBuildingSafetyRegulatorRegistrationCodeRequest : IRequest<Unit>
    {
        public string BuildingSafetyRegulatorRegistrationCode { get; set; }
    }
}
