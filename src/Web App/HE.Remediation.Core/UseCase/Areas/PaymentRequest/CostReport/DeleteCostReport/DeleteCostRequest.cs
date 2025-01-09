using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.PaymentRequest.CostReport.DeleteCostReport;

public class DeleteCostRequest : IRequest
{
    public Guid FileId { get; set; }

    public string ReturnUrl { get; set; }
}
