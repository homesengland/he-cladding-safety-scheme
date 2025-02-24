using HE.Remediation.Core.Enums;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.FireRiskAppraisal.InternalWorksRequired;

public class SetInternalWorksRequiredRequest: IRequest
{
    public ENoYes WorkRequired { get; set; }
}
