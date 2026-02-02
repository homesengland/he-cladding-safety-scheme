using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Data.StoredProcedureParameters.VariationRequest;
using HE.Remediation.Core.Exceptions;
using Mediator;
using System.Transactions;

namespace HE.Remediation.Core.UseCase.Areas.VariationRequest.UnsafeCladdingCosts.Set
{
    public class SetUnsafeCladdingCostsHandler : IRequestHandler<SetUnsafeCladdingCostsRequest>
    {
        private readonly IVariationRequestRepository _variationRequestRepository;

        public SetUnsafeCladdingCostsHandler(IVariationRequestRepository variationRequestRepository)
        {
            _variationRequestRepository = variationRequestRepository;
        }

        public async ValueTask<Unit> Handle(SetUnsafeCladdingCostsRequest request, CancellationToken cancellationToken)
        {
            var currentVariationRequestId = await _variationRequestRepository.GetCurrentVariationRequestId();

            if (!currentVariationRequestId.HasValue)
            {
                throw new EntityNotFoundException(
                     "No valid Variation Request found for this Application.");
            }

            using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

            await _variationRequestRepository.UpdateVariationUnsafeCladdingCosts(new UpdateVariationUnsafeCladdingCostsParameters
            {
                VariationRequestId = (Guid)currentVariationRequestId,
                VariationRemovalOfCladdingAmount = request.VariationRemovalOfCladdingAmount,
                VariationRemovalOfCladdingDescription = request.VariationRemovalOfCladdingDescription
            });

            scope.Complete();

            return Unit.Value;
        }
    }
}