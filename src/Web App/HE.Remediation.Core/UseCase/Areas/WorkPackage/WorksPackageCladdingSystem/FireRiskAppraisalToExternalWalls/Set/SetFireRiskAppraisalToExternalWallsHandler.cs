using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Enums;
using HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageCladdingSystem.FireRiskAppraisalToExternalWalls.Get;
using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageCladdingSystem.FireRiskAppraisalToExternalWalls.Set;

public class SetFireRiskAppraisalToExternalWallsHandler : IRequestHandler<SetFireRiskAppraisalToExternalWallsRequest, Unit>
{
    private readonly IWorkPackageRepository _workPackageRepository;

    public SetFireRiskAppraisalToExternalWallsHandler(IWorkPackageRepository workPackageRepository)
    {
        _workPackageRepository = workPackageRepository;
    }

    public async ValueTask<Unit> Handle(SetFireRiskAppraisalToExternalWallsRequest request, CancellationToken cancellationToken)
    {
        await _workPackageRepository.UpdateWorkPackageCladdingSystemStatus(ETaskStatus.Completed);

        return Unit.Value;
    }
}
