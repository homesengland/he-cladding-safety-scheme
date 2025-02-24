using HE.Remediation.Core.UseCase.Areas.ClosingReport.GetSubmitPayment;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.ClosingReport.SetSubmitPayment;

public class SetSubmitPaymentRequest  : IRequest<SetSubmitPaymentResponse>
{
    public MonthlyCost FinalMonthCost { get; set; }
}
