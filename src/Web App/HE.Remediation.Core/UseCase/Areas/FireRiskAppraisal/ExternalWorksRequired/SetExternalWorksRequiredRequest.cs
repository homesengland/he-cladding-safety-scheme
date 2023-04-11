using HE.Remediation.Core.Enums;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.FireRiskAppraisal.ExternalWorksRequired
{
    public class SetExternalWorksRequiredRequest: IRequest
    {
        public ENoYes WorkRequired { get; set; }
    }
}
