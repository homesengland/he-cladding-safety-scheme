using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Data.StoredProcedureParameters.VariationRequest;
using HE.Remediation.Core.Exceptions;
using Mediator;
using System.Transactions;

namespace HE.Remediation.Core.UseCase.Areas.VariationRequest.IneligibleCostsChanges.Set;

public class SetIneligibleCostsChangesHandler : IRequestHandler<SetIneligibleCostsChangesRequest>
{
    private readonly IVariationRequestRepository _variationRequestRepository;

    public SetIneligibleCostsChangesHandler(IVariationRequestRepository variationRequestRepository)
    {
        _variationRequestRepository = variationRequestRepository;
    }

    public async ValueTask<Unit> Handle(SetIneligibleCostsChangesRequest request, CancellationToken cancellationToken)
    {
        var currentVariationRequestId = await _variationRequestRepository.GetCurrentVariationRequestId();

        if (!currentVariationRequestId.HasValue)
        {
            throw new EntityNotFoundException(
                 "No valid Variation Request found for this Application.");
        }

        using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

        await _variationRequestRepository.UpdateVariationIneligibleCosts(new UpdateVariationIneligibleCostsParameters
        {
            VariationRequestId = (Guid)currentVariationRequestId,
            HasVariationIneligibleCosts = request.VariationIneligibleAmount > 0 ? true : false,
            VariationIneligibleAmount = request.VariationIneligibleAmount,
            VariationIneligibleDescription = request.VariationIneligibleDescription
        });

        scope.Complete();

        return Unit.Value;
    }
}