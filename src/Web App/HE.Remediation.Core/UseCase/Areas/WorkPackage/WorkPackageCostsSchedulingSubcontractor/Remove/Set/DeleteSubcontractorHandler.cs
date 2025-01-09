using HE.Remediation.Core.Data.Repositories;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageCostsSchedulingSubcontractor.Remove.Set;

public class DeleteSubcontractorHandler : IRequestHandler<DeleteSubcontractorRequest>
{
    private readonly IWorkPackageRepository _workPackageRepository;

    public DeleteSubcontractorHandler(IWorkPackageRepository workPackageRepository)
    {
        _workPackageRepository = workPackageRepository;
    }

    public async Task<Unit> Handle(DeleteSubcontractorRequest request, CancellationToken cancellationToken)
    {
        await _workPackageRepository.DeleteCostsScheduleSubcontractor(request.SubcontractorId);
        return Unit.Value;
    }
}
