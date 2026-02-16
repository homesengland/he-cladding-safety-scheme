using System.Transactions;
using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Data.StoredProcedureParameters.VariationRequest;
using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.VariationRequest.Declaration.Set;

public class SetDeclarationHandler : IRequestHandler<SetDeclarationRequest>
{
    private readonly IVariationRequestRepository _variationRequestRepository;

    public SetDeclarationHandler(IVariationRequestRepository variationRequestRepository)
    {
        _variationRequestRepository = variationRequestRepository;
    }

    public async ValueTask<Unit> Handle(SetDeclarationRequest request, CancellationToken cancellationToken)
    {
        using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

        var declarationParams = new UpdateDeclarationParameters
        {
            ConfirmedAwareOfApproval = request?.ConfirmedAwareOfApproval,
            ConfirmedCostsReasonable = request?.ConfirmedCostsReasonable,
            ConfirmedCoversRecommendations = request?.ConfirmedCoversRecommendations
        };

        await _variationRequestRepository.UpdateDeclaration(declarationParams);

        scope.Complete();

        return Unit.Value;
    }
}