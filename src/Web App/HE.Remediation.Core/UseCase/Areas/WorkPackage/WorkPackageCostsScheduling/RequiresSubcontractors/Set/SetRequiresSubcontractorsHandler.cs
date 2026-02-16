using HE.Remediation.Core.Data.Repositories;
using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageCostsScheduling.RequiresSubcontractors.Set;

public class SetRequiresSubcontractorsHandler : IRequestHandler<SetRequiresSubcontractorsRequest>
{
    private readonly IWorkPackageRepository _workPackageRepository;

    public SetRequiresSubcontractorsHandler(IWorkPackageRepository workPackageRepository)
    {
        _workPackageRepository = workPackageRepository;
    }

    public async ValueTask<Unit> Handle(SetRequiresSubcontractorsRequest request, CancellationToken cancellationToken)
    {
        await _workPackageRepository.UpdateCostsScheduleRequiresSubcontractors(request.RequiresSubcontractors);
        
        return Unit.Value;
    }
}
