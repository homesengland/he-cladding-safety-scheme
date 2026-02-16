using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Data.StoredProcedureParameters;
using HE.Remediation.Core.Data.StoredProcedureResults;
using HE.Remediation.Core.Interface;
using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.PaymentRequest;

public class GetPaymentRequestInvoicesHandler : IRequestHandler<GetPaymentRequestInvoicesRequest, GetPaymentRequestInvoicesResponse>
{
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IApplicationRepository _applicationRepository;
    private readonly IBuildingDetailsRepository _buildingDetailsRepository;
    private readonly IPaymentRequestRepository _paymentRequestRepository;

    public GetPaymentRequestInvoicesHandler(
        IApplicationDataProvider applicationDataProvider, 
        IApplicationRepository applicationRepository, 
        IBuildingDetailsRepository buildingDetailsRepository, 
        IPaymentRequestRepository paymentRequestRepository)
    {
        _applicationDataProvider = applicationDataProvider;
        _applicationRepository = applicationRepository;
        _buildingDetailsRepository = buildingDetailsRepository;
        _paymentRequestRepository = paymentRequestRepository;
    }

    public async ValueTask<GetPaymentRequestInvoicesResponse> Handle(GetPaymentRequestInvoicesRequest request, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var applicationId = _applicationDataProvider.GetApplicationId();
        var paymentRequestId = _applicationDataProvider.GetPaymentRequestId();

        var referenceNumber = await _applicationRepository.GetApplicationReferenceNumber(applicationId);
        var buildingName = await _buildingDetailsRepository.GetBuildingUniqueName(applicationId);

        var isSubmitted = await _paymentRequestRepository.IsPaymentRequestSubmitted(paymentRequestId);
        var isExpired = await _paymentRequestRepository.IsPaymentRequestExpired(paymentRequestId);

        var files = await _paymentRequestRepository.GetPaymentRequestInvoiceFiles(
            new GetPaymentRequestInvoiceFilesParameters
            {
                ApplicationId = applicationId,
                PaymentRequestId = paymentRequestId
            });

        return new GetPaymentRequestInvoicesResponse
        {
            ApplicationReferenceNumber = referenceNumber,
            BuildingName = buildingName,
            IsSubmitted = isSubmitted,
            IsExpired = isExpired,
            AddedFiles = files.ToList()
        };
    }
}

public class GetPaymentRequestInvoicesRequest : IRequest<GetPaymentRequestInvoicesResponse>
{
    private GetPaymentRequestInvoicesRequest()
    {
    }

    public static readonly GetPaymentRequestInvoicesRequest Request = new();
}

public class GetPaymentRequestInvoicesResponse
{
    public IList<GetPaymentRequestInvoiceFilesResult> AddedFiles { get; set; }

    public string ApplicationReferenceNumber { get; set; }
    public string BuildingName { get; set; }
    public bool? IsSubmitted { get; set; }
    public bool IsExpired { get; set; }
}