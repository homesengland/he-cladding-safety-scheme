using HE.Remediation.Core.Enums;
using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.FireRiskAppraisal.InternalWorksRequired;

public class SetInternalWorksRequiredRequest: IRequest
{
    public ENoYes WorkRequired { get; set; }
}
