using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Interface;
using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.PaymentRequest.GetSubContractorRating;

public class GetSubContractorRatingsHandler : IRequestHandler<GetSubContractorRatingsRequest, GetSubContractorRatingsResponse>
{
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IApplicationRepository _applicationRepository;
    private readonly IBuildingDetailsRepository _buildingDetailsRepository;
    private readonly IPaymentRequestRepository _paymentRequestRepository;
    private readonly ISubContractorSurveyRepository _subContractorSurveyRepository;

    public GetSubContractorRatingsHandler(IApplicationDataProvider applicationDataProvider,
        IApplicationRepository applicationRepository,
        IBuildingDetailsRepository buildingDetailsRepository,
        ISubContractorSurveyRepository subContractorSurveyRepository, IPaymentRequestRepository paymentRequestRepository)
    {
        _applicationDataProvider = applicationDataProvider;
        _applicationRepository = applicationRepository;
        _buildingDetailsRepository = buildingDetailsRepository;
        _subContractorSurveyRepository = subContractorSurveyRepository;
        _paymentRequestRepository = paymentRequestRepository;
    }

    public async ValueTask<GetSubContractorRatingsResponse> Handle(GetSubContractorRatingsRequest request,
        CancellationToken cancellationToken)
    {
        var applicationId = _applicationDataProvider.GetApplicationId();
        var paymentRequestId = _applicationDataProvider.GetPaymentRequestId();

        var applicationReferenceNumber = await _applicationRepository.GetApplicationReferenceNumber(applicationId);
        var buildingName = await _buildingDetailsRepository.GetBuildingUniqueName(applicationId);

        var ratings = await _subContractorSurveyRepository.GetRating(request.Id);
        var isSubmitted = await _paymentRequestRepository.IsPaymentRequestSubmitted(paymentRequestId);

        return new GetSubContractorRatingsResponse
        {
            Ratings = ratings,
            IsSubmitted = isSubmitted,
            BuildingName = buildingName,
            ApplicationReferenceNumber = applicationReferenceNumber
        };
    }
}