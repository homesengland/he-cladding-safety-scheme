using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Data.StoredProcedureResults.VariationRequest;
using HE.Remediation.Core.Exceptions;
using HE.Remediation.Core.Interface;
using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.VariationRequest.Costs.Get;

public class GetCostsHandler : IRequestHandler<GetCostsRequest, GetCostsResponse>
{
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IApplicationRepository _applicationRepository;
    private readonly IBuildingDetailsRepository _buildingDetailsRepository;
    private readonly IVariationRequestRepository _variationRequestRepository;
    private readonly IWorkPackageRepository _workPackageRepository;

    public GetCostsHandler(IApplicationDataProvider applicationDataProvider,
                                 IBuildingDetailsRepository buildingDetailsRepository,
                                 IApplicationRepository applicationRepository,
                                 IVariationRequestRepository variationRequestRepository,
                                 IWorkPackageRepository workPackageRepository)
    {
        _applicationDataProvider = applicationDataProvider;
        _buildingDetailsRepository = buildingDetailsRepository;
        _applicationRepository = applicationRepository;
        _variationRequestRepository = variationRequestRepository;
        _workPackageRepository = workPackageRepository;
    }

    public async ValueTask<GetCostsResponse> Handle(GetCostsRequest request, CancellationToken cancellationToken)
    {
        var applicationId = _applicationDataProvider.GetApplicationId();

        var applicationReferenceNumber = await _applicationRepository.GetApplicationReferenceNumber(applicationId);
        var buildingName = await _buildingDetailsRepository.GetBuildingUniqueName(applicationId);
        var isSubmitted = await _variationRequestRepository.IsVariationRequestSubmitted();

        var variationRequestId = await _variationRequestRepository.GetLatestVariationRequestId();

        if (!variationRequestId.HasValue)
        {
            throw new EntityNotFoundException(
                 "No valid Variation Request found for this Application.");
        }

        var variationCosts = await _variationRequestRepository.GetVariationCosts(variationRequestId.Value);

        var variationReason = await _variationRequestRepository.GetVariationReason();

        if (variationCosts is null && !variationReason.IsCostVariation.GetValueOrDefault(false))
        {
            return new GetCostsResponse
            {
                HasCostVariation = false,
                IsThirdPartyContributionVariation = variationReason?.IsThirdPartyContributionVariation
            };
        }

        if (variationCosts is null)
        {
            Guid? workPackageCostsId;

            //gets latest, non-draft WP Costs Id
            workPackageCostsId = await _workPackageRepository.GetLatestWorkPackageCostsId();

            await _variationRequestRepository.InsertVariationCosts(variationRequestId.Value, workPackageCostsId.Value);

            variationCosts = await _variationRequestRepository.GetVariationCosts(variationRequestId.Value);
        }

        var workPackageCosts = await _workPackageRepository.GetWorkPackageCostsByVariationRequestId((Guid)variationRequestId);

        return new GetCostsResponse
        {
            ApplicationReferenceNumber = applicationReferenceNumber,
            BuildingName = buildingName,
            IsSubmitted = isSubmitted,

            UnsafeCladdingRemovalAmount = workPackageCosts.RemovalOfCladdingAmount,
            NewCladdingAmount = workPackageCosts.NewCladdingAmount,
            ExternalWorksAmount = workPackageCosts.OtherEligibleWorkToExternalWallAmount,
            InternalWorksAmount = workPackageCosts.InternalMitigationWorksAmount,
            MainContractorPreliminariesAmount = workPackageCosts.MainContractorPreliminariesAmount,
            AccessAmount = workPackageCosts.AccessAmount,
            MainContractorOverheadAmount = workPackageCosts.OverheadsAndProfitAmount,
            ContractorContingenciesAmount = workPackageCosts.ContractorContingenciesAmount,
            FraewSurveyAmount = workPackageCosts.FraewSurveyAmount,
            FeasibilityStageAmount = workPackageCosts.FeasibilityStageAmount,
            PostTenderStageAmount = workPackageCosts.PostTenderStageAmount,
            PropertyManagerAmount = workPackageCosts.PropertyManagerAmount,
            IrrecoverableVatAmount = workPackageCosts.IrrecoverableVatAmount,

            VariationUnsafeCladdingRemovalAmount = variationCosts?.RemovalOfCladdingAmount,
            VariationNewCladdingAmount = variationCosts?.NewCladdingAmount,
            VariationExternalWorksAmount = variationCosts?.OtherEligibleWorkToExternalWallAmount,
            VariationInternalWorksAmount = variationCosts?.InternalMitigationWorksAmount,
            VariationMainContractorPreliminariesAmount = variationCosts?.MainContractorPreliminariesAmount,
            VariationAccessAmount = variationCosts?.AccessAmount,
            VariationMainContractorOverheadAmount = variationCosts?.OverheadsAndProfitAmount,
            VariationContractorContingenciesAmount = variationCosts?.ContractorContingenciesAmount,
            VariationFraewSurveyAmount = variationCosts?.FraewSurveyAmount,
            VariationFeasibilityStageAmount = variationCosts?.FeasibilityStageAmount,
            VariationPostTenderStageAmount = variationCosts?.PostTenderStageAmount,
            VariationPropertyManagerAmount = variationCosts?.PropertyManagerAmount,
            VariationIrrecoverableVatAmount = variationCosts?.IrrecoverableVatAmount,

            IsThirdPartyContributionVariation = variationReason?.IsThirdPartyContributionVariation
        };
    }
}