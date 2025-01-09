using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Data.StoredProcedureParameters.VariationRequest;
using MediatR;
using System.Transactions;

namespace HE.Remediation.Core.UseCase.Areas.VariationRequest.Scope.Set
{
    public class SetAdjustScopeHandler : IRequestHandler<SetAdjustScopeRequest>
    {
        private readonly IVariationRequestRepository _variationRequestRepository;

        public SetAdjustScopeHandler(IVariationRequestRepository variationRequestRepository)
        {
            _variationRequestRepository = variationRequestRepository;
        }

        public async Task<Unit> Handle(SetAdjustScopeRequest request, CancellationToken cancellationToken)
        {
            using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

            await _variationRequestRepository.UpdateVariationScope(new UpdateVariationScopeParameters
            {
                ChangeOfScope = request.ChangeOfScope
            });

            scope.Complete();

            return Unit.Value;
        }
    }
}