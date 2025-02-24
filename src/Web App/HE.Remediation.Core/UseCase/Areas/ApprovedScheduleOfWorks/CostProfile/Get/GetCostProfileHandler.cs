using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Enums;
using HE.Remediation.Core.Interface;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.ApprovedScheduleOfWorks.CostProfile.Get;

public class GetCostProfileHandler : IRequestHandler<GetCostProfileRequest, GetCostProfileResponse>
{
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IApplicationRepository _applicationRepository;
    private readonly IBuildingDetailsRepository _buildingDetailsRepository;
    private readonly IScheduleOfWorksRepository _scheduleOfWorksRepository;

    public GetCostProfileHandler(IApplicationDataProvider applicationDataProvider,
                                 IBuildingDetailsRepository buildingDetailsRepository,
                                 IApplicationRepository applicationRepository,
                                 IScheduleOfWorksRepository scheduleOfWorksRepository)
    {
        _applicationDataProvider = applicationDataProvider;
        _buildingDetailsRepository = buildingDetailsRepository;
        _applicationRepository = applicationRepository;
        _scheduleOfWorksRepository = scheduleOfWorksRepository;
    }

    public async Task<GetCostProfileResponse> Handle(GetCostProfileRequest request, CancellationToken cancellationToken)
    {
        var applicationId = _applicationDataProvider.GetApplicationId();

        var applicationReferenceNumber = await _applicationRepository.GetApplicationReferenceNumber(applicationId);
        var buildingName = await _buildingDetailsRepository.GetBuildingUniqueName(applicationId);

        var overview = await _scheduleOfWorksRepository.GetApprovedScheduleOfWorksOverview();
        var costsProfile = await _scheduleOfWorksRepository.GetApprovedApplicationScheduleOfWorksCosts();

        var totalSubmittedPaidToDateValue = costsProfile is not null
            ? costsProfile.Where(x => x.Value is not null && 
                                x.Type == EScheduleOfWorksCostType.GrantPaidToDate && 
                                (x.Status == EPaymentStatus.Approved || x.Status == EPaymentStatus.Paid || x.Status == EPaymentStatus.Recommended))
            .Sum(x => x.Value)
            : 0;

        var totalSubmittedPaidToDateConfirmedValue = costsProfile is not null
            ? costsProfile.Where(x => x.ConfirmedValue is not null &&
                                x.Type == EScheduleOfWorksCostType.GrantPaidToDate &&
                                (x.Status == EPaymentStatus.Approved || x.Status == EPaymentStatus.Paid || x.Status == EPaymentStatus.Recommended))
            .Sum(x => x.ConfirmedValue)
            : 0;

        var totalMonthlyAmountValue = costsProfile is not null
            ? costsProfile.Where(x => x.Value is not null && x.Type == EScheduleOfWorksCostType.MonthlyCosts)
            .Sum(x => x.Value)
            : 0;

        var totalMonthlyAmountConfirmedValue = costsProfile is not null
            ? costsProfile
            .Where(x => x.ConfirmedValue is not null && x.Type == EScheduleOfWorksCostType.MonthlyCosts)
            .Sum(x => x.ConfirmedValue)
            : 0;

        var finalCostConfirmed = costsProfile?.Single(x => x.Type == EScheduleOfWorksCostType.FinalPayment).ConfirmedValue ?? 0;

        var finalCostValue = costsProfile?.Single(x => x.Type == EScheduleOfWorksCostType.FinalPayment).Value ?? 0;

        return new GetCostProfileResponse
        {
            BuildingName = buildingName,
            ApplicationReferenceNumber = applicationReferenceNumber,
            TotalGrantFunding = overview?.TotalGrantFunding ?? 0,
            TotalGrantPaidToDate = overview?.TotalGrantPaidToDate ?? 0,
            TotalUnclaimedGrant = overview?.TotalUnclaimedGrant ?? 0,
            ProjectDuration = overview?.ProjectDurationInMonths ?? 0,
            TotalConfirmedValue = (totalSubmittedPaidToDateConfirmedValue + totalMonthlyAmountConfirmedValue + finalCostConfirmed),
            TotalSubmittedValue = (totalSubmittedPaidToDateValue + totalMonthlyAmountValue + finalCostValue),
            CostsProfile = costsProfile
        };
    }
}
