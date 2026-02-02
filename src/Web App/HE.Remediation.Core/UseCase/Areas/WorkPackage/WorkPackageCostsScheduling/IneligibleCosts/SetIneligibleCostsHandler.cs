using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Data.StoredProcedureParameters.WorkPackage.CostsScheduling;
using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageCostsScheduling.IneligibleCosts;

public class SetIneligibleCostsHandler : IRequestHandler<SetIneligibleCostsRequest>
{
    private readonly IWorkPackageRepository _workPackageRepository;

    public SetIneligibleCostsHandler(IWorkPackageRepository workPackageRepository)
    {
        _workPackageRepository = workPackageRepository;
    }

    public async ValueTask<Unit> Handle(SetIneligibleCostsRequest request, CancellationToken cancellationToken)
    {
        await _workPackageRepository.UpdateIneligibleCosts(new UpdateIneligibleCostsParameters
        {
            IneligibleAmount = request.IneligibleAmount,
            IneligibleDescription = request.IneligibleDescription
        });

        return Unit.Value;
    }
}