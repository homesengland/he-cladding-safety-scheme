using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Enums;
using HE.Remediation.Core.Interface;
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

        var isExpired = await _paymentRequestRepository.IsPaymentRequestExpired(paymentRequestId);
        
        return new GetReviewPaymentResponse
        {
            IsSubmitted = paymentDetails is null ? false : paymentDetails.IsSubmitted,             
            IsExpired = isExpired,
            ChangeToMonthlyCost = changeToMonthlyCost,
            TotalGrantFunding = costs.TotalGrantFunding,
            GrantPaidToDate = costs.TotalGrantPaidToDate,
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
