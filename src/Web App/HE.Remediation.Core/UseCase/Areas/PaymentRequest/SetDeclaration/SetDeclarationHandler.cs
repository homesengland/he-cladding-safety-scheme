using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Data.StoredProcedureParameters.PaymentRequest;
using HE.Remediation.Core.Interface;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.PaymentRequest.SetDeclaration;

public class SetDeclarationHandler : IRequestHandler<SetDeclarationRequest>
{
    private readonly IApplicationDataProvider _adp;
    private readonly IPaymentRequestRepository _paymentRequestRepository;

    public SetDeclarationHandler(IApplicationDataProvider adp, 
                                 IPaymentRequestRepository paymentRequestRepository)
    {
        _adp = adp;
        _paymentRequestRepository = paymentRequestRepository;
    }

    public async Task<Unit> Handle(SetDeclarationRequest request, CancellationToken cancellationToken)
    {
        var applicationId = _adp.GetApplicationId();
        var paymentRequestId = _adp.GetPaymentRequestId();

        PaymentRequestDeclarationParameters declarationParams = new PaymentRequestDeclarationParameters
        {
            AwareProcess = request?.AwareProcess,
            AwareNoPrecedentForFuture = request?.AwareNoPrecedentForFuture,
            PredictionsAccurate = request?.PredictionsAccurate
        };

        await _paymentRequestRepository.UpdatePaymentRequestDeclaration(paymentRequestId, declarationParams);

        return Unit.Value;
    }
}
