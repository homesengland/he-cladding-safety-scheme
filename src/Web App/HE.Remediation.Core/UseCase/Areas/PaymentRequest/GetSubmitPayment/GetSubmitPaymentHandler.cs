using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Enums;
using HE.Remediation.Core.Exceptions;
using HE.Remediation.Core.Helpers;
using HE.Remediation.Core.Interface;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.PaymentRequest.GetSubmitPayment;

public class GetSubmitPaymentHandler : IRequestHandler<GetSubmitPaymentRequest, GetSubmitPaymentResponse>
{
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IApplicationRepository _applicationRepository;
    private readonly IBuildingDetailsRepository _buildingDetailsRepository;
    private readonly IPaymentRequestRepository _paymentRequestRepository;

    public GetSubmitPaymentHandler(IApplicationDataProvider applicationDataProvider,
                           IBuildingDetailsRepository buildingDetailsRepository,
                           IApplicationRepository applicationRepository,
                           IPaymentRequestRepository paymentRequestRepository)
    {
        _applicationDataProvider = applicationDataProvider;
        _buildingDetailsRepository = buildingDetailsRepository;
        _applicationRepository = applicationRepository;
        _paymentRequestRepository = paymentRequestRepository;   
    }

    public async Task<GetSubmitPaymentResponse> Handle(GetSubmitPaymentRequest request, CancellationToken cancellationToken)
    {
        var applicationId = _applicationDataProvider.GetApplicationId();
        var paymentRequestId = _applicationDataProvider.GetPaymentRequestId();
        
        var applicationReferenceNumber = await _applicationRepository.GetApplicationReferenceNumber(applicationId);
        var buildingName = await _buildingDetailsRepository.GetBuildingUniqueName(applicationId);
        var isSubmitted = await _paymentRequestRepository.IsPaymentRequestSubmitted(paymentRequestId);
        var isExpired = await _paymentRequestRepository.IsPaymentRequestExpired(paymentRequestId);
        var isItALastSchedulePayment = await _paymentRequestRepository.IsItALastScheduledPayment(applicationId, paymentRequestId);

        var overview = await _paymentRequestRepository.GetOverview();
        var costsProfile = await _paymentRequestRepository.GetCostsProfile();

        if (costsProfile is null)
        {
            throw new EntityNotFoundException("Cannot locate cost profile for schedule of work.");
        }

        var currentPayment = costsProfile.Where(x => x.Type == EPaymentRequestCostType.CurrentPayment).FirstOrDefault();
        var finalPayment = costsProfile.Where(x => x.Type == EPaymentRequestCostType.FinalPayment).FirstOrDefault();
        var additionalPayment = costsProfile.FirstOrDefault(x => x.Type == EPaymentRequestCostType.AdditionalPayment);

        var paidCosts = costsProfile.Where(x => ((x.Type == EPaymentRequestCostType.GrantPaidToDate) &&
                                                 (x.IsApproved == true)))
                                    .Select(x => new MonthlyCost 
                                    { 
                                        Id = x.Id, 
                                        Amount = x.Value, 
                                        MonthName = x.ItemName, 
                                        Paid = x.Paid,
                                        IsApproved = x.IsApproved == true
                                    }).ToList();

        var missedPayments = costsProfile.Where(x => ((x.Type == EPaymentRequestCostType.GrantPaidToDate) &&
                                                      (x.Paid == false && (x.IsApproved == null || x.IsApproved == false))))
                                         .Select(x => new MonthlyCost
                                         { 
                                            Id = x.Id, 
                                            Amount = x.Value, 
                                            MonthName = x.ItemName, 
                                            Paid = x.Paid,
                                            Status = x.IsApproved == false ? EPaymentRequestStatus.Rejected : EPaymentRequestStatus.Missed
                                         }).ToList();

        var missedPaymentTotal = missedPayments.Select(x => x.Amount).Sum() ?? 0;
        
        var monthlyCosts = costsProfile.Where(x => x.Type == EPaymentRequestCostType.MonthlyCosts).Select(x => new MonthlyCost { Id = x.Id, Amount = x.Value, MonthName = x.ItemName, Paid = x.Paid }).ToList();

        var totalMonthlyCost = monthlyCosts.Select(x => x.Amount).Sum() ?? 0;

        var projectDuration = await _paymentRequestRepository.GetPaymentRequestProjectDuration(paymentRequestId);
        int projectDurationInMonths = projectDuration ?? 0;        
                
        var finalMonthTotal = finalPayment?.ConfirmedValue ?? 0;

        var calculatedCosts = CostsCalculationHelper.CalculateMonthlyCosts(new MonthlyCostsCalculationRequest
        {
            ApprovedGrantFunding = overview.TotalGrantFunding,
            MonthlyCosts = monthlyCosts.Select(x => x.Amount ?? 0),
            AdditionalCost = additionalPayment?.ConfirmedValue ?? 0,
            CurrentCost = currentPayment?.Value ?? 0,
            FinalCost = finalPayment?.ConfirmedValue ?? 0,
            GrantPaidToDate = overview?.TotalGrantPaidToDate
        });        

        return new GetSubmitPaymentResponse
        {
            BuildingName = buildingName,
            ApplicationReferenceNumber = applicationReferenceNumber,
            TotalGrantFunding = overview?.TotalGrantFunding,
            ApprovedGrantFunding = overview.TotalGrantFunding,
            GrantPaidToDate = overview?.TotalGrantPaidToDate,
            MonthlyCosts = monthlyCosts,
            MonthlyCostsTotal = totalMonthlyCost,
            TotalGrantPaidToDate = (overview?.TotalGrantPaidToDate ?? 0),
            UnprofiledGrantFunding = calculatedCosts.UnprofiledAmount,
            IsSubmitted = isSubmitted,
            IsExpired = isExpired,
            ProjectDuration = projectDurationInMonths,
            CurrentMonth = new MonthlyCost
            {                
                Id = currentPayment?.Id ?? Guid.NewGuid(),
                MonthName = currentPayment?.ItemName,
                Amount = (currentPayment?.Value ?? 0),
                Paid = currentPayment?.Paid
            },
            FinalMonthCost = new MonthlyCost
            {
                Amount = (finalPayment?.ConfirmedValue ?? 0),
                Paid = currentPayment?.Paid
            },
            CurrentMonthTotal = currentPayment?.Value + (additionalPayment?.ConfirmedValue ?? 0),
            AdditionalCost = additionalPayment == null ? null : new MonthlyCost
            {
                Id = additionalPayment.Id,
                Amount = additionalPayment?.ConfirmedValue,
                MonthName = additionalPayment?.ItemName,
                Paid = additionalPayment?.Paid
            },
            PaidCosts = paidCosts,
            MissedPayments = missedPayments,
            MissedPaymentTotal = missedPaymentTotal,
            FinalMonthTotal = finalMonthTotal,
            IsItALastSchedulePayment = isItALastSchedulePayment
    };
    }
}
