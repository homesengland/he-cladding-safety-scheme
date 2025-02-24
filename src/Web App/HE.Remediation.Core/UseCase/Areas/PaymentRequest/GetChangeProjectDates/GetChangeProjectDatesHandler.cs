using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Interface;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.PaymentRequest.GetChangeProjectDates;

public class GetChangeProjectDatesHandler : IRequestHandler<GetChangeProjectDatesRequest, GetChangeProjectDatesResponse>
{
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IApplicationRepository _applicationRepository;
    private readonly IBuildingDetailsRepository _buildingDetailsRepository;
    private readonly IPaymentRequestRepository _paymentRequestRepository;

    public GetChangeProjectDatesHandler(IApplicationDataProvider applicationDataProvider,
                                        IApplicationRepository applicationRepository,
                                        IBuildingDetailsRepository buildingDetailsRepository,
                                        IPaymentRequestRepository paymentRequestRepository)
    {
        _applicationDataProvider = applicationDataProvider;
        _applicationRepository = applicationRepository;
        _buildingDetailsRepository = buildingDetailsRepository;
        _paymentRequestRepository = paymentRequestRepository;   
    }

    public async Task<GetChangeProjectDatesResponse> Handle(GetChangeProjectDatesRequest request, CancellationToken cancellationToken)
    {
        var applicationId = _applicationDataProvider.GetApplicationId();
        var paymentRequestId = _applicationDataProvider.GetPaymentRequestId();

        var applicationReferenceNumber = await _applicationRepository.GetApplicationReferenceNumber(applicationId);
        var buildingName = await _buildingDetailsRepository.GetBuildingUniqueName(applicationId);

        var isSubmitted = await _paymentRequestRepository.IsPaymentRequestSubmitted(paymentRequestId);
        var isExpired = await _paymentRequestRepository.IsPaymentRequestExpired(paymentRequestId);
        
        var keyDates = await _paymentRequestRepository.GetLatestWorkPackageKeyDates(paymentRequestId, true);

        var expectedStartDate = keyDates?.StartDate;
        if(expectedStartDate is null)
        {
            var originalKeyDates = await _paymentRequestRepository.GetLatestWorkPackageKeyDates(paymentRequestId, false);
            expectedStartDate = originalKeyDates?.StartDate;
        }

        return new GetChangeProjectDatesResponse
        {
            ExpectedStartDate = expectedStartDate,
            IsSubmitted = isSubmitted,
            IsExpired = isExpired,
            BuildingName = buildingName,
            ApplicationReferenceNumber = applicationReferenceNumber,
            ProjectDateEndMonth = keyDates?.ExpectedDateForCompletion?.Month,
            ProjectDateEndYear = keyDates?.ExpectedDateForCompletion?.Year
        };        
    }
}
