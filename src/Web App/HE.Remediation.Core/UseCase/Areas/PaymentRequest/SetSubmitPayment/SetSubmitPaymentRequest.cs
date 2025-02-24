using HE.Remediation.Core.Data.StoredProcedureResults.Costs;
using HE.Remediation.Core.UseCase.Areas.PaymentRequest.GetSubmitPayment;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.PaymentRequest.SetSubmitPayment;

public class SetSubmitPaymentRequest  : IRequest
{
    public MonthlyCost FirstMonthCost { get; set; }

    public MonthlyCost FirstMonthAdditionalCost { get; set; }

    public MonthlyCost CurrentMonth { get; set; }

    public MonthlyCost FinalMonthCost { get; set; }

    public IList<MonthlyCost> MonthlyCosts { get; set; }
}
