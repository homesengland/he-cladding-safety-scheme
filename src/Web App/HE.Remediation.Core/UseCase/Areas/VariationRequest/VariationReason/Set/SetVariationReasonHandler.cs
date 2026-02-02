using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Data.StoredProcedureParameters.VariationRequest;
using Mediator;
using System.Transactions;
using HE.Remediation.Core.Enums;
using HE.Remediation.Core.Interface;
using HE.Remediation.Core.Services.StatusTransition;

namespace HE.Remediation.Core.UseCase.Areas.VariationRequest.VariationReason.Set;

public class SetVariationReasonHandler : IRequestHandler<SetVariationReasonRequest>
{
    private readonly IVariationRequestRepository _variationRequestRepository;
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IStatusTransitionService _statusTransitionService;

    public SetVariationReasonHandler(
        IVariationRequestRepository variationRequestRepository, 
        IApplicationDataProvider applicationDataProvider, 
        IStatusTransitionService statusTransitionService)
    {
        _variationRequestRepository = variationRequestRepository;
        _applicationDataProvider = applicationDataProvider;
        _statusTransitionService = statusTransitionService;
    }

    public async ValueTask<Unit> Handle(SetVariationReasonRequest request, CancellationToken cancellationToken)
    {
        using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

        var applicationId = _applicationDataProvider.GetApplicationId();

        await _variationRequestRepository.UpsertVariationSelection(new UpsertVariationSelectionParameters
        {
            IsScopeVariation = request.IsScopeVariation,
            IsCostVariation = request.IsCostVariation,
            IsTimescaleVariation = request.IsTimescaleVariation,
            IsThirdPartyContributionVariation = request.IsThirdPartyContributionVariation
        });

        await _statusTransitionService.TransitionToInternalStatus(EApplicationInternalStatus.VariationInProgress, applicationIds: applicationId);

        scope.Complete();

        return Unit.Value;
    }
}