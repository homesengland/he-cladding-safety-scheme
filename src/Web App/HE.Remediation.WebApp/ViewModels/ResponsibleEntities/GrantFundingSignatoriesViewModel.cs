using HE.Remediation.Core.Enums;

namespace HE.Remediation.WebApp.ViewModels.ResponsibleEntities;

public class GrantFundingSignatoriesViewModel
{
    public IEnumerable<GrantFundingSignatoryViewModel> GrantFundingSignatories { get; set; }

    public ESubmitAction SubmitAction { get; set; }
    public string ReturnUrl { get; set; }
}