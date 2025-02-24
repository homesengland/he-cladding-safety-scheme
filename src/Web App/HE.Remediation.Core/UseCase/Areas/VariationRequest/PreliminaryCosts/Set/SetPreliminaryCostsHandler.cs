using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Data.StoredProcedureParameters.VariationRequest;
using HE.Remediation.Core.Exceptions;
using MediatR;
using System.Transactions;

namespace HE.Remediation.Core.UseCase.Areas.VariationRequest.PreliminaryCosts.Set
{
    public class SetPreliminaryCostsHandler : IRequestHandler<SetPreliminaryCostsRequest>
    {
        private readonly IVariationRequestRepository _variationRequestRepository;

        public SetPreliminaryCostsHandler(IVariationRequestRepository variationRequestRepository)
        {
            _variationRequestRepository = variationRequestRepository;
        }

        public async Task<Unit> Handle(SetPreliminaryCostsRequest request, CancellationToken cancellationToken)
        {
            var currentVariationRequestId = await _variationRequestRepository.GetCurrentVariationRequestId();

            if (!currentVariationRequestId.HasValue)
            {
                throw new EntityNotFoundException(
                     "No valid Variation Request found for this Application.");
            }

            using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

            await _variationRequestRepository.UpdateVariationPreliminaryCosts(new UpdateVariationPreliminaryCostsParameters
            {
                VariationRequestId = (Guid)currentVariationRequestId,
                VariationMainContractorPreliminariesAmount = request.VariationMainContractorPreliminariesAmount,
                VariationMainContractorPreliminariesDescription = request.VariationMainContractorPreliminariesDescription,
                VariationAccessAmount = request.VariationAccessAmount,
                VariationAccessDescription = request.VariationAccessDescription,
                VariationOverheadsAndProfitAmount = request.VariationOverheadsAndProfitAmount,
                VariationOverheadsAndProfitDescription = request.VariationOverheadsAndProfitDescription,
                VariationContractorContingenciesAmount = request.VariationContractorContingenciesAmount,
                VariationContractorContingenciesDescription = request.VariationContractorContingenciesDescription
            });

            scope.Complete();

            return Unit.Value;
        }
    }
}