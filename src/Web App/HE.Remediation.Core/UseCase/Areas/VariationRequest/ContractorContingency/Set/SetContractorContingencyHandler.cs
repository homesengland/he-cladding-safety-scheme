using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Data.StoredProcedureParameters.VariationRequest;
using HE.Remediation.Core.Exceptions;
using MediatR;
using System.Transactions;

namespace HE.Remediation.Core.UseCase.Areas.VariationRequest.ContractorContingency.Set;

public class SetContractorContingencyHandler : IRequestHandler<SetContractorContingencyRequest>
{
    private readonly IVariationRequestRepository _variationRequestRepository;

    public SetContractorContingencyHandler(IVariationRequestRepository variationRequestRepository)
    {
        _variationRequestRepository = variationRequestRepository;
    }

    public async Task<Unit> Handle(SetContractorContingencyRequest request, CancellationToken cancellationToken)
    {
        var currentVariationRequestId = await _variationRequestRepository.GetCurrentVariationRequestId();

        if (!currentVariationRequestId.HasValue)
        {
            throw new EntityNotFoundException(
                 "No valid Variation Request found for this Application.");
        }

        using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
        await _variationRequestRepository.UpdateVariationContractorContingency(new UpdateVariationContractorContingencyParameters
        {
            VariationRequestId = (Guid)currentVariationRequestId,
            UsedContractorContingency = request.UsedVariationContractorContingency == Enums.ENoYes.Yes,
            UsedContractorContingencyDescription = request.ContractorContingencyAdditionalNotes

        });


        scope.Complete();

        return Unit.Value;
    }
}
