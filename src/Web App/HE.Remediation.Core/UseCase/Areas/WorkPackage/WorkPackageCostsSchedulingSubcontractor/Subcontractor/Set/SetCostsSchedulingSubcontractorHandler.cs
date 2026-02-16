using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Data.StoredProcedureParameters.WorkPackage.CostsScheduling;
using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageCostsSchedulingSubcontractor.Subcontractor.Set;

public class SetCostsSchedulingSubcontractorHandler : IRequestHandler<SetCostsSchedulingSubcontractorRequest, Guid>
{
    private readonly IWorkPackageRepository _workPackageRepository;

    public SetCostsSchedulingSubcontractorHandler(IWorkPackageRepository workPackageRepository)
    {
        _workPackageRepository = workPackageRepository;
    }

    public async ValueTask<Guid> Handle(SetCostsSchedulingSubcontractorRequest request, CancellationToken cancellationToken)
    {
        var subcontractorId = await _workPackageRepository.UpsertCostsScheduleSubcontractor(new UpsertCostsSchedulingSubcontractorParameters
        {
            SubcontractorId = request.SubcontractorId,
            CompanyName = request.CompanyName,
            CompanyRegistration = request.CompanyRegistration
        });

        return subcontractorId;
    }
}