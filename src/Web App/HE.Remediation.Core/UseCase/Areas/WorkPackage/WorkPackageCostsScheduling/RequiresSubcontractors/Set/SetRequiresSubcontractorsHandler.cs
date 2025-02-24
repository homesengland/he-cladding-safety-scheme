using HE.Remediation.Core.Data.Repositories;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageCostsScheduling.RequiresSubcontractors.Set;

public class SetRequiresSubcontractorsHandler : IRequestHandler<SetRequiresSubcontractorsRequest>
{
    private readonly IWorkPackageRepository _workPackageRepository;

    public SetRequiresSubcontractorsHandler(IWorkPackageRepository workPackageRepository)
    {
        _workPackageRepository = workPackageRepository;
    }

    public async Task<Unit> Handle(SetRequiresSubcontractorsRequest request, CancellationToken cancellationToken)
    {
        await _workPackageRepository.UpdateCostsScheduleRequiresSubcontractors(request.RequiresSubcontractors);
        
        return Unit.Value;
    }
}
