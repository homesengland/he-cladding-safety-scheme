using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Interface;
using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.PaymentRequest.GetCladdingRemoved;

public class GetCladdingRemovedHandler : IRequestHandler<GetCladdingRemovedRequest, GetCladdingRemovedResponse>
{
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IApplicationRepository _applicationRepository;
    private readonly IBuildingDetailsRepository _buildingDetailsRepository;
    private readonly IPaymentRequestRepository _paymentRequestRepository;

    public GetCladdingRemovedHandler(IApplicationDataProvider applicationDataProvider,
                                     IApplicationRepository applicationRepository,
                                     IBuildingDetailsRepository buildingDetailsRepository,
                                     IPaymentRequestRepository paymentRequestRepository)
    {
        _applicationDataProvider = applicationDataProvider;
        _applicationRepository = applicationRepository;
        _buildingDetailsRepository = buildingDetailsRepository;
        _paymentRequestRepository = paymentRequestRepository;
    }

    public async ValueTask<GetCladdingRemovedResponse> Handle(GetCladdingRemovedRequest request, CancellationToken cancellationToken)
    {
        var applicationId = _applicationDataProvider.GetApplicationId();
        var paymentRequestId = _applicationDataProvider.GetPaymentRequestId();

        var applicationReferenceNumber = await _applicationRepository.GetApplicationReferenceNumber(applicationId);
        var buildingName = await _buildingDetailsRepository.GetBuildingUniqueName(applicationId);

        var projectDetails = await _paymentRequestRepository.GetPaymentRequestProjectDetails(paymentRequestId);

        var isSubmitted = await _paymentRequestRepository.IsPaymentRequestSubmitted(paymentRequestId);        
        var isExpired = await _paymentRequestRepository.IsPaymentRequestExpired(paymentRequestId);

        return new GetCladdingRemovedResponse
        {
            IsSubmitted = isSubmitted,
            IsExpired = isExpired,
            UnsafeCladdingRemoved = projectDetails?.UnsafeCladdingRemoved,
            BuildingName = buildingName,
            ApplicationReferenceNumber = applicationReferenceNumber            
        };        
    }
}
