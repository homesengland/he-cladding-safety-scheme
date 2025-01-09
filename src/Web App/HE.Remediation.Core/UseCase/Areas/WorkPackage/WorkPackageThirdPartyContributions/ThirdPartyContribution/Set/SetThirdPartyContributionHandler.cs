using System;
using System.IO;
using System.Transactions;
using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Data.StoredProcedureParameters.WorkPackage.ThirdPartyContributions;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageThirdPartyContributions.ThirdPartyContribution.Set;

public class SetThirdPartyContributionHandler : IRequestHandler<SetThirdPartyContributionRequest>
{
    private readonly IWorkPackageRepository _workPackageRepository;

    public SetThirdPartyContributionHandler(IWorkPackageRepository workPackageRepository)
    {
        _workPackageRepository = workPackageRepository;
    }

    public async Task<Unit> Handle(SetThirdPartyContributionRequest request, CancellationToken cancellationToken)
    {
        using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
        
        await _workPackageRepository.UpdateThirdPartyContributionsThirdPartyContribution(
            new ThirdPartyContributionParameters
            {
                ContributionPursuingTypes = request.ContributionPursuingTypes.Cast<int>().ToList(),
                ContributionAmount = request.ContributionAmount,
                ContributionNotes = request.ContributionNotes
            });

        scope.Complete();
        
        return Unit.Value;
    }
}