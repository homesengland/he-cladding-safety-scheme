using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.PaymentRequest.Start;

public class CheckStatusRequest : IRequest<CheckStatusResponse>
{
    public Guid PaymentRequestId { get; set; }
}
