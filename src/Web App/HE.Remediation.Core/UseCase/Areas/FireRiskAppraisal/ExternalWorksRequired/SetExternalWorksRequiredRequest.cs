using HE.Remediation.Core.Enums;
using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.FireRiskAppraisal.ExternalWorksRequired
{
    public class SetExternalWorksRequiredRequest: IRequest
    {
        public ENoYes WorkRequired { get; set; }
    }
}
