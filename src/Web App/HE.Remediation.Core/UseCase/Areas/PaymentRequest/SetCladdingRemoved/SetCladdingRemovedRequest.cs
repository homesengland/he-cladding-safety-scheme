using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.PaymentRequest.SetCladdingRemoved;

public class SetCladdingRemovedRequest : IRequest
{
    public bool? UnsafeCladdingRemoved { get; set; }        
}
