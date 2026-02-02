using System.Transactions;
using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Interface;
using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.PaymentRequest.GetSubContractorCheckYourAnswers;

public class GetSubContractorCheckYourAnswersHandler : IRequestHandler<GetSubContractorCheckYourAnswersRequest, GetSubContractorCheckYourAnswersResponse>
{
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IApplicationRepository _applicationRepository;
    private readonly IBuildingDetailsRepository _buildingDetailsRepository;
    private readonly IPaymentRequestRepository _paymentRequestRepository;
    private readonly ISubContractorSurveyRepository _subContractorSurveyRepository;

    public GetSubContractorCheckYourAnswersHandler(IApplicationDataProvider applicationDataProvider,
        IApplicationRepository applicationRepository,
        IBuildingDetailsRepository buildingDetailsRepository,
        ISubContractorSurveyRepository subContractorSurveyRepository, 
        IPaymentRequestRepository paymentRequestRepository)
    {
        _applicationDataProvider = applicationDataProvider;
        _applicationRepository = applicationRepository;
        _buildingDetailsRepository = buildingDetailsRepository;
        _subContractorSurveyRepository = subContractorSurveyRepository;
        _paymentRequestRepository = paymentRequestRepository;
    }

    public async ValueTask<GetSubContractorCheckYourAnswersResponse> Handle(GetSubContractorCheckYourAnswersRequest request,
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

        var summary = await _subContractorSurveyRepository.GetSummary(ratingsId.Value);
        var isSubmitted = await _paymentRequestRepository.IsPaymentRequestSubmitted(paymentRequestId);

        return new GetSubContractorCheckYourAnswersResponse
        {
            SubcontractorRatings = summary,
            IsSubmitted = isSubmitted,
            BuildingName = buildingName,
            ApplicationReferenceNumber = applicationReferenceNumber
        };
    }
}