using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Data.StoredProcedureParameters.VariationRequest;
using MediatR;
using System.Transactions;
using HE.Remediation.Core.Enums;
using HE.Remediation.Core.Interface;

namespace HE.Remediation.Core.UseCase.Areas.VariationRequest.VariationReason.Set;

public class SetVariationReasonHandler : IRequestHandler<SetVariationReasonRequest>
{
    private readonly IVariationRequestRepository _variationRequestRepository;
    private readonly IApplicationRepository _applicationRepository;
    private readonly IApplicationDataProvider _applicationDataProvider;

    public SetVariationReasonHandler(IVariationRequestRepository variationRequestRepository, IApplicationRepository applicationRepository, IApplicationDataProvider applicationDataProvider)
    {
        _variationRequestRepository = variationRequestRepository;
        _applicationRepository = applicationRepository;
        _applicationDataProvider = applicationDataProvider;
    }

    public async Task<Unit> Handle(SetVariationReasonRequest request, CancellationToken cancellationToken)
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

        await _applicationRepository.UpdateInternalStatus(applicationId, EApplicationInternalStatus.VariationInProgress);

        scope.Complete();

        return Unit.Value;
    }
}