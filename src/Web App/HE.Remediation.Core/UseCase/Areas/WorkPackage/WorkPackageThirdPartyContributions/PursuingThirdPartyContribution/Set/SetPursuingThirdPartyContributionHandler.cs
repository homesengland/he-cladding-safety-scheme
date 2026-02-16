using System.Transactions;
using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Data.StoredProcedureParameters.WorkPackage.ThirdPartyContributions;
using HE.Remediation.Core.Enums;
using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageThirdPartyContributions.PursuingThirdPartyContribution.Set;

public class SetPursuingThirdPartyContributionHandler : IRequestHandler<SetPursuingThirdPartyContributionRequest>
{
    private readonly IWorkPackageRepository _workPackageRepository;

    public SetPursuingThirdPartyContributionHandler(IWorkPackageRepository workPackageRepository)
    {
        _workPackageRepository = workPackageRepository;
    }

    public async ValueTask<Unit> Handle(SetPursuingThirdPartyContributionRequest request, CancellationToken cancellationToken)
    {
        using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

        var thirdPartyContributionsStatus = await _workPackageRepository.GetThirdPartyContributionsStatus();
        if (thirdPartyContributionsStatus is null)
        {
            await _workPackageRepository.InsertThirdPartyContributions();
        }

        await _workPackageRepository.UpdateThirdPartyContributionsPursuingThirdPartyContribution(request.ThirdPartyContributionPursuitStatusTypeId);

        if (request.ThirdPartyContributionPursuitStatusTypeId.HasValue 
            && (request.ThirdPartyContributionPursuitStatusTypeId == EThirdPartyContributionPursuitStatus.YesSecuredContribution
                || request.ThirdPartyContributionPursuitStatusTypeId == EThirdPartyContributionPursuitStatus.YesClaimInProgress))
        {
            await _workPackageRepository.UpdateThirdPartyContributionsThirdPartyContribution(new ThirdPartyContributionParameters());
        }

        await _workPackageRepository.UpdateThirdPartyContributionsStatus(ETaskStatus.InProgress);

        scope.Complete();
        
        return Unit.Value;
    }
}
