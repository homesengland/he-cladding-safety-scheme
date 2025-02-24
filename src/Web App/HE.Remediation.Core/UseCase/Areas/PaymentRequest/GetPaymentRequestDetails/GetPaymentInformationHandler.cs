using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Interface;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.PaymentRequest.GetPaymentRequestDetails;

public class
    GetPaymentRequestDetailsHandler : IRequestHandler<GetPaymentRequestDetailsRequest, GetPaymentRequestDetailsResponse>
{
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IPaymentRequestRepository _paymentRequestRepository;
    private readonly ISubContractorSurveyRepository _subContractorSurveyRepository;


    public GetPaymentRequestDetailsHandler(IApplicationDataProvider applicationDataProvider,
                                           IPaymentRequestRepository paymentRequestRepository,
                                           ISubContractorSurveyRepository subContractorSurveyRepository)
    {
        _applicationDataProvider = applicationDataProvider;
        _paymentRequestRepository = paymentRequestRepository;
        _subContractorSurveyRepository = subContractorSurveyRepository;
    }

    public async Task<GetPaymentRequestDetailsResponse> Handle(GetPaymentRequestDetailsRequest request,
        CancellationToken cancellationToken)
    {
        var applicationId = _applicationDataProvider.GetApplicationId();

        var paymentRequestId = _applicationDataProvider.GetPaymentRequestId();
        var version = await _paymentRequestRepository.GetPaymentRequestVersion(paymentRequestId);

        var subContractorCount = await _subContractorSurveyRepository.GetSubcontractorSurveyCount(applicationId);

        return new GetPaymentRequestDetailsResponse
        {
            Version = version ?? -1,
            SubContractorCount = subContractorCount
        };
    }
}