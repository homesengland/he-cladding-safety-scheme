using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Data.StoredProcedureParameters.VariationRequest;
using MediatR;
using System.Transactions;

namespace HE.Remediation.Core.UseCase.Areas.VariationRequest.Confirmation.Set
{
    public class SetConfirmationHandler : IRequestHandler<SetConfirmationRequest>
    {
        private readonly IVariationRequestRepository _variationRequestRepository;

        public SetConfirmationHandler(IVariationRequestRepository variationRequestRepository)
        {
            _variationRequestRepository = variationRequestRepository;
        }

        public async Task<Unit> Handle(SetConfirmationRequest request, CancellationToken cancellationToken)
        {
            using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

            await _variationRequestRepository.UpdateConfirmationVariationSummary(new UpdateConfirmationVariationSummaryParameters
            {
                VariationSummary = request.VariationSummary
            });

            scope.Complete();

            return Unit.Value;
        }
    }
}
