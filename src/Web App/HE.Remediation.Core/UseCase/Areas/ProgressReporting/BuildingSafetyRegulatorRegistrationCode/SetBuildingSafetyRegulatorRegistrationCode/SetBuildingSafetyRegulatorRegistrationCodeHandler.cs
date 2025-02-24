using HE.Remediation.Core.Data.Repositories;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.ProgressReporting.BuildingSafetyRegulatorRegistrationCode.SetBuildingSafetyRegulatorRegistrationCode
{
    public class SetBuildingSafetyRegulatorRegistrationCodeHandler : IRequestHandler<SetBuildingSafetyRegulatorRegistrationCodeRequest>
    {
        private readonly IProgressReportingRepository _progressReportingRepository;

        public SetBuildingSafetyRegulatorRegistrationCodeHandler(IProgressReportingRepository progressReportingRepository)
        {
            _progressReportingRepository = progressReportingRepository;
        }

        public async Task<Unit> Handle(SetBuildingSafetyRegulatorRegistrationCodeRequest request, CancellationToken cancellationToken)
        {
            await _progressReportingRepository.UpdateProgressReportingBuildingSafetyRegulatorRegistrationCode(request.BuildingSafetyRegulatorRegistrationCode); 
            
            return Unit.Value;
        }
    }
}
