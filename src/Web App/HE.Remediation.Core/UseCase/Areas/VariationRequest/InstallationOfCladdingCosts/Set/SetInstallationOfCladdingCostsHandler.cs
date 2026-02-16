using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Data.StoredProcedureParameters.VariationRequest;
using HE.Remediation.Core.Exceptions;
using Mediator;
using System.Transactions;

namespace HE.Remediation.Core.UseCase.Areas.VariationRequest.InstallationOfCladdingCosts.Set
{
    public class SetInstallationOfCladdingCostsHandler : IRequestHandler<SetInstallationOfCladdingCostsRequest>
    {
        private readonly IVariationRequestRepository _variationRequestRepository;

        public SetInstallationOfCladdingCostsHandler(IVariationRequestRepository variationRequestRepository)
        {
            _variationRequestRepository = variationRequestRepository;
        }

        public async ValueTask<Unit> Handle(SetInstallationOfCladdingCostsRequest request, CancellationToken cancellationToken)
        {
            var currentVariationRequestId = await _variationRequestRepository.GetCurrentVariationRequestId();

            if (!currentVariationRequestId.HasValue)
            {
                throw new EntityNotFoundException(
                     "No valid Variation Request found for this Application.");
            }

            using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

            await _variationRequestRepository.UpdateVariationNewCladdingCosts(new UpdateVariationNewCladdingCostsParameters
            {
                VariationRequestId = (Guid)currentVariationRequestId,
                NewCladdingAmount = request.VariationNewCladdingAmount,
                NewCladdingDescription = request.VariationNewCladdingDescription,
                OtherEligibleWorkToExternalWallAmount = request.VariationOtherEligibleWorkToExternalWallAmount,
                OtherEligibleWorkToExternalWallDescription = request.VariationOtherEligibleWorkToExternalWallDescription,
                InternalMitigationWorksAmount = request.VariationInternalMitigationWorksAmount,
                InternalMitigationWorksDescription = request.VariationInternalMitigationWorksDescription
            });

            scope.Complete();

            return Unit.Value;
        }
    }
}
