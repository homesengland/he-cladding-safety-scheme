using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Enums;
using HE.Remediation.Core.Exceptions;
using HE.Remediation.Core.Interface;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.VariationRequest.CostProfile.Get;

public class GetCostProfileHandler : IRequestHandler<GetCostProfileRequest, GetCostProfileResponse>
{
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IApplicationRepository _applicationRepository;
    private readonly IBuildingDetailsRepository _buildingDetailsRepository;
    private readonly IVariationRequestRepository _variationRequestRepository;

    public GetCostProfileHandler(IApplicationDataProvider applicationDataProvider,
                                 IBuildingDetailsRepository buildingDetailsRepository,
                                 IApplicationRepository applicationRepository,
                                 IVariationRequestRepository variationRequestRepository,
                                 IScheduleOfWorksRepository scheduleOfWorksRepository)
    {
        _applicationDataProvider = applicationDataProvider;
        _buildingDetailsRepository = buildingDetailsRepository;
        _applicationRepository = applicationRepository;
        _variationRequestRepository = variationRequestRepository;
    }

    public async Task<GetCostProfileResponse> Handle(GetCostProfileRequest request, CancellationToken cancellationToken)
    {
        var applicationId = _applicationDataProvider.GetApplicationId();

        var applicationReferenceNumber = await _applicationRepository.GetApplicationReferenceNumber(applicationId);
        var buildingName = await _buildingDetailsRepository.GetBuildingUniqueName(applicationId);

        var currentVariationRequestId = await _variationRequestRepository.GetCurrentVariationRequestId();

        var overview = await _variationRequestRepository.GetOverview(currentVariationRequestId);
        var variationReason = await _variationRequestRepository.GetVariationReason();

        var costsProfile = await _variationRequestRepository.GetCostsProfile(currentVariationRequestId);

        var costsProfileResult = costsProfile is not null
            ? costsProfile
                .Select(x => new GetCostProfileResultItem
                {
                    ItemName = x.ItemName,
                    Type = x.Type,
                    Amount = x.Amount,
                    PaymentStatus = x.PaymentStatus
                }).ToList()
            : new List<GetCostProfileResultItem>();

        var total = costsProfileResult
            .Where(x => x.Amount is not null && x.PaymentStatus != EPaymentStatus.Missed && x.PaymentStatus != EPaymentStatus.Rejected)
            .Sum(x => x.Amount);

        return new GetCostProfileResponse
        {
            BuildingName = buildingName,
            ApplicationReferenceNumber = applicationReferenceNumber,
            ApprovedGrantFunding = overview?.TotalGrantFunding ?? 0,
            GrantPaidToDate = overview?.TotalGrantPaidToDate ?? 0,
            UnclaimedGrantFunding = overview?.TotalUnclaimedGrant ?? 0,
            ProjectDuration = overview?.ProjectDurationInMonths ?? 0,
            CostsProfile = costsProfileResult,
            TotalAmount = total,
            IsThirdPartyContributionVariation = variationReason?.IsThirdPartyContributionVariation,
        };
    }
}
