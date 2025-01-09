using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Interface;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.PaymentRequest.GetVariationRequired;

public class GetVariationRequiredHandler : IRequestHandler<GetVariationRequiredRequest, GetVariationRequiredResponse>
{
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IApplicationRepository _applicationRepository;
    private readonly IBuildingDetailsRepository _buildingDetailsRepository;
    private readonly IPaymentRequestRepository _paymentRequestRepository;

    public GetVariationRequiredHandler(IApplicationDataProvider applicationDataProvider,
                                       IApplicationRepository applicationRepository,
                                       IBuildingDetailsRepository buildingDetailsRepository,
                                       IPaymentRequestRepository paymentRequestRepository)
    {
        _applicationDataProvider = applicationDataProvider;
        _applicationRepository = applicationRepository;
        _buildingDetailsRepository = buildingDetailsRepository;
        _paymentRequestRepository = paymentRequestRepository;
    }

    public async Task<GetVariationRequiredResponse> Handle(GetVariationRequiredRequest request, CancellationToken cancellationToken)
    {
        var applicationId = _applicationDataProvider.GetApplicationId();
        var paymentRequestId = _applicationDataProvider.GetPaymentRequestId();

        var applicationReferenceNumber = await _applicationRepository.GetApplicationReferenceNumber(applicationId);
        var buildingName = await _buildingDetailsRepository.GetBuildingUniqueName(applicationId);

        var projectDetails = await _paymentRequestRepository.GetPaymentRequestProjectDetails(paymentRequestId);

        var isSubmitted = await _paymentRequestRepository.IsPaymentRequestSubmitted(paymentRequestId);
        var isExpired = await _paymentRequestRepository.IsPaymentRequestExpired(paymentRequestId);

        var noOfMonthsSlippage = 2;
        var endDateSlipped = await EndDateHasSlipped(applicationId, noOfMonthsSlippage);        
        
        return new GetVariationRequiredResponse
        {
            IsSubmitted = isSubmitted,
            IsExpired = isExpired,
            CostsChanged = projectDetails?.CostsChanged,
            EndDateSlipped = endDateSlipped,
            ThirdPartyContributionsChanged = projectDetails?.ThirdPartyContributionsChanged,
            BuildingName = buildingName,
            ApplicationReferenceNumber = applicationReferenceNumber            
        };        
    }

    public async Task<bool> EndDateHasSlipped(Guid applicationId, int slippageInMonths)
    {
        var endVersionDates = await _paymentRequestRepository.GetPaymentRequestEndVersionDates(applicationId);
        if (endVersionDates == null)
        {
            return false;
        }
        if (endVersionDates.MostRecentVersion > 1)
        {
            if ((endVersionDates.MostRecentEndDate != null) &&
                (endVersionDates.PreviousEndDate != null))
            {                
                var monthDifference = ((endVersionDates.MostRecentEndDate.Value.Month + endVersionDates.MostRecentEndDate.Value.Year * 12) - 
                                       (endVersionDates.PreviousEndDate.Value.Month + endVersionDates.PreviousEndDate.Value.Year * 12));
                return (Math.Abs(monthDifference) >= slippageInMonths);
            }
        }

        return false;
    }
}
