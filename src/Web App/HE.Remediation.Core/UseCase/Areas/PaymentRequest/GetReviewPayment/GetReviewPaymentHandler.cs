using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Enums;
using HE.Remediation.Core.Helpers;
using HE.Remediation.Core.Interface;
using HE.Remediation.Core.UseCase.Areas.ClosingReport.GetSubmitPayment;

using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.PaymentRequest.GetReviewPayment;

public class GetReviewPaymentHandler : IRequestHandler<GetReviewPaymentRequest, GetReviewPaymentResponse>
{
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IApplicationRepository _applicationRepository;
    private readonly IBuildingDetailsRepository _buildingDetailsRepository;
    private readonly IPaymentRequestRepository _paymentRequestRepository;

    public GetReviewPaymentHandler(IApplicationDataProvider applicationDataProvider,
                                   IApplicationRepository applicationRepository,
                                   IBuildingDetailsRepository buildingDetailsRepository,
                                   IPaymentRequestRepository paymentRequestRepository)
    {
        _applicationDataProvider = applicationDataProvider;
        _applicationRepository = applicationRepository;
        _buildingDetailsRepository = buildingDetailsRepository;
        _paymentRequestRepository = paymentRequestRepository;
    }

    public async Task<GetReviewPaymentResponse> Handle(GetReviewPaymentRequest request, CancellationToken cancellationToken)
    {
        var applicationId = _applicationDataProvider.GetApplicationId();
        var paymentRequestId = _applicationDataProvider.GetPaymentRequestId();

        var applicationReferenceNumber = await _applicationRepository.GetApplicationReferenceNumber(applicationId);
        var buildingName = await _buildingDetailsRepository.GetBuildingUniqueName(applicationId);

        var costs = await _paymentRequestRepository.GetOverview();
        var costsProfile = await _paymentRequestRepository.GetCostsProfile();
        var paymentDetails = await _paymentRequestRepository.GetPaymentRequestDetails(applicationId, paymentRequestId);

        var changeToMonthlyCost = paymentDetails != null ?
                                  paymentDetails.PaymentRequestCost != paymentDetails.ScheduledAmountCost :
                                  false;

        var additionalPayment = costsProfile.FirstOrDefault(x => x.Type == EPaymentRequestCostType.AdditionalPayment);

        var monthlyCosts = costsProfile.Where(x => x.Type == EPaymentRequestCostType.MonthlyCosts).Select(x => new MonthlyCost { Id = x.Id, Amount = x.Value, MonthName = x.ItemName, Paid = x.Paid }).ToList();
        var currentPayment = costsProfile.Where(x => x.Type == EPaymentRequestCostType.CurrentPayment).FirstOrDefault();
        var finalPayment = costsProfile.Where(x => x.Type == EPaymentRequestCostType.FinalPayment).FirstOrDefault();

        var isExpired = await _paymentRequestRepository.IsPaymentRequestExpired(paymentRequestId);

        var calculatedCosts = CostsCalculationHelper.CalculateMonthlyCosts(new MonthlyCostsCalculationRequest
        {
            ApprovedGrantFunding = costs.TotalGrantFunding,
            MonthlyCosts = monthlyCosts.Select(x => x.Amount ?? 0),
            AdditionalCost = additionalPayment?.ConfirmedValue ?? 0,
            CurrentCost = currentPayment?.Value ?? 0,
            FinalCost = finalPayment?.ConfirmedValue ?? 0,
            GrantPaidToDate = costs?.TotalGrantPaidToDate
        });

        return new GetReviewPaymentResponse
        {
            IsSubmitted = paymentDetails is null ? false : paymentDetails.IsSubmitted,
            IsExpired = isExpired,
            ChangeToMonthlyCost = changeToMonthlyCost,
            TotalGrantFunding = costs.TotalGrantFunding,
            GrantPaidToDate = costs.TotalGrantPaidToDate,
            UnprofiledFunding = calculatedCosts.UnprofiledAmount,
            PaymentRequestName = paymentDetails?.CreatedDate.ToString("MMMM yyyy"),
            RequestedAmount = paymentDetails?.PaymentRequestCost,
            ScheduledAmount = paymentDetails?.ScheduledAmountCost,
            AdditionalCostTitle = additionalPayment?.ItemName,
            AdditionalCost = additionalPayment?.ConfirmedValue ?? additionalPayment?.Value,
            ReasonForChange = paymentDetails?.ReasonForChange,
            BuildingName = buildingName,
            ApplicationReferenceNumber = applicationReferenceNumber
        };
    }
}
