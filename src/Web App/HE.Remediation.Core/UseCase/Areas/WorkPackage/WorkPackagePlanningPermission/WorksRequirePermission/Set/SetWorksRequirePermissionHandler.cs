using System.Transactions;
using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Enums;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackagePlanningPermission.WorksRequirePermission.Set;

public class SetWorksRequirePermissionHandler : IRequestHandler<SetWorksRequirePermissionRequest>
{
    private readonly IWorkPackageRepository _workPackageRepository;

    public SetWorksRequirePermissionHandler(IWorkPackageRepository workPackageRepository)
    {
        _workPackageRepository = workPackageRepository;
    }

    public async Task<Unit> Handle(SetWorksRequirePermissionRequest request, CancellationToken cancellationToken)
    {

        var requirePlanningPermission = await _workPackageRepository.GetRequirePlanningPermission();

        if (requirePlanningPermission is null)
        {
            await InsertRequirePlanningPermission(request);
        }
        else
        {
            await UpdateRequirePlanningPermission(request);
        }

        return Unit.Value;
    }

    private async Task InsertRequirePlanningPermission(SetWorksRequirePermissionRequest request)
    {
        using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

        await _workPackageRepository.InsertRequirePlanningPermission(request.PermissionRequired, request.ReasonPermissionNotRequired);

        await _workPackageRepository.UpdateWorkPackagePlanningPermissionStatus(ETaskStatus.InProgress);

        scope.Complete();
    }

    private async Task UpdateRequirePlanningPermission(SetWorksRequirePermissionRequest request)
    {
        using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

        await _workPackageRepository.UpdateRequirePlanningPermission(request.PermissionRequired, request.ReasonPermissionNotRequired);

        await _workPackageRepository.UpdateWorkPackagePlanningPermissionStatus(ETaskStatus.InProgress);

        scope.Complete();
    }
}
