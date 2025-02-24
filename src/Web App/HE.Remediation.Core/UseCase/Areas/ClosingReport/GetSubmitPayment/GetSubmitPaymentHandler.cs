using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Enums;
using HE.Remediation.Core.Exceptions;
using HE.Remediation.Core.Helpers;
using HE.Remediation.Core.Interface;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.ClosingReport.GetSubmitPayment;

public class GetSubmitPaymentHandler : IRequestHandler<GetSubmitPaymentRequest, GetSubmitPaymentResponse>
{
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IApplicationRepository _applicationRepository;
    private readonly IBuildingDetailsRepository _buildingDetailsRepository;
    private readonly IPaymentRequestRepository _paymentRequestRepository;
    private readonly IClosingReportRepository _closingReportRepository;

    public GetSubmitPaymentHandler(IApplicationDataProvider applicationDataProvider,
                                   IBuildingDetailsRepository buildingDetailsRepository,
                                   IApplicationRepository applicationRepository,
                                   IPaymentRequestRepository paymentRequestRepository,
                                   IClosingReportRepository closingReportRepository)
    {
        _applicationDataProvider = applicationDataProvider;
        _buildingDetailsRepository = buildingDetailsRepository;
        _applicationRepository = applicationRepository;
        _paymentRequestRepository = paymentRequestRepository;
        _closingReportRepository = closingReportRepository;
    }

    public async Task<GetSubmitPaymentResponse> Handle(GetSubmitPaymentRequest request, CancellationToken cancellationToken)
    {
        var applicationId = _applicationDataProvider.GetApplicationId();
        
        var applicationReferenceNumber = await _applicationRepository.GetApplicationReferenceNumber(applicationId);
        var buildingName = await _buildingDetailsRepository.GetBuildingUniqueName(applicationId);
        var isSubmitted = await _closingReportRepository.IsClosingReportSubmitted(applicationId);

        decimal totalGrantPaidToDate = 0.0m;
        decimal totalGrantFunding = 0.0m;

        var paymentRequestId = await _closingReportRepository.GetPaymentRequestIDForClosingReport(applicationId);
        if (paymentRequestId is not null)
        {
            var overview = await _paymentRequestRepository.GetOverview(paymentRequestId.Value);
            totalGrantPaidToDate = overview is null ? 0 : overview.TotalGrantPaidToDate;
            totalGrantFunding = overview is null ? 0 : overview.TotalGrantFunding;
        }
        
        var costsProfile = await _closingReportRepository.GetClosingReportCostProfile(applicationId);

        if (costsProfile is null)
        {
            throw new EntityNotFoundException("Cannot locate cost profile for schedule of work.");
        }        

        var paidCosts = costsProfile.Where(x => x.Type == EPaymentRequestCostType.GrantPaidToDate)
                                    .Select(x => new MonthlyCost
                                    {
                                        Id = x.Id,
                                        Amount = x.Value,
                                        MonthName = x.ItemName,
                                        Paid = x.Paid
                                    }).ToList();
        var finalPayment = costsProfile.SingleOrDefault(x => x.Type == EPaymentRequestCostType.CurrentPayment);
        
        var projectDuration = 0;
        if (costsProfile != null)
        {
            projectDuration = costsProfile.Where(x => (x.Type == EPaymentRequestCostType.GrantPaidToDate) &&
                                                      (!x.ItemName.Contains("Additional"))).Count();
        }

        var subContractorsRequired = await _closingReportRepository.GetSubContractorsRequired(applicationId);

        var calculatedCosts = CostsCalculationHelper.CalculateMonthlyCosts(new MonthlyCostsCalculationRequest
        {
            ApprovedGrantFunding = totalGrantFunding,
            CurrentCost = finalPayment?.Value ?? 0,
            GrantPaidToDate = totalGrantPaidToDate
        });

        return new GetSubmitPaymentResponse
        {
            BuildingName = buildingName,
            ApplicationReferenceNumber = applicationReferenceNumber,
            TotalGrantFunding = totalGrantFunding,
            ApprovedGrantFunding = totalGrantFunding,
            GrantPaidToDate = totalGrantPaidToDate,
            ProjectDuration = projectDuration,
            PaidCosts = paidCosts,
            FinalMonthCost = new MonthlyCost
            {
                Id = finalPayment.Id,
                Amount = finalPayment.Value,
                MonthName = finalPayment.ItemName
            },
            FinalMonthTotal = finalPayment.Value,
            SubcontractorsRequired = subContractorsRequired,
            UnprofiledGrantFunding = calculatedCosts.UnprofiledAmount,
            IsSubmitted = isSubmitted
    };
    }
}
