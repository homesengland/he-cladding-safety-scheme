using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Interface;
using Mediator;
namespace HE.Remediation.Core.UseCase.Areas.PaymentRequest.GetForecastGateway3Submission;

public class GetForecastGateway3SubmissionHandler : IRequestHandler<GetForecastGateway3SubmissionRequest, GetForecastGateway3SubmissionResponse>
{
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IApplicationRepository _applicationRepository;
    private readonly IBuildingDetailsRepository _buildingDetailsRepository;
    private readonly IPaymentRequestRepository _paymentRequestRepository;

    public GetForecastGateway3SubmissionHandler(IApplicationDataProvider applicationDataProvider,
                                  IApplicationRepository applicationRepository,
                                  IBuildingDetailsRepository buildingDetailsRepository,
                                  IPaymentRequestRepository paymentRequestRepository)
    {
        _applicationDataProvider = applicationDataProvider;
        _applicationRepository = applicationRepository;
        _buildingDetailsRepository = buildingDetailsRepository;
        _paymentRequestRepository = paymentRequestRepository;
    }

    public async ValueTask<GetForecastGateway3SubmissionResponse> Handle(GetForecastGateway3SubmissionRequest request, CancellationToken cancellationToken)
    {
        var applicationId = _applicationDataProvider.GetApplicationId();
        var paymentRequestId = _applicationDataProvider.GetPaymentRequestId();

        var applicationReferenceNumber = await _applicationRepository.GetApplicationReferenceNumber(applicationId);
        var buildingName = await _buildingDetailsRepository.GetBuildingUniqueName(applicationId);

        var projectDetails = await _paymentRequestRepository.GetPaymentRequestProjectDetails(paymentRequestId);

        var isSubmitted = await _paymentRequestRepository.IsPaymentRequestSubmitted(paymentRequestId);
        var isExpired = await _paymentRequestRepository.IsPaymentRequestExpired(paymentRequestId);

        Guid? previousPaymentRequestId = await _paymentRequestRepository.GetPreviousPaymentRequest(applicationId);

        DateTime? previousExpectedSubmissionDate = null;

        if (previousPaymentRequestId is Guid prevPaymentRequestId)
        {
            var projectDetailsForLatestPayment = await _paymentRequestRepository.GetPaymentRequestProjectDetails(prevPaymentRequestId);
            previousExpectedSubmissionDate = projectDetailsForLatestPayment?.ExpectedSubmissionDateForGateway3Application;
        }

        return new GetForecastGateway3SubmissionResponse
        {
            IsSubmitted = isSubmitted,
            IsExpired = isExpired,
            BuildingName = buildingName,
            ApplicationReferenceNumber = applicationReferenceNumber,
            ExpectedSubmissionDateMonth = projectDetails?.ExpectedSubmissionDateForGateway3Application?.Month,
            ExpectedSubmissionDateYear = projectDetails?.ExpectedSubmissionDateForGateway3Application?.Year,
            PreviousExpectedSubmissionDate = previousExpectedSubmissionDate
        };
    }
}
