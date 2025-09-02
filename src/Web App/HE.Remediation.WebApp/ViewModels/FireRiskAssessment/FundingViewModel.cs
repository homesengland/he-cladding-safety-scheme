using HE.Remediation.Core.Enums;

namespace HE.Remediation.WebApp.ViewModels.FireRiskAssessment;

public class FundingViewModel
{
    public bool? HasFunding { get; set; }
    public bool? HasDefects { get; set; }
    public EFraFundingType? HasFundingType { get; set; }
    public EFraFundingType? HasNoFundingType { get; set; }

    public ESubmitAction SubmitAction { get; set; }
}