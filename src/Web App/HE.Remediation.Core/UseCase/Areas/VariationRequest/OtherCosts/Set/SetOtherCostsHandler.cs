using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Data.StoredProcedureParameters.VariationRequest;
using HE.Remediation.Core.Exceptions;
using MediatR;
using System.Transactions;

namespace HE.Remediation.Core.UseCase.Areas.VariationRequest.OtherCosts.Set
{
    public class SetOtherCostsHandler : IRequestHandler<SetOtherCostsRequest>
    {
        private readonly IVariationRequestRepository _variationRequestRepository;

        public SetOtherCostsHandler(IVariationRequestRepository variationRequestRepository)
        {
            _variationRequestRepository = variationRequestRepository;
        }

        public async Task<Unit> Handle(SetOtherCostsRequest request, CancellationToken cancellationToken)
        {
            var currentVariationRequestId = await _variationRequestRepository.GetCurrentVariationRequestId();

            if (!currentVariationRequestId.HasValue)
            {
                throw new EntityNotFoundException(
                     "No valid Variation Request found for this Application.");
            }

            using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

            await _variationRequestRepository.UpdateVariationOtherCosts(new UpdateVariationOtherCostsParameters
            {
                VariationRequestId = (Guid)currentVariationRequestId,
                VariationFraewSurveyCostsAmount = request.VariationFraewSurveyCostsAmount,
                VariationFraewSurveyCostsDescription = request.VariationFraewSurveyCostsDescription,
                VariationFeasibilityStageAmount = request?.VariationFeasibilityStageAmount,
                VariationFeasibilityStageDescription = request.VariationFeasibilityStageDescription,
                VariationPostTenderStageAmount = request?.VariationPostTenderStageAmount,
                VariationPostTenderStageDescription = request.VariationPostTenderStageDescription,
                VariationIrrecoverableVatAmount = request?.VariationIrrecoverableVatAmount,
                VariationIrrecoverableVatDescription = request.VariationIrrecoverableVatDescription,
                VariationPropertyManagerAmount = request?.VariationPropertyManagerAmount,
                VariationPropertyManagerDescription = request.VariationPropertyManagerDescription
            });

            scope.Complete();

            return Unit.Value;
        }
    }
}
