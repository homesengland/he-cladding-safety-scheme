using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Data.StoredProcedureParameters;
using HE.Remediation.Core.Enums;
using HE.Remediation.Core.Exceptions;
using HE.Remediation.Core.Interface;
using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.PaymentRequest.GetCheckYourAnswers;

public class GetCheckYourAnswersHandler : IRequestHandler<GetCheckYourAnswersRequest, GetCheckYourAnswersResponse>
{
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IApplicationRepository _applicationRepository;
    private readonly IBuildingDetailsRepository _buildingDetailsRepository;
    private readonly IPaymentRequestRepository _paymentRequestRepository;

    public GetCheckYourAnswersHandler(IApplicationDataProvider applicationDataProvider,
                                      IApplicationRepository applicationRepository,
                                      IBuildingDetailsRepository buildingDetailsRepository,
                                      IPaymentRequestRepository paymentRequestRepository)
    {
        _applicationDataProvider = applicationDataProvider;
        _applicationRepository = applicationRepository;
        _buildingDetailsRepository = buildingDetailsRepository;    
        _paymentRequestRepository = paymentRequestRepository;
    }

    public async ValueTask<GetCheckYourAnswersResponse> Handle(GetCheckYourAnswersRequest request, CancellationToken cancellationToken)
    {
        var applicationId = _applicationDataProvider.GetApplicationId();
        var paymentRequestId = _applicationDataProvider.GetPaymentRequestId();

        var requestDetails = await _paymentRequestRepository.GetPaymentRequestDetails(applicationId, paymentRequestId);

        var applicationReferenceNumber = await _applicationRepository.GetApplicationReferenceNumber(applicationId);
        var buildingName = await _buildingDetailsRepository.GetBuildingUniqueName(applicationId);

        var paymentRequestCostFiles = await _paymentRequestRepository.GetCostReportsForPaymentRequest(paymentRequestId);
        var paymentRequestCostFileNames = paymentRequestCostFiles?
            .Select(f => new PaymentRequestCostFile { Name = f.Name })
            .ToList();

        var invoiceFiles = await _paymentRequestRepository.GetPaymentRequestInvoiceFiles(new GetPaymentRequestInvoiceFilesParameters
        {
            ApplicationId = applicationId,
            PaymentRequestId = paymentRequestId
        });

        var lastCommunicationEvidenceFiles = await _paymentRequestRepository.GetLeaseholderResidentUploadEvidenceForPaymentRequest(paymentRequestId);

        var teamMembers = await _paymentRequestRepository.GetPaymentRequestTeamMembers();

        var keyDates = await _paymentRequestRepository.GetLatestWorkPackageKeyDates(paymentRequestId, true);

        var projectDetails = await _paymentRequestRepository.GetPaymentRequestProjectDetails(paymentRequestId);

        string paymentRequestName = requestDetails?.CreatedDate.ToString("MMMM");

        var costsProfile = await _paymentRequestRepository.GetCostsProfile();
        if (costsProfile is null)
        {
            throw new EntityNotFoundException("Cannot locate cost profile for schedule of work.");
        }
        var currentPayment = costsProfile.FirstOrDefault(x => x.Type == EPaymentRequestCostType.CurrentPayment);
        var additionalPayment = costsProfile.FirstOrDefault(x => x.Type == EPaymentRequestCostType.AdditionalPayment);

        var isSubmitted = await _paymentRequestRepository.IsPaymentRequestSubmitted(paymentRequestId);
        var isExpired = await _paymentRequestRepository.IsPaymentRequestExpired(paymentRequestId);

        var unsafeCladdingAlreadyRemoved = await _paymentRequestRepository.GetUnsafeCladdingAlreadyRemoved(applicationId, paymentRequestId);

        return new GetCheckYourAnswersResponse
        {
            IsSubmitted = isSubmitted,
            IsExpired = isExpired,
            PaymentRequestName = paymentRequestName,
            CostsChanged = projectDetails?.CostsChanged,
            ThirdPartyContributionsChanged = projectDetails?.ThirdPartyContributionsChanged,
            ReasonForChange = requestDetails?.ReasonForChange,
            UnsafeCladdingRemoved = projectDetails?.UnsafeCladdingRemoved,
            UnsafeCladdingRemovedDate = projectDetails?.UnsafeCladdingRemovedDate,
            ExpectedStartDate = keyDates?.StartDate,
            ExpectedEndDate = keyDates?.ExpectedDateForCompletion,
            ScheduledAmount = requestDetails?.ScheduledAmountCost,
            LastCommunicationDate = projectDetails?.LeaseholderResidentCommunicationDate,
            PaymentRequestLastCommunicationFileNames = lastCommunicationEvidenceFiles?.Select(x => x.Name).ToList(),
            ProjectDatesChanged = projectDetails?.ProjectDatesChanged,
            CurrentMonthCost = (currentPayment?.Value ?? 0),
            AdditionalCostMonthTitle = additionalPayment?.ItemName,
            AdditionalCostAmount = additionalPayment?.ConfirmedValue,
            PaymentRequestCostFiles = paymentRequestCostFileNames,
            PaymentRequestInvoiceFileNames = invoiceFiles.Select(x => x.Name).ToList(),
            TeamMembers = teamMembers,
            ExpectedSubmissionDateForGateway3Application = projectDetails?.ExpectedSubmissionDateForGateway3Application,
            ExpectedPracticalCompletionDate = projectDetails?.ExpectedPracticalCompletionDate,
            UnsafeCladdingAlreadyRemoved = unsafeCladdingAlreadyRemoved,
            BuildingName = buildingName,
            ApplicationReferenceNumber = applicationReferenceNumber
        };
    }
}
