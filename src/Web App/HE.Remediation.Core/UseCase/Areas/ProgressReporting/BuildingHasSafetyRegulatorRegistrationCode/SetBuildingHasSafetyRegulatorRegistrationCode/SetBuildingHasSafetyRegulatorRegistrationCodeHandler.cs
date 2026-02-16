using HE.Remediation.Core.Data.Repositories;
using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.ProgressReporting.BuildingHasSafetyRegulatorRegistrationCode.SetBuildingHasSafetyRegulatorRegistrationCode;

public class SetBuildingHasSafetyRegulatorRegistrationCodeHandler : IRequestHandler<SetBuildingHasSafetyRegulatorRegistrationCodeRequest>
{
    private readonly IProgressReportingRepository _progressReportingRepository;

    public SetBuildingHasSafetyRegulatorRegistrationCodeHandler(IProgressReportingRepository progressReportingRepository)
    {
        _progressReportingRepository = progressReportingRepository;
    }

    public async ValueTask<Unit> Handle(SetBuildingHasSafetyRegulatorRegistrationCodeRequest request, CancellationToken cancellationToken)
    {
        await _progressReportingRepository.UpdateProgressReportingBuildingHasSafetyRegulatorRegistrationCode(request.BuildingHasSafetyRegulatorRegistrationCode);

        return Unit.Value;
    }
}