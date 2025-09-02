using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HE.Remediation.Core.UseCase.Areas.ClosingReport.SetReviewPayment;

public class SetReviewPaymentRequest : IRequest
{
    public bool ChangeToMonthlyCost { get; set; }
    
    public string ReasonForChange { get; set; }

    public bool Confirm { get; set; }
}
