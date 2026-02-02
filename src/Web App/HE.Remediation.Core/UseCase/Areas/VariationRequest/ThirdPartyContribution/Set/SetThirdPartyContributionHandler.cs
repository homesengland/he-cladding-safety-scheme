using System.Transactions;
using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Data.StoredProcedureParameters.VariationRequest;
using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.VariationRequest.ThirdPartyContribution.Set;

public class SetThirdPartyContributionHandler : IRequestHandler<SetThirdPartyContributionRequest>
{
    private readonly IVariationRequestRepository _variationRequestRepository;

    public SetThirdPartyContributionHandler(IVariationRequestRepository variationRequestRepository)
    {
        _variationRequestRepository = variationRequestRepository;
    }

    public async ValueTask<Unit> Handle(SetThirdPartyContributionRequest request, CancellationToken cancellationToken)
    {
        using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

        var variationRequestId = await _variationRequestRepository.GetLatestVariationRequestId();
        await _variationRequestRepository.UpdateThirdPartyContributionsThirdPartyContribution(
            new UpdateThirdPartyContributionParameters
            {
                VariationRequestId = variationRequestId,
                ContributionPursuingTypes = request.ContributionPursuingTypes.Cast<int>().ToList(),
                ContributionAmount = request.ContributionAmount,
                ContributionNotes = request.ContributionNotes
            });

        scope.Complete();
        
        return Unit.Value;
    }
}