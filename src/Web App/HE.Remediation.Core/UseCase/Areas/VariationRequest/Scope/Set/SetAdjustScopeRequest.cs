using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.VariationRequest.Scope.Set
{
    public class SetAdjustScopeRequest : IRequest
    {
        public string ChangeOfScope { get; set; }
    }
}
