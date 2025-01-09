using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Data.StoredProcedureResults.VariationRequest;
using HE.Remediation.Core.Interface;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.VariationRequest.VariationReason.Get;

public class GetVariationReasonHandler : IRequestHandler<GetVariationReasonRequest, GetVariationReasonResponse>
{
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IApplicationRepository _applicationRepository;
    private readonly IBuildingDetailsRepository _buildingDetailsRepository;
    private readonly IVariationRequestRepository _variationRequestRepository;

    public GetVariationReasonHandler(IApplicationDataProvider applicationDataProvider,
                                     IBuildingDetailsRepository buildingDetailsRepository,
                                     IApplicationRepository applicationRepository,
                                     IVariationRequestRepository variationRequestRepository)
    {
        _applicationDataProvider = applicationDataProvider;
        _buildingDetailsRepository = buildingDetailsRepository;
        _applicationRepository = applicationRepository;
        _variationRequestRepository = variationRequestRepository;
    }

    public async Task<GetVariationReasonResponse> Handle(GetVariationReasonRequest request, CancellationToken cancellationToken)
    {
        var applicationId = _applicationDataProvider.GetApplicationId();

        var applicationReferenceNumber = await _applicationRepository.GetApplicationReferenceNumber(applicationId);
        var buildingName = await _buildingDetailsRepository.GetBuildingUniqueName(applicationId);

        var variationRequestId = await _variationRequestRepository.GetLatestVariationRequestId();
        var isSubmitted = await _variationRequestRepository.IsVariationRequestSubmitted();
        var approvalStatus = await _variationRequestRepository.GetApprovalStatus();

        OverviewResult overview;
        GetVariationReasonResult variationReason;

        var isNewVariationRequest = variationRequestId is null 
            || (isSubmitted && approvalStatus is not null &&
                (approvalStatus == Enums.EVariationRequestApprovalStatus.Approved ||
                 approvalStatus == Enums.EVariationRequestApprovalStatus.Rejected));

        if (isNewVariationRequest)
        {
            variationReason = null;
            overview = await _variationRequestRepository.GetOverview(null);
            isSubmitted = false;
        }
        else
        {
            variationReason = await _variationRequestRepository.GetVariationReason();
            overview = await _variationRequestRepository.GetOverview(variationRequestId);
        }

        return new GetVariationReasonResponse
        {
            BuildingName = buildingName,
            ApplicationReferenceNumber = applicationReferenceNumber,
            IsSubmitted = isSubmitted,
            IsTimescaleVariation = variationReason?.IsTimescaleVariation,
            IsScopeVariation = variationReason?.IsScopeVariation,
            IsCostVariation = variationReason?.IsCostVariation,
            IsThirdPartyContributionVariation = variationReason?.IsThirdPartyContributionVariation,
            ApprovedGrantFunding = overview?.TotalGrantFunding ?? 0,
            GrantPaidToDate = overview?.TotalGrantPaidToDate ?? 0,
            UnclaimedGrantFunding = overview?.TotalUnclaimedGrant ?? 0
        };
    }
}
