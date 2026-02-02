using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Data.StoredProcedureParameters.WorkPackage.CostsScheduling;
using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageCostsScheduling.Costs.UnsafeCladding;

public class SetUnsafeCladdingCostsHandler : IRequestHandler<SetUnsafeCladdingCostsRequest>
{
    private readonly IWorkPackageRepository _workPackageRepository;

    public SetUnsafeCladdingCostsHandler(IWorkPackageRepository workPackageRepository)
    {
        _workPackageRepository = workPackageRepository;
    }

    public async ValueTask<Unit> Handle(SetUnsafeCladdingCostsRequest request, CancellationToken cancellationToken)
    {
        await _workPackageRepository.UpdateUnsafeCladdingCosts(new UpdateUnsafeCladdingCostsParameters
        {
            RemovalOfCladdingAmount = request.UnsafeCladdingRemovalAmount,
            RemovalOfCladdingDescription = request.UnsafeCladdingRemovalDescription
        });

        return Unit.Value;
    }
}