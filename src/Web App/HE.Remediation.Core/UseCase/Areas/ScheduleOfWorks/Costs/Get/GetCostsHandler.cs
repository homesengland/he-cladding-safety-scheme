using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Data.StoredProcedureResults.Costs;
using HE.Remediation.Core.Helpers;
using HE.Remediation.Core.Interface;
using HE.Remediation.Core.UseCase.Shared.Costs.Get;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.ScheduleOfWorks.Costs.Get;

public class GetCostsHandler : IRequestHandler<GetCostsRequest, GetCostsResponse>
{
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IApplicationRepository _applicationRepository;
    private readonly IBuildingDetailsRepository _buildingDetailsRepository;
    private readonly IScheduleOfWorksRepository _scheduleOfWorksRepository;

    public GetCostsHandler(IApplicationDataProvider applicationDataProvider,
                           IBuildingDetailsRepository buildingDetailsRepository,
                           IApplicationRepository applicationRepository,
                           IScheduleOfWorksRepository scheduleOfWorksRepository                           )
    {
        _applicationDataProvider = applicationDataProvider;
        _buildingDetailsRepository = buildingDetailsRepository;
        _applicationRepository = applicationRepository;
        _scheduleOfWorksRepository = scheduleOfWorksRepository;
    }

    public async Task<GetCostsResponse> Handle(GetCostsRequest request, CancellationToken cancellationToken)
    {
        var applicationId = _applicationDataProvider.GetApplicationId();        

        var applicationReferenceNumber = await _applicationRepository.GetApplicationReferenceNumber(applicationId);
        var buildingName = await _buildingDetailsRepository.GetBuildingUniqueName(applicationId);
        var isSubmitted = await _scheduleOfWorksRepository.IsScheduleOfWorksSubmitted();

        await _scheduleOfWorksRepository.CreateCosts();

        var costs = await _scheduleOfWorksRepository.GetCosts();

        if(costs is not null)
        {
            TruncateCosts(costs);
        }

        var overview = await _scheduleOfWorksRepository.GetOverview();

        var calculatedCosts = CostsCalculationHelper.CalculateMonthlyCosts(new MonthlyCostsCalculationRequest
        {
            ApprovedGrantFunding = costs?.ApprovedGrantFunding,
            GrantPaidToDate = costs?.GrantPaidToDate,
            MonthlyCosts = costs?.MonthlyCosts.Select(x => x.Amount ?? 0)
        });

        return new GetCostsResponse
        {
            BuildingName = buildingName,
            ApplicationReferenceNumber = applicationReferenceNumber,
            TotalGrantFunding = overview?.TotalGrantFunding,
            ProjectStartDate = costs?.ProjectStartDate,
            ProjectEndDate = costs?.ProjectEndDate,
            ApprovedGrantFunding = costs?.ApprovedGrantFunding,
            ThirdPartyContribution = costs?.ThirdPartyContribution,
            PtfsPayment = costs?.PtfsPayment,
            IsPtfsPaymentPaid = costs?.IsPtfsPaymentPaid ?? false,
            GrantPaidToDate = costs?.GrantPaidToDate,
            MonthlyCosts = costs?.MonthlyCosts.ToList() ?? new List<MonthlyCostResult>(),
            MonthlyCostsTotal = calculatedCosts.TotalMonthlyCosts,
            TotalGrantPaidToDate = costs?.GrantPaidToDate,
            UnprofiledGrantFunding = calculatedCosts.UnprofiledAmount,
            IsSubmitted = isSubmitted,
            IsAdditionalPtfsPaid = costs?.IsAdditionalPtfsPaid ?? false,
            AdditionalPtfsPayment = costs?.AdditionalPtfsPayment
        };
    }

    private static void TruncateCosts(CostsResult costs)
    {
        costs.ApprovedGrantFunding = costs.ApprovedGrantFunding.HasValue ? Math.Truncate(costs.ApprovedGrantFunding.Value) : null;
        costs.GrantPaidToDate = costs.GrantPaidToDate.HasValue ? Math.Truncate(costs.GrantPaidToDate.Value) : null;
        costs.PtfsPayment = costs.PtfsPayment.HasValue ? Math.Truncate(costs.PtfsPayment.Value) : null;
        costs.AdditionalPtfsPayment = costs.AdditionalPtfsPayment.HasValue ? Math.Truncate(costs.AdditionalPtfsPayment.Value) : null;

        foreach (var month in costs.MonthlyCosts)
        {
            if (month.Amount.HasValue)
            {
                month.Amount = Math.Truncate(month.Amount.Value);
            }
        }
    }
}
