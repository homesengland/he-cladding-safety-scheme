using HE.Remediation.Core.Enums;

namespace HE.Remediation.WebApp.ViewModels.FireRiskAppraisal;

public class InternalWorksRequiredViewModel
{
    public ENoYes? WorksRequired { get; set; }

    public ENoYes? ExternalWorksRequired { get; set; }

    public ESubmitAction? SubmitAction { get; set; }
}
