using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Interface;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.PaymentRequest.GetProjectDates;

public class GetProjectDatesHandler : IRequestHandler<GetProjectDatesRequest, GetProjectDatesResponse>
{
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IApplicationRepository _applicationRepository;
    private readonly IBuildingDetailsRepository _buildingDetailsRepository;
    private readonly IPaymentRequestRepository _paymentRequestRepository;

    public GetProjectDatesHandler(IApplicationDataProvider applicationDataProvider,
                                  IApplicationRepository applicationRepository,
                                  IBuildingDetailsRepository buildingDetailsRepository,
                                  IPaymentRequestRepository paymentRequestRepository)
    {
        _applicationDataProvider = applicationDataProvider;
        _applicationRepository = applicationRepository;
        _buildingDetailsRepository = buildingDetailsRepository;
        _paymentRequestRepository = paymentRequestRepository;   
    }

    public async Task<GetProjectDatesResponse> Handle(GetProjectDatesRequest request, CancellationToken cancellationToken)
    {
        var applicationId = _applicationDataProvider.GetApplicationId();
        var paymentRequestId = _applicationDataProvider.GetPaymentRequestId();

        var applicationReferenceNumber = await _applicationRepository.GetApplicationReferenceNumber(applicationId);
        var buildingName = await _buildingDetailsRepository.GetBuildingUniqueName(applicationId);

        var keyDates = await _paymentRequestRepository.GetLatestWorkPackageKeyDates(paymentRequestId);

        var isSubmitted = await _paymentRequestRepository.IsPaymentRequestSubmitted(paymentRequestId);
        var isExpired = await _paymentRequestRepository.IsPaymentRequestExpired(paymentRequestId);

        var projectDetails = await _paymentRequestRepository.GetPaymentRequestProjectDetails(paymentRequestId);

        var firstPaymentRequestId = await _paymentRequestRepository.GetPaymentRequestFirstPaymentRequest(applicationId);
        var isFirstPaymentRequestId = (firstPaymentRequestId is not null ? firstPaymentRequestId == paymentRequestId : false);

        return new GetProjectDatesResponse
        {
            IsSubmitted = isSubmitted,
            IsExpired = isExpired,
            ExpectedStartDate = keyDates?.StartDate,
            ExpectedEndDate = keyDates?.ExpectedDateForCompletion,
            IsFirstPaymentRequest = isFirstPaymentRequestId,
            BuildingName = buildingName,
            ApplicationReferenceNumber = applicationReferenceNumber,          
            ProjectDatesChanged = projectDetails?.ProjectDatesChanged
        };        
    }
}
