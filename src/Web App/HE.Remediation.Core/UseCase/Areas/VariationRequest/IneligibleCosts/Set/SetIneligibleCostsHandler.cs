using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Data.StoredProcedureParameters.VariationRequest;
using HE.Remediation.Core.Exceptions;
using Mediator;
using System.Transactions;

namespace HE.Remediation.Core.UseCase.Areas.VariationRequest.IneligibleCosts.Set;

public class SetIneligibleCostsHandler : IRequestHandler<SetIneligibleCostsRequest>
{
    private readonly IVariationRequestRepository _variationRequestRepository;

    public SetIneligibleCostsHandler(IVariationRequestRepository variationRequestRepository)
    {
        _variationRequestRepository = variationRequestRepository;
    }

    public async ValueTask<Unit> Handle(SetIneligibleCostsRequest request, CancellationToken cancellationToken)
    {
        var currentVariationRequestId = await _variationRequestRepository.GetCurrentVariationRequestId();

        if (!currentVariationRequestId.HasValue)
        {
            throw new EntityNotFoundException(
                 "No valid Variation Request found for this Application.");
        }

        using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

        if (request.HasVariationIneligibleCosts == Enums.ENoYes.Yes)
        {
            await _variationRequestRepository.UpdateHasVariationIneligibleCosts(new UpdateHasVariationIneligibleCostsParameters
            {
                VariationRequestId = (Guid)currentVariationRequestId,
                HasVariationIneligibleCosts = true
            });
        }
        else
        {
            await _variationRequestRepository.UpdateVariationIneligibleCosts(new UpdateVariationIneligibleCostsParameters
            {
                VariationRequestId = (Guid)currentVariationRequestId,
                HasVariationIneligibleCosts = false,
                VariationIneligibleAmount = null,
                VariationIneligibleDescription = null
            });
        }

        scope.Complete();

        return Unit.Value;
    }
}
