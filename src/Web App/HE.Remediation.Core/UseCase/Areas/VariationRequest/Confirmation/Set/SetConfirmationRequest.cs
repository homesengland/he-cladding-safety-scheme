using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.VariationRequest.Confirmation.Set
{
    public class SetConfirmationRequest : IRequest
    {
        public string VariationSummary { get; set; }
    }
}
