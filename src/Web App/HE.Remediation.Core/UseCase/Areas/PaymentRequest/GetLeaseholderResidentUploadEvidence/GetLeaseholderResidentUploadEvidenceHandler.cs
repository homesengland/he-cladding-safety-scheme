using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Interface;
using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.PaymentRequest.GetLeaseholderResidentUploadEvidence;

public class GetLeaseholderResidentUploadEvidenceHandler : IRequestHandler<GetLeaseholderResidentUploadEvidenceRequest, GetLeaseholderResidentUploadEvidenceResponse>
{
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IApplicationRepository _applicationRepository;
    private readonly IBuildingDetailsRepository _buildingDetailsRepository;
    private readonly IPaymentRequestRepository _paymentRequestRepository;

    public GetLeaseholderResidentUploadEvidenceHandler(IApplicationDataProvider applicationDataProvider,
                                      IApplicationRepository applicationRepository,
                                      IBuildingDetailsRepository buildingDetailsRepository,
                                      IPaymentRequestRepository paymentRequestRepository)
    {
        _applicationDataProvider = applicationDataProvider;
        _applicationRepository = applicationRepository;
        _buildingDetailsRepository = buildingDetailsRepository;
        _paymentRequestRepository = paymentRequestRepository;
    }

    public async ValueTask<GetLeaseholderResidentUploadEvidenceResponse> Handle(GetLeaseholderResidentUploadEvidenceRequest request, CancellationToken cancellationToken)
    {
        var applicationId = _applicationDataProvider.GetApplicationId();
        var paymentRequestId = _applicationDataProvider.GetPaymentRequestId();

        var applicationReferenceNumber = await _applicationRepository.GetApplicationReferenceNumber(applicationId);
        var buildingName = await _buildingDetailsRepository.GetBuildingUniqueName(applicationId);

        var costReportFiles = await _paymentRequestRepository.GetLeaseholderResidentUploadEvidenceForPaymentRequest(paymentRequestId);

        var projectDetails = await _paymentRequestRepository.GetPaymentRequestProjectDetails(paymentRequestId);

        var isSubmitted = await _paymentRequestRepository.IsPaymentRequestSubmitted(paymentRequestId);
        var isExpired = await _paymentRequestRepository.IsPaymentRequestExpired(paymentRequestId);

        return new GetLeaseholderResidentUploadEvidenceResponse
        {
            IsSubmitted = isSubmitted,
            IsExpired = isExpired,
            AddedFiles = costReportFiles,
            BuildingName = buildingName,
            ApplicationReferenceNumber = applicationReferenceNumber,
            LastCommunicationDateMonth = projectDetails?.LeaseholderResidentCommunicationDate?.Month,
            LastCommunicationDateYear = projectDetails?.LeaseholderResidentCommunicationDate?.Year
        };
    }
}
