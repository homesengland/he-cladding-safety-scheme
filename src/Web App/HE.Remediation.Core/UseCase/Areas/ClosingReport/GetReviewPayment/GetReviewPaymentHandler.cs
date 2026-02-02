using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Enums;
using HE.Remediation.Core.Helpers;
using HE.Remediation.Core.Interface;
using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.ClosingReport.GetReviewPayment;

public class GetReviewPaymentHandler : IRequestHandler<GetReviewPaymentRequest, GetReviewPaymentResponse>
{
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IApplicationRepository _applicationRepository;
    private readonly IBuildingDetailsRepository _buildingDetailsRepository;
    private readonly IPaymentRequestRepository _paymentRequestRepository;
    private readonly IClosingReportRepository _closingReportRepository;

    public GetReviewPaymentHandler(IApplicationDataProvider applicationDataProvider,
                                   IApplicationRepository applicationRepository,
                                   IBuildingDetailsRepository buildingDetailsRepository,
                                   IPaymentRequestRepository paymentRequestRepository,
                                   IClosingReportRepository closingReportRepository)
    {
        _applicationDataProvider = applicationDataProvider;
        _applicationRepository = applicationRepository;
        _buildingDetailsRepository = buildingDetailsRepository;
        _paymentRequestRepository = paymentRequestRepository;
        _closingReportRepository = closingReportRepository;
    }

    public async ValueTask<GetReviewPaymentResponse> Handle(GetReviewPaymentRequest request, CancellationToken cancellationToken)
    {
        var applicationId = _applicationDataProvider.GetApplicationId();

        var applicationReferenceNumber = await _applicationRepository.GetApplicationReferenceNumber(applicationId);
        var buildingName = await _buildingDetailsRepository.GetBuildingUniqueName(applicationId);
        var isSubmitted = await _closingReportRepository.IsClosingReportSubmitted(applicationId);

        var costsProfile = await GetCalculatedCosts(applicationId);

        var paymentOverviewDetails = await _closingReportRepository.GetClosingReportReviewPaymentOverview(applicationId);

        return new GetReviewPaymentResponse
        {
            IsSubmitted = isSubmitted,
            ChangeToMonthlyCost = paymentOverviewDetails.CostsChanged,
            TotalGrantFunding = paymentOverviewDetails.TotalGrantFunding,
            GrantPaidToDate = paymentOverviewDetails.GrantPaidToDate,
            PaymentRequestName = "Final payment request",
            RequestedAmount = paymentOverviewDetails.RequestedAmount,
            ScheduledAmount = paymentOverviewDetails.ScheduledAmount,
            ReasonForChange = paymentOverviewDetails.ReasonForChange,
            BuildingName = buildingName,
            ApplicationReferenceNumber = applicationReferenceNumber,
            UnprofiledFunding = costsProfile?.UnprofiledAmount
        };
    }

    private async ValueTask<MonthlyCostsCalculationResult> GetCalculatedCosts(Guid applicationId)
    {
        decimal totalGrantPaidToDate = 0.0m;
        decimal totalGrantFunding = 0.0m;

        var paymentRequestId = await _closingReportRepository.GetPaymentRequestIDForClosingReport(applicationId);

        if (paymentRequestId is not null)
        {
            var overview = await _paymentRequestRepository.GetOverview(paymentRequestId.Value);
            totalGrantPaidToDate = overview is null ? 0 : overview.TotalGrantPaidToDate;
            totalGrantFunding = overview is null ? 0 : overview.TotalGrantFunding;
        }
        else
        {
            return null;
        }

        var costsProfile = await _closingReportRepository.GetClosingReportCostProfile(applicationId);

        if (costsProfile is null)
        {
            return null;
        }

        var finalPayment = costsProfile.SingleOrDefault(x => x.Type == EPaymentRequestCostType.CurrentPayment);

        return CostsCalculationHelper.CalculateMonthlyCosts(new MonthlyCostsCalculationRequest
        {
            ApprovedGrantFunding = totalGrantFunding,
            CurrentCost = finalPayment?.Value ?? 0,
            GrantPaidToDate = totalGrantPaidToDate
        });
    }
}
