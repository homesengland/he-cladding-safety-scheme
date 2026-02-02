using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Enums;
using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageCostsScheduling.TotalCosts;

public class SetTotalCostHandler : IRequestHandler<SetTotalCostRequest>
{
    private readonly IWorkPackageRepository _workPackageRepository;

    public SetTotalCostHandler(IWorkPackageRepository workPackageRepository)
    {
        _workPackageRepository = workPackageRepository;
    }

    public async ValueTask<Unit> Handle(SetTotalCostRequest request, CancellationToken cancellationToken)
    {
        await _workPackageRepository.UpdateCostsScheduleStatus(ETaskStatus.Completed);

        return Unit.Value;
    }
}