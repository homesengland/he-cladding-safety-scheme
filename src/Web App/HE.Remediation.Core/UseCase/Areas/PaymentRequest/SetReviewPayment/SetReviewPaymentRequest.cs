using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.PaymentRequest.SetReviewPayment;

public class SetReviewPaymentRequest : IRequest
{
    public bool ChangeToMonthlyCost { get; set; }
    
    public string ReasonForChange { get; set; }
}
