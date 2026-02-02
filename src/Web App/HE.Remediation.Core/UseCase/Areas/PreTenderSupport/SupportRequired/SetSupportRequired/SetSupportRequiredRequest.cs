using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.PreTenderSupport.SupportRequired.SetSupportRequired
{
    public class SetSupportRequiredRequest : IRequest
    {
        public bool SupportRequired { get; set; }
    }
}
