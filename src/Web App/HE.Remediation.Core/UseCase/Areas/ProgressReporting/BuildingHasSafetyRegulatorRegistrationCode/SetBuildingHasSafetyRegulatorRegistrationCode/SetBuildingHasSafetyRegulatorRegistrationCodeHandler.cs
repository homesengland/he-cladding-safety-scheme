using HE.Remediation.Core.Data.Repositories;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.ProgressReporting.BuildingHasSafetyRegulatorRegistrationCode.SetBuildingHasSafetyRegulatorRegistrationCode;

public class SetBuildingHasSafetyRegulatorRegistrationCodeHandler : IRequestHandler<SetBuildingHasSafetyRegulatorRegistrationCodeRequest>
{
    private readonly IProgressReportingRepository _progressReportingRepository;

    public SetBuildingHasSafetyRegulatorRegistrationCodeHandler(IProgressReportingRepository progressReportingRepository)
    {
        _progressReportingRepository = progressReportingRepository;
    }

    public async Task<Unit> Handle(SetBuildingHasSafetyRegulatorRegistrationCodeRequest request, CancellationToken cancellationToken)
    {
        await _progressReportingRepository.UpdateProgressReportingBuildingHasSafetyRegulatorRegistrationCode(request.BuildingHasSafetyRegulatorRegistrationCode);

        return Unit.Value;
    }
}