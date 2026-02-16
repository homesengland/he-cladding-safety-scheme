using System.Transactions;
using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Interface;
using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.PaymentRequest.GetSubContractors;

public class GetSubContractorsHandler : IRequestHandler<GetSubContractorsRequest, GetSubContractorsResponse>
{
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IApplicationRepository _applicationRepository;
    private readonly IBuildingDetailsRepository _buildingDetailsRepository;
    private readonly IPaymentRequestRepository _paymentRequestRepository;
    private readonly ISubContractorSurveyRepository _subContractorSurveyRepository;

    public GetSubContractorsHandler(IApplicationDataProvider applicationDataProvider,
        IApplicationRepository applicationRepository,
        IBuildingDetailsRepository buildingDetailsRepository,
        IPaymentRequestRepository paymentRequestRepository, 
        ISubContractorSurveyRepository subContractorSurveyRepository)
    {
        _applicationDataProvider = applicationDataProvider;
        _applicationRepository = applicationRepository;
        _buildingDetailsRepository = buildingDetailsRepository;
        _paymentRequestRepository = paymentRequestRepository;
        _subContractorSurveyRepository = subContractorSurveyRepository;
    }

    public async ValueTask<GetSubContractorsResponse> Handle(GetSubContractorsRequest request,
        CancellationToken cancellationToken)
    {
        var applicationId = _applicationDataProvider.GetApplicationId();
        var paymentRequestId = _applicationDataProvider.GetPaymentRequestId();

        var applicationReferenceNumber = await _applicationRepository.GetApplicationReferenceNumber(applicationId);
        var buildingName = await _buildingDetailsRepository.GetBuildingUniqueName(applicationId);

        var ratingsId = await _paymentRequestRepository.GetSubcontractorSurveyId(paymentRequestId);
        if(!ratingsId.HasValue)
        {
            using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
            ratingsId = await _subContractorSurveyRepository.CreateSurvey(applicationId);
            await _paymentRequestRepository.UpdateSubcontractorSurveyId(paymentRequestId, ratingsId.Value);
            scope.Complete();
        }

        var subContractors = await _subContractorSurveyRepository.GetSurveyOverview(ratingsId.Value);
        var isSubmitted = await _paymentRequestRepository.IsPaymentRequestSubmitted(paymentRequestId);

        return new GetSubContractorsResponse
        {
            SubContractors = subContractors,
            IsSubmitted = isSubmitted,
            BuildingName = buildingName,
            ApplicationReferenceNumber = applicationReferenceNumber
        };
    }
}