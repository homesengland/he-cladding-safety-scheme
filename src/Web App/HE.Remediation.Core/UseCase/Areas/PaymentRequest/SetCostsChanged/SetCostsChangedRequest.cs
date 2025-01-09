using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.PaymentRequest.SetCostsChanged;

public class SetCostsChangedRequest : IRequest<SetCostsChangedResponse>
{
    public bool? CostsChanged { get; set; }
}
