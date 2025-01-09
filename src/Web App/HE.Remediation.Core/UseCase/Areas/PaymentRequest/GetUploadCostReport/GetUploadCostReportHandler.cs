using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Interface;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.PaymentRequest.GetUploadCostReport;

public class GetUploadCostReportHandler : IRequestHandler<GetUploadCostReportRequest, GetUploadCostReportResponse>
{
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IApplicationRepository _applicationRepository;
    private readonly IBuildingDetailsRepository _buildingDetailsRepository;
    private readonly IPaymentRequestRepository _paymentRequestRepository;

    public GetUploadCostReportHandler(IApplicationDataProvider applicationDataProvider,
                                      IApplicationRepository applicationRepository,
                                      IBuildingDetailsRepository buildingDetailsRepository,
                                      IPaymentRequestRepository paymentRequestRepository)
    {
        _applicationDataProvider = applicationDataProvider;
        _applicationRepository = applicationRepository;
        _buildingDetailsRepository = buildingDetailsRepository;    
        _paymentRequestRepository = paymentRequestRepository;
    }

    public async Task<GetUploadCostReportResponse> Handle(GetUploadCostReportRequest request, CancellationToken cancellationToken)
    {
        var applicationId = _applicationDataProvider.GetApplicationId();
        var paymentRequestId = _applicationDataProvider.GetPaymentRequestId();

        var applicationReferenceNumber = await _applicationRepository.GetApplicationReferenceNumber(applicationId);
        var buildingName = await _buildingDetailsRepository.GetBuildingUniqueName(applicationId);
        
        var costReportFiles = await _paymentRequestRepository.GetCostReportsForPaymentRequest(paymentRequestId);

        var isSubmitted = await _paymentRequestRepository.IsPaymentRequestSubmitted(paymentRequestId);
        var isExpired = await _paymentRequestRepository.IsPaymentRequestExpired(paymentRequestId);

        return new GetUploadCostReportResponse
        {            
            IsSubmitted = isSubmitted,
            IsExpired = isExpired,
            AddedFiles = costReportFiles,
            BuildingName = buildingName,
            ApplicationReferenceNumber = applicationReferenceNumber            
        };        
    }
}
