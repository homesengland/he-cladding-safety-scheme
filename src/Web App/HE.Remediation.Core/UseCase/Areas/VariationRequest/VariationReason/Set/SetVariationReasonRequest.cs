using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.VariationRequest.VariationReason.Set;

public class SetVariationReasonRequest : IRequest
{
    public bool? IsCostVariation { get; set; }

    public bool? IsScopeVariation { get; set; }
    
    public bool? IsTimescaleVariation { get; set; }

    public bool? IsThirdPartyContributionVariation { get; set; }
}
