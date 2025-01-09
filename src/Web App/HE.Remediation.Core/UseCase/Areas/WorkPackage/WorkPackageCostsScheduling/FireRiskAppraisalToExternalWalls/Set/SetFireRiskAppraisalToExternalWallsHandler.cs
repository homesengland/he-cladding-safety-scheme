using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Enums;
using HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageCostsScheduling.FireRiskAppraisalToExternalWalls.Get;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageCostsScheduling.FireRiskAppraisalToExternalWalls.Set;

public class SetFireRiskAppraisalToExternalWallsHandler : IRequestHandler<SetFireRiskAppraisalToExternalWallsRequest, Unit>
{
    private readonly IWorkPackageRepository _workPackageRepository;

    public SetFireRiskAppraisalToExternalWallsHandler(IWorkPackageRepository workPackageRepository)
    {
        _workPackageRepository = workPackageRepository;
    }

    public async Task<Unit> Handle(SetFireRiskAppraisalToExternalWallsRequest request, CancellationToken cancellationToken)
    {
        await _workPackageRepository.UpdateCostsScheduleStatus(ETaskStatus.InProgress);

        return Unit.Value;
    }
}
