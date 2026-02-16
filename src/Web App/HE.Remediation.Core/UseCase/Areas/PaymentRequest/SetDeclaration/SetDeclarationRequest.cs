using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.PaymentRequest.SetDeclaration;

public class SetDeclarationRequest : IRequest
{
    public bool? AwareProcess { get; set; }
    public bool? AwareNoPrecedentForFuture { get; set; }
    public bool? PredictionsAccurate { get; set; }
}
