using System.Transactions;
using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Enums;
using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageProjectTeam.StartInformation.Set;

public class SetStartInformationHandler : IRequestHandler<SetStartInformationRequest, Unit>
{
    private readonly IWorkPackageRepository _workPackageRepository;

    public SetStartInformationHandler(IWorkPackageRepository workPackageRepository)
    {
        _workPackageRepository = workPackageRepository;
    }

    public async ValueTask<Unit> Handle(SetStartInformationRequest request, CancellationToken cancellationToken)
    {
        using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

        var projectTeamStatus = await _workPackageRepository.GetTeamStatus();
        if (!projectTeamStatus.HasValue)
        {
            await _workPackageRepository.InsertTeam();
            await _workPackageRepository.UpdateTeamStatus(ETaskStatus.InProgress);
        }

        scope.Complete();
        
        return Unit.Value;
    }
}