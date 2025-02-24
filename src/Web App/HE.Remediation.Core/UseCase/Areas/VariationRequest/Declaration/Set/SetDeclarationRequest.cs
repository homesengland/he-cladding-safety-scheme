using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.VariationRequest.Declaration.Set;

public class SetDeclarationRequest : IRequest
{
    public bool? ConfirmedAwareOfApproval { get; set; }

    public bool? ConfirmedCostsReasonable { get; set; }

    public bool? ConfirmedCoversRecommendations { get; set; }
}
