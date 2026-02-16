using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.PaymentRequest.SetChangeCladdingRemovedDate;

public class SetChangeCladdingRemovedDateRequest : IRequest
{
    public int? DateRemovedMonth { get; set; }
    public int? DateRemovedYear { get; set; }
}
