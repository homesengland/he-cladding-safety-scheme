using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Interface;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.PaymentRequest.GetDeclaration;

public class GetDeclarationHandler : IRequestHandler<GetDeclarationRequest, GetDeclarationResponse>
{
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IApplicationRepository _applicationRepository;
    private readonly IBuildingDetailsRepository _buildingDetailsRepository;
    private readonly IPaymentRequestRepository _paymentRequestRepository;

    public GetDeclarationHandler(IApplicationDataProvider applicationDataProvider,
                                 IApplicationRepository applicationRepository,
                                 IBuildingDetailsRepository buildingDetailsRepository,
                                 IPaymentRequestRepository paymentRequestRepository)
    {
        _applicationDataProvider = applicationDataProvider;
        _applicationRepository = applicationRepository;
        _buildingDetailsRepository = buildingDetailsRepository;    
        _paymentRequestRepository = paymentRequestRepository;
    }

    public async Task<GetDeclarationResponse> Handle(GetDeclarationRequest request, CancellationToken cancellationToken)
    {
        var applicationId = _applicationDataProvider.GetApplicationId();
        var paymentRequestId = _applicationDataProvider.GetPaymentRequestId();

        var applicationReferenceNumber = await _applicationRepository.GetApplicationReferenceNumber(applicationId);
        var buildingName = await _buildingDetailsRepository.GetBuildingUniqueName(applicationId);

        var declarationDetails = await _paymentRequestRepository.GetPaymentRequestDeclarationDetails(paymentRequestId);

        var isSubmitted = await _paymentRequestRepository.IsPaymentRequestSubmitted(paymentRequestId);
        var isExpired = await _paymentRequestRepository.IsPaymentRequestExpired(paymentRequestId);

        return new GetDeclarationResponse
        {
            IsSubmitted = isSubmitted,
            IsExpired = isExpired,
            AwareProcess = declarationDetails?.AwareProcess,
	        AwareNoPrecedentForFuture = declarationDetails?.AwareNoPrecedentForFuture,
	        PredictionsAccurate = declarationDetails?.PredictionsAccurate,
            BuildingName = buildingName,
            ApplicationReferenceNumber = applicationReferenceNumber
        };
    }
}
