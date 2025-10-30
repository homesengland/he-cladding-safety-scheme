using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Interface;
using MediatR;
namespace HE.Remediation.Core.UseCase.Areas.PaymentRequest.GetPracticalCompletionDate;

public class GetPracticalCompletionDateHandler : IRequestHandler<GetPracticalCompletionDateRequest, GetPracticalCompletionDateResponse>
{
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IApplicationRepository _applicationRepository;
    private readonly IBuildingDetailsRepository _buildingDetailsRepository;
    private readonly IPaymentRequestRepository _paymentRequestRepository;

    public GetPracticalCompletionDateHandler(IApplicationDataProvider applicationDataProvider,
                                  IApplicationRepository applicationRepository,
                                  IBuildingDetailsRepository buildingDetailsRepository,
                                  IPaymentRequestRepository paymentRequestRepository)
    {
        _applicationDataProvider = applicationDataProvider;
        _applicationRepository = applicationRepository;
        _buildingDetailsRepository = buildingDetailsRepository;
        _paymentRequestRepository = paymentRequestRepository;
    }

    public async Task<GetPracticalCompletionDateResponse> Handle(GetPracticalCompletionDateRequest request, CancellationToken cancellationToken)
    {
        var applicationId = _applicationDataProvider.GetApplicationId();
        var paymentRequestId = _applicationDataProvider.GetPaymentRequestId();

        var applicationReferenceNumber = await _applicationRepository.GetApplicationReferenceNumber(applicationId);
        var buildingName = await _buildingDetailsRepository.GetBuildingUniqueName(applicationId);

        var projectDetails = await _paymentRequestRepository.GetPaymentRequestProjectDetails(paymentRequestId);

        var isSubmitted = await _paymentRequestRepository.IsPaymentRequestSubmitted(paymentRequestId);
        var isExpired = await _paymentRequestRepository.IsPaymentRequestExpired(paymentRequestId);

        Guid? previousPaymentRequestId = await _paymentRequestRepository.GetPreviousPaymentRequest(applicationId);

        DateTime? previousExpectedPracticalDate = null;

        if (previousPaymentRequestId is Guid prevPaymentRequestId)
        {
            var projectDetailsForLatestPayment = await _paymentRequestRepository.GetPaymentRequestProjectDetails(prevPaymentRequestId);
            previousExpectedPracticalDate = projectDetailsForLatestPayment?.ExpectedPracticalCompletionDate;
        }

        return new GetPracticalCompletionDateResponse
        {
            IsSubmitted = isSubmitted,
            IsExpired = isExpired,
            BuildingName = buildingName,
            ApplicationReferenceNumber = applicationReferenceNumber,
            ExpectedPracticalDateMonth = projectDetails?.ExpectedPracticalCompletionDate?.Month,
            ExpectedPracticalDateYear = projectDetails?.ExpectedPracticalCompletionDate?.Year,
            PreviousExpectedPracticalDate = previousExpectedPracticalDate
        };
    }
}
