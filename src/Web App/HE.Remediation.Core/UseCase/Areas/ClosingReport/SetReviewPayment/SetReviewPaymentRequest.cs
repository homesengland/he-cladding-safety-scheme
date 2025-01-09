using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.ClosingReport.SetReviewPayment;

public class SetReviewPaymentRequest : IRequest
{
    public bool ChangeToMonthlyCost { get; set; }
    
    public string ReasonForChange { get; set; }
}
