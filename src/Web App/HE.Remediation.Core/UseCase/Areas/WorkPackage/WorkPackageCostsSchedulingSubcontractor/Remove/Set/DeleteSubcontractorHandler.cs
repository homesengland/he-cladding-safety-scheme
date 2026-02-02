using HE.Remediation.Core.Data.Repositories;
using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageCostsSchedulingSubcontractor.Remove.Set;

public class DeleteSubcontractorHandler : IRequestHandler<DeleteSubcontractorRequest>
{
    private readonly IWorkPackageRepository _workPackageRepository;

    public DeleteSubcontractorHandler(IWorkPackageRepository workPackageRepository)
    {
        _workPackageRepository = workPackageRepository;
    }

    public async ValueTask<Unit> Handle(DeleteSubcontractorRequest request, CancellationToken cancellationToken)
    {
        await _workPackageRepository.DeleteCostsScheduleSubcontractor(request.SubcontractorId);
        return Unit.Value;
    }
}
