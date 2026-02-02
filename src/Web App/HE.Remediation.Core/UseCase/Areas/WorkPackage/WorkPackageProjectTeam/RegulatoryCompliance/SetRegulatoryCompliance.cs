using System.Transactions;
using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Enums;
using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageProjectTeam.RegulatoryCompliance;

public class SetRegulatoryComplianceHandler : IRequestHandler<SetRegulatoryComplianceRequest>
{
    private readonly IWorkPackageRepository _workPackageRepository;

    public SetRegulatoryComplianceHandler(IWorkPackageRepository workPackageRepository)
    {
        _workPackageRepository = workPackageRepository;
    }

    public async ValueTask<Unit> Handle(SetRegulatoryComplianceRequest request, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        await _workPackageRepository.UpdateWorkPackageRegulatoryCompliance(request.RegulatoryCompliancePersonId!.Value);

        return Unit.Value;
    }
}

public class SetRegulatoryComplianceRequest : IRequest
{
    public Guid? RegulatoryCompliancePersonId { get; set; }
}