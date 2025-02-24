using HE.Remediation.Core.Enums;

namespace HE.Remediation.Core.UseCase.Areas.FireRiskAppraisal.InternalWorksRequired;

public class GetInternalWorksRequiredResponse
{
    public ENoYes? WorksRequired { get; set; }

    public ENoYes? ExternalWorksRequired { get; set; }    
}
