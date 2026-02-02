using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Exceptions;
using Mediator;
using System.Transactions;

namespace HE.Remediation.Core.UseCase.Areas.VariationRequest.Costs.Set
{
    public class SetCostsHandler : IRequestHandler<SetCostsRequest, SetCostsResponse>
    {
        private readonly IVariationRequestRepository _variationRequestRepository;

        public SetCostsHandler(IVariationRequestRepository variationRequestRepository)
        {
            _variationRequestRepository = variationRequestRepository;
        }

        public async ValueTask<SetCostsResponse> Handle(SetCostsRequest request, CancellationToken cancellationToken)
        {
            var currentVariationRequestId = await _variationRequestRepository.GetCurrentVariationRequestId();

            if (!currentVariationRequestId.HasValue)
            {
                throw new EntityNotFoundException(
                     "No valid Variation Request found for this Application.");
            }

            using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

            var costs = await _variationRequestRepository.GetVariationCosts((Guid)currentVariationRequestId);

            var variationTotal =
                costs.RemovalOfCladdingAmount.GetValueOrDefault(0) +
                costs.NewCladdingAmount.GetValueOrDefault(0) +
                costs.OtherEligibleWorkToExternalWallAmount.GetValueOrDefault(0) +
                costs.InternalMitigationWorksAmount.GetValueOrDefault(0) +
                costs.MainContractorPreliminariesAmount.GetValueOrDefault(0) +
                costs.AccessAmount.GetValueOrDefault(0) +
                costs.OverheadsAndProfitAmount.GetValueOrDefault(0) +
                costs.ContractorContingenciesAmount.GetValueOrDefault(0) +
                costs.FeasibilityStageAmount.GetValueOrDefault(0) +
                costs.FraewSurveyAmount.GetValueOrDefault(0) +
                costs.IrrecoverableVatAmount.GetValueOrDefault(0) +
                costs.PostTenderStageAmount.GetValueOrDefault(0) +
                costs.PropertyManagerAmount.GetValueOrDefault(0);

            if (variationTotal > 0)
            {
                return new SetCostsResponse { VariationCostsValidation = true };
            }

            return new SetCostsResponse { VariationCostsValidation = false };
        }
    }
}