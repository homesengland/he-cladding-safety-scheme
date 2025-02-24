using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Data.StoredProcedureParameters.PaymentRequest;
using HE.Remediation.Core.Interface;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.PaymentRequest.SetCostsChanged;

public class SetCostsChangedHandler : IRequestHandler<SetCostsChangedRequest, SetCostsChangedResponse>
{
    private readonly IApplicationDataProvider _adp;
    private readonly IPaymentRequestRepository _paymentRequestRepository;

    public SetCostsChangedHandler(IApplicationDataProvider adp,
                                  IPaymentRequestRepository paymentRequestRepository)
    {
        _adp = adp;
        _paymentRequestRepository = paymentRequestRepository;
    }

    public async Task<SetCostsChangedResponse> Handle(SetCostsChangedRequest request, CancellationToken cancellationToken)
    {
        var paymentRequestId = _adp.GetPaymentRequestId();

        PaymentRequestCostChangedParameters costParams = new PaymentRequestCostChangedParameters
        {
            CostsChanged = request?.CostsChanged
        };

        await _paymentRequestRepository.UpdatePaymentRequestCostChangedDetails(paymentRequestId, costParams);

        return new SetCostsChangedResponse
        {
            CostsChanged = request?.CostsChanged
        };
    }
}
